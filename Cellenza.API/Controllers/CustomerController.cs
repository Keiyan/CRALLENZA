using Cellenza.Service;
using Cellenza.Service.Data;
using Cellenza.Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Cellenza.API.Controllers
{
    public class CustomerController : CellenzApiControllerBase
    {
        public IEnumerable<Mission> AddMission(Mission mission, string token)
        {
            if (!IsConnected(token))
            {
                throw new NotConnectedException();
            }

            return MissionDAL.AddMission(mission);
        }

        public IEnumerable<Mission> UpdateMission(Mission mission, string token)
        {
            if (!IsConnected(token))
            {
                throw new NotConnectedException();
            }

            return MissionDAL.UpdateMission(mission);
        }

        public IEnumerable<Mission> DeleteMission(Mission mission, string token)
        {
            if (!IsConnected(token))
            {
                throw new NotConnectedException();
            }

            return MissionDAL.DeleteMission(mission);
        }

        public IEnumerable<Mission> GetMissions(string token)
        {
            if (!IsConnected(token))
            {
                throw new NotConnectedException();
            }

            return MissionDAL.GetMissions();
        }
    }
}
