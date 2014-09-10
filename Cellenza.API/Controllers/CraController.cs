using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Cellenza.Service.Business;

namespace Crallenza.Areas.api.Controllers
{
    public class CraController : ApiController
    {
        public string Get(int year, int month, string user)
        {
            return CraBl.GetCra(year, month, user);
        }
    }
}
