using Cellenza.Service.Business;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Cellenza.API.Controllers
{
    public class CraController : CellenzApiControllerBase
    {
        [HttpGet]
        public string Get(int? year = null, int? month = null, string user = null)
        {
            return CraBl.GetCra(year, month, user);
        }

        public class PostData
        {
            public string Activity { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
        }

        [HttpPost]
        public bool Save(int year, int month, string user)
        {
            Request.Content.ReadAsStringAsync().ContinueWith(s=>
                {
                    var data = JsonConvert.DeserializeObject<PostData>(s.Result);
                    CraBl.CreateActivities(data.StartDate, data.EndDate, data.Activity, user);
                }
                );
            return false;
        }
    }
}
