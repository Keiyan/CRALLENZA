using System;
using Cellenza.Service.Model;

namespace Cellenza.Service.Data.DataExtention
{
    internal static class ActivityDataExtention
    {
        internal static CompteRenduActivite CreateCompteRenduActivite(
            this CompteRenduActiviteTable compteRenduActiviteTable)
        {
            return new CompteRenduActivite
            {
                Id = compteRenduActiviteTable.ID,
                ConsultantId = compteRenduActiviteTable.ConsultantTableID,
                Month = compteRenduActiviteTable.Month,
                Year = compteRenduActiviteTable.Year,
                Status = (ItemStatus)compteRenduActiviteTable.Status
            };
        }

        internal static void Update(this CompteRenduActiviteTable oldCRA, CompteRenduActivite newCRA)
        {
            oldCRA.ConsultantTableID = newCRA.ConsultantId;
            oldCRA.Month = newCRA.Month;
            oldCRA.Status = (int)newCRA.Status;
            oldCRA.Year = newCRA.Year;
        }

        internal static ActiviteTable CreateDataActivity(this Activity activity, int craId, bool afternoon)
        {
            return new ActiviteTable
                       {
                           ID = activity.Id,
                           Afternoon = afternoon,
                           MissionTableID = activity.MissionId,
                           Comment = activity.Comment,
                           CompteRenduActiviteTableID = craId,
                           Day = activity.Date.Day,
                           Type = (int)activity.ActivityType
                       };
        }

        internal static void Update(this ActiviteTable activityTable, Activity activity)
        {
            activityTable.Comment = activity.Comment;
            activityTable.MissionTableID = activity.MissionId;
            activityTable.Type = (int)activity.ActivityType;
        }

        internal static Activity CreateActivity(this ActiviteTable activity, int consultantId, DateTime date)
        {
            return new Activity
                       {
                           Id = activity.ID,
                           ActivityType = (ActivityType)activity.Type,
                           Comment = activity.Comment,
                           ConsultantId = consultantId,
                           Date = date,
                           MissionId = activity.MissionTableID
                       };
        }

        public static string ConvertToFile(this Activity activity)
        {
            switch (activity.ActivityType)
            {
                case ActivityType.Mission:
                    return activity.MissionId != null
                               ? MissionDAL.GetMission(activity.MissionId.Value).Title
                               : string.Empty;
                case ActivityType.Bootcamp:
                    return "Bootcamp";
                case ActivityType.CP:
                    return "Congé Payé";
                case ActivityType.Intercontrat:
                    return "Intercontrat";
                case ActivityType.RTT_Employeur:
                    return "RTT Employeur";
                case ActivityType.RTT_Employe:
                    return "RTT Employé";
                case ActivityType.Autre:
                    return !string.IsNullOrEmpty(activity.Comment) ? activity.Comment : "Autre";
                default:
                    return string.Empty;
            }
        }

        internal static bool IsWorkingDay(this DateTime dtDate)
        {
            var arrDateFerie = new DateTime[8];

            // 01 Janvier
            arrDateFerie.SetValue(new DateTime(dtDate.Year, 1, 1), 0);
            // 01 Mai
            arrDateFerie.SetValue(new DateTime(dtDate.Year, 5, 1), 1);
            // 08 Mai
            arrDateFerie.SetValue(new DateTime(dtDate.Year, 5, 8), 2);
            // 14 Juillet
            arrDateFerie.SetValue(new DateTime(dtDate.Year, 7, 14), 3);
            // 15 Aout
            arrDateFerie.SetValue(new DateTime(dtDate.Year, 8, 15), 4);
            // 01 Novembre
            arrDateFerie.SetValue(new DateTime(dtDate.Year, 11, 1), 5);
            // 11 Novembre
            arrDateFerie.SetValue(new DateTime(dtDate.Year, 11, 11), 6);
            // Noël
            arrDateFerie.SetValue(new DateTime(dtDate.Year, 12, 25), 7);

            // Samedi dimanche ou jour férié
            var bolWorkingDay =
                !((dtDate.DayOfWeek == DayOfWeek.Saturday) || (dtDate.DayOfWeek == DayOfWeek.Sunday) ||
                  (Array.BinarySearch(arrDateFerie, dtDate) >= 0));
            if (bolWorkingDay)
            {
                // Calcul du jour de pâques (algorithme de Oudin (1940))
                //Calcul du nombre d'or - 1
                var intGoldNumber = dtDate.Year % 19;
                // Année divisé par cent
                var intAnneeDiv100 = dtDate.Year / 100;
                // intEpacte est = 23 - Epacte (modulo 30)
                var intEpacte = ((intAnneeDiv100 - intAnneeDiv100 / 4 - (8 * intAnneeDiv100 + 13) / 25 + (
                                                                                                       19 *
                                                                                                       intGoldNumber) +
                                  15) % 30);
                //Le nombre de jours à partir du 21 mars pour atteindre la pleine lune Pascale
                var intDaysEquinoxeToMoonFull =
                    (intEpacte - (intEpacte / 28) * (1 - (intEpacte / 28) * (29 / (intEpacte + 1)) * ((21 - intGoldNumber) / 11)));
                //Jour de la semaine pour la pleine lune Pascale (0=dimanche)
                var intWeekDayMoonFull = ((dtDate.Year + dtDate.Year / 4 + intDaysEquinoxeToMoonFull +
                                           2 - intAnneeDiv100 + intAnneeDiv100 / 4) % 7);
                // Nombre de jours du 21 mars jusqu'au dimanche de ou 
                // avant la pleine lune Pascale (un nombre entre -6 et 28)
                var intDaysEquinoxeBeforeFullMoon = intDaysEquinoxeToMoonFull - intWeekDayMoonFull;
                // mois de pâques
                var intMonthPaques = (3 + (intDaysEquinoxeBeforeFullMoon + 40) / 44);
                // jour de pâques
                var intDayPaques = (intDaysEquinoxeBeforeFullMoon + 28 - 31 * (intMonthPaques / 4));
                // lundi de pâques
                var dtMondayPaques = new DateTime(dtDate.Year, intMonthPaques, intDayPaques).AddDays(1);
                // Ascension
                var dtAscension = dtMondayPaques.AddDays(38);
                //Pentecote
                var dtMondayPentecote = dtMondayPaques.AddDays(49);
                bolWorkingDay =
                    !((DateTime.Compare(dtMondayPaques, dtDate) == 0) || (DateTime.Compare(dtAscension, dtDate) == 0)
                      || (DateTime.Compare(dtMondayPentecote, dtDate) == 0));
            }

            return bolWorkingDay;
        }
    }
}