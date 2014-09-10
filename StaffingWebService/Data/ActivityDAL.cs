using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using StaffingWebService.Data.DataExtention;
using StaffingWebService.Model;

namespace StaffingWebService.Data
{
    public class ActivityDAL
    {
        internal static CompteRenduActivite CreateCRA(int consultantId, int year, int month)
        {
            using (var entity = new StaffingModelContainer())
            {
                var item = (from cra in entity.CompteRenduActiviteTable.Where(o => o.ConsultantTableID == consultantId
                                                                                   && o.Month == month &&
                                                                                   o.Year == year)
                            select cra).SingleOrDefault();

                if (item != null)
                {
                    return item.CreateCompteRenduActivite();
                }


                entity.CompteRenduActiviteTable.Add(new CompteRenduActiviteTable
                                                        {
                                                            ConsultantTableID = consultantId,
                                                            Month = month,
                                                            Year = year,
                                                            Status = (int)ItemStatus.Editing
                                                        });
                entity.SaveChanges();

                item = (from cra in entity.CompteRenduActiviteTable.Where(o => o.ConsultantTableID == consultantId
                                                                               && o.Month == month &&
                                                                               o.Year == year)
                        select cra).Single();

                var activityDays = CreateMonthDays(consultantId, year, month);
                foreach (var activityDay in activityDays)
                {
                    entity.ActiviteTable.Add(activityDay.MorningActivity.CreateDataActivity(item.ID, false));
                    entity.ActiviteTable.Add(activityDay.AfternoonActivity.CreateDataActivity(item.ID, true));
                }

                entity.SaveChanges();

                return item.CreateCompteRenduActivite();
            }
        }

        private static IEnumerable<ActivityDay> CreateMonthDays(int consiltantId, int year, int month)
        {
            var nbOfDays = DateTime.DaysInMonth(year, month);
            var activityDays = new List<ActivityDay>();

            for (var i = 1; i <= nbOfDays; i++)
            {
                var date = new DateTime(year, month, i);
                var day = new ActivityDay
                              {
                                  Date = date,
                                  ConsultantId = consiltantId,
                                  IsWorkingDay = date.IsWorkingDay(),
                                  MorningActivity = new Activity
                                  {
                                      ActivityType = ActivityType.Vide,
                                      ConsultantId = consiltantId,
                                      Date = date
                                  },
                                  AfternoonActivity = new Activity
                                  {
                                      ActivityType = ActivityType.Vide,
                                      ConsultantId = consiltantId,
                                      Date = date
                                  }
                              };

                activityDays.Add(day);
            }

            return activityDays;
        }

        internal static IEnumerable<CompteRenduActivite> GetCRA(int? consultantId, int? year, int? month)
        {
            IEnumerable<CompteRenduActivite> cras;

            using (var entity = new StaffingModelContainer())
            {
                var items =
                    (from cra in
                         entity.CompteRenduActiviteTable.Where(
                             o => (!consultantId.HasValue || o.ConsultantTableID == consultantId)
                                  && (!year.HasValue || o.Year == year)
                                  && (!month.HasValue || o.Month == month))
                     select cra).ToList();

                cras = items.Select(o => o.CreateCompteRenduActivite()).ToList();
            }

            return cras;
        }

        internal static CompteRenduActivite GetCRAFromId(int craId)
        {
            using (var entity = new StaffingModelContainer())
            {
                var item = entity.CompteRenduActiviteTable.SingleOrDefault(o => o.ID == craId);

                if (item != null)
                {
                    return item.CreateCompteRenduActivite();
                }

                return null;
            }
        }

        internal static IEnumerable<ActivityWeek> GetActivities(int craId)
        {
            var cra = GetCRAFromId(craId);

            if (cra == null)
            {
                return null;
            }

            var days = CreateMonthDays(cra.ConsultantId, cra.Year, cra.Month);

            var weeks = new List<ActivityWeek>();

            using (var entity = new StaffingModelContainer())
            {
                CultureInfo ci = Thread.CurrentThread.CurrentCulture;

                var week = new ActivityWeek
                {
                    WeekNumber = ci.Calendar.GetWeekOfYear(new DateTime(cra.Year, cra.Month, 1),
                                                      ci.DateTimeFormat.CalendarWeekRule,
                                                      ci.DateTimeFormat.FirstDayOfWeek)
                };

                foreach (var activityDay in days)
                {
                    if (activityDay.Date.DayOfWeek == DayOfWeek.Monday)
                    {
                        if (week.Activities.Count > 0)
                        {
                            weeks.Add(week);
                        }

                        week = new ActivityWeek
                                   {
                                       WeekNumber = ci.Calendar.GetWeekOfYear(activityDay.Date,
                                                                              ci.DateTimeFormat.CalendarWeekRule,
                                                                              ci.DateTimeFormat.FirstDayOfWeek)
                                   };
                    }

                    var items = entity.ActiviteTable.Where(o => o.CompteRenduActiviteTableID == craId
                        && o.Day == activityDay.Date.Day).ToList();

                    foreach (var activity in items)
                    {
                        if (activity.Afternoon)
                        {
                            activityDay.AfternoonActivity = activity.CreateActivity(activityDay.ConsultantId, activityDay.Date);
                        }
                        else
                        {
                            activityDay.MorningActivity = activity.CreateActivity(activityDay.ConsultantId, activityDay.Date);
                        }
                    }

                    week.Activities.Add(activityDay);
                }

                weeks.Add(week);

                return weeks;
            }
        }

        internal static void UpdateActivities(IEnumerable<ActivityDay> days)
        {
            using (var entity = new StaffingModelContainer())
            {
                foreach (var day in days)
                {
                    entity.ActiviteTable.First(o => o.ID == day.MorningActivity.Id).Update(day.MorningActivity);
                    entity.ActiviteTable.First(o => o.ID == day.AfternoonActivity.Id).Update(day.AfternoonActivity);

                    entity.SaveChanges();
                }
            }
        }

        public static IEnumerable<CompteRenduActivite> UpdateCRA(CompteRenduActivite compteRenduActivite)
        {
            using (var entity = new StaffingModelContainer())
            {
                entity.CompteRenduActiviteTable.Single(o => o.ID == compteRenduActivite.Id).Update(compteRenduActivite);
                entity.SaveChanges();
            }

            switch (compteRenduActivite.Status)
            {
                case ItemStatus.Waiting:
                    Mail.Send(compteRenduActivite.ConsultantId, MailType.CRASubmited, compteRenduActivite);
                    break;
                case ItemStatus.Refused:
                    Mail.Send(compteRenduActivite.ConsultantId, MailType.CRARefused, compteRenduActivite);
                    break;
                case ItemStatus.Validate:
                    Mail.Send(compteRenduActivite.ConsultantId, MailType.CRAValidated, compteRenduActivite);
                    break;
            }

            return GetCRA(null, null, null);
        }
    }
}