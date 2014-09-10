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