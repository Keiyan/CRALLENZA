<<<<<<< HEAD
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
            public string StartDate { get; set; }
            public string EndDate { get; set; }
        }

        [HttpPost]
        public bool Save()
        {
            Request.Content.ReadAsStringAsync().ContinueWith(s=>
                {
                    var data = JsonConvert.DeserializeObject<PostData>(s);
                    CraBl.CreateActivities();
                }
                );
            return false;
        }
    }
}
=======
using Cellenza.Service.Business;
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
        public string Get(int? year = null, int? month = null, string user = null)
        {
            //CraBl.CreateActivities(new DateTime(2014, 08, 01), new DateTime(2014, 08, 31), "test", "pamphile");
            return CraBl.GetCra(year, month, user);
        }

        public class PostData
        {
            public string Activity { get; set; }
            public string StartDate { get; set; }
            public string EndDate { get; set; }
        }

        [HttpPost]
        public bool Save(int year
            , int month
            , int user
            , PostData data)
        {
            return false;
        }
    }
}
>>>>>>> cc586b0d5d6f789858821f7a9bdcc800fe2a4e87
