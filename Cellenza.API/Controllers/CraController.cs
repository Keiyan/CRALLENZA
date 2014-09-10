using Cellenza.Service.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Cellenza.API.Areas.CRAApi
{
    public class CraController : ApiController
    {
        public string Get(int? year = null, int? month = null, string user = null)
        {
            return CraBl.GetCra(year, month, user);
        }

        [HttpPost]
        public bool Save()
        {
            return false;
        }
    }
}
