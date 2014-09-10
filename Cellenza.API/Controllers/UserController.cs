using Cellenza.Service;
using Cellenza.Service.Data;
using Cellenza.Service.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Cellenza.API.Controllers
{
    public class UserController : CellenzApiControllerBase
    {
        public IEnumerable<Consultant> AddConsultant(Consultant consultant, string token)
        {
            if (!IsConnected(token))
            {
                throw new NotConnectedException();
            }

            return ConsultantDAL.AddConsultant(consultant);
        }

        public IEnumerable<Consultant> UpdateConsultant(Consultant consultant, string token)
        {
            if (!IsConnected(token))
            {
                throw new NotConnectedException();
            }

            return ConsultantDAL.UpdateConsultant(consultant);
        }

        public IEnumerable<Consultant> DeleteConsultant(Consultant consultant, string token)
        {
            if (!IsConnected(token))
            {
                throw new NotConnectedException();
            }

            return ConsultantDAL.DeleteConsultant(consultant);
        }

        public IEnumerable<Consultant> GetConsultants(string token)
        {
            if (!IsConnected(token))
            {
                throw new NotConnectedException();
            }

            return ConsultantDAL.GetConsultants();
        }

        public Consultant ConnectUser(string token)
        {
            try
            {
                //Run synchronously to access HttpContext
                string email = GetLiveEmail(token).Result;
                var user = ConsultantDAL.GetConsultant(email);

                if (user == null)
                {
                    throw new ConnectionErrorException(string.Format("User not found for user {0}.", email));
                }

                HttpContext.Current.Application[token] = user;

                return user;
            }
            catch (Exception e)
            {
                throw new ConnectionErrorException(e.Message);
            }

        }

        private async Task<string> GetLiveEmail(string token)
        {
            string liveUri = string.Format("https://apis.live.net/v5.0/me?access_token={0}", token);

            var client = new HttpClient();
            HttpResponseMessage message = await client.GetAsync(liveUri);
            string response = await message.Content.ReadAsStringAsync();

            LiveInformation infos = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<LiveInformation>(response));
            return infos.Emails.Account;
        }
    }
}
