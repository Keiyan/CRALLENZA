using System;
using System.Linq;
using Cellenza.Service.Data;
using Newtonsoft.Json;

namespace Cellenza.Service.Business
{
    public class CraBl
    {
        public static string GetCra(int? year, int? month, string user)
        {
            var consultant = ConsultantBl.GetConsultantByName(user);
            var cras = ActivityDal.GetCra(consultant != null ? (int?)consultant.Id : null, year, month);
            var result = JsonConvert.SerializeObject(cras);
            return result;
        }

        public static string CreateActivities(DateTime dateDebut, DateTime dateFin, string activite, string user)
        {
            var consultant = ConsultantBl.GetConsultantByName(user);
            var result = string.Empty;
            if (dateDebut.Year != dateFin.Year || dateDebut.Month != dateFin.Month || consultant == null)
            {
                return result;
            }

            var cra = ActivityDal.CreateCra(consultant.Id, dateDebut.Year, dateDebut.Month);
            var activities =
                cra.ActivityWeeks.SelectMany(o => o.Activities)
                    .Where(o => o.Date >= dateDebut && o.Date <= dateFin && o.IsWorkingDay)
                    .ToList();

            foreach (var activityDay in activities)
            {
                activityDay.MorningActivity.Comment = activite;
                activityDay.AfternoonActivity.Comment = activite;
            }

            ActivityDal.UpdateActivities(activities);

            var cras = ActivityDal.GetCra(consultant.Id, dateDebut.Year, dateDebut.Month);
            result = JsonConvert.SerializeObject(cras);

            return result;
        }
    }
}