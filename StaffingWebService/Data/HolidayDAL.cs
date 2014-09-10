using System.Collections.Generic;
using System.Linq;
using Cellenza.Service.Data.DataExtention;
using Cellenza.Service.Model;

namespace Cellenza.Service.Data
{
    internal class HolidayDAL
    {
        internal static IEnumerable<Holiday> GetHolidays(int? consultantId)
        {
            IEnumerable<Holiday> holidays;
            using (var entity = new StaffingModelContainer())
            {
                var items = entity.VacanceTable.Where(
                    o => !consultantId.HasValue || o.ConsultantTableID == consultantId).ToList();
                holidays = items.Where(o => o.Actif).Select(o => o.CreateHoliday()).ToList();
            }

            return holidays;
        }

        internal static Holiday AddHoliday(Holiday newHoliday)
        {
            using (var entity = new StaffingModelContainer())
            {
                var result = entity.VacanceTable.Add(newHoliday.CreateDatabaseHoliday());
                entity.SaveChanges();

                return result.CreateHoliday();
            }
        }

        public static Holiday UpdateHoliday(Holiday holiday)
        {
            using (var entity = new StaffingModelContainer())
            {
                entity.VacanceTable.Single(o => o.ID == holiday.Id).Update(holiday);
                entity.SaveChanges();

                holiday = entity.VacanceTable.Single(o => o.ID == holiday.Id).CreateHoliday();
            }

            switch (holiday.Status)
            {
                case ItemStatus.Waiting:
                    Mail.Send(holiday.ConsultantId, MailType.HolidayRequest, holiday);
                    break;
                case ItemStatus.Refused:
                    Mail.Send(holiday.ConsultantId, MailType.HolidayRefused, holiday);
                    break;
                case ItemStatus.Validate:
                    Mail.Send(holiday.ConsultantId, MailType.HolidayValidated, holiday);
                    break;
            }

            return holiday;
        }

        public static void DeleteHoliday(Holiday holiday)
        {
            using (var entity = new StaffingModelContainer())
            {
                entity.VacanceTable.Single(o => o.ID == holiday.Id).Actif = false;
                entity.SaveChanges();
            }
        }
    }
}