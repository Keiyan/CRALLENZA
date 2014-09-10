using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Crallenza.Areas.api.Controllers
{
    public class CraController : ApiController
    {
        public IList<string> Get(int year, int month, string user)
        {
            return new List<string>(new string[] { "toto", "tata" });
        }
    }
}
