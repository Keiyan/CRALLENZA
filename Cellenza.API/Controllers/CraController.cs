<<<<<<< HEAD:Cellenza.API/Controllers/CraController.cs
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Cellenza.API.Areas.CRAApi
{
    public class CraController : ApiController
    {
        public IList<string> Get(int? year = null, int? month = null, string user = null)
        {
            return new List<string>(new string[] { string.Format("{0}", year), string.Format("{0}", month), string.Format("{0}", user) });
        }

        [HttpPost]
        public bool Save()
        {
            return false;
        }
    }
}
=======
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Cellenza.Service.Business;

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
>>>>>>> 014e11b96e166756cfb9b4eec343d6f2255e9402:Cellenza.API/Areas/CRAApi/Controllers/CraController.cs
