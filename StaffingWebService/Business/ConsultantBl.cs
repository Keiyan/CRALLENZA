using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cellenza.Service.Data;
using Cellenza.Service.Model;

namespace Cellenza.Service.Business
{
    public class ConsultantBl
    {
        public static Consultant GetConsultantByName(string name)
        {
            return ConsultantDAL.GetConsultantByName(name);
        }
    }
}