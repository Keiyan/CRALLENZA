using System;
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

        public static string CreateActivities()
        {
            throw new NotImplementedException();
        }
    }
}