using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.ServiceModel.Activation;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using StaffingWebService.Data;
using StaffingWebService.Model;

namespace StaffingWebService
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    public class StaffingWS : IStaffingWS
    {
        //private string APPLICATION_KEY = "TO COMPLETE!!!";

        private bool IsConnected(string token)
        {
            return HttpContext.Current.Application.AllKeys.Any(o => o == token);
        }

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

        public IEnumerable<Client> AddClient(Client client, string token)
        {
            if (!IsConnected(token))
            {
                throw new NotConnectedException();
            }

            return ClientDAL.AddClient(client);
        }

        public IEnumerable<Client> UpdateClient(Client client, string token)
        {
            if (!IsConnected(token))
            {
                throw new NotConnectedException();
            }

            return ClientDAL.UpdateClient(client);
        }

        public IEnumerable<Client> DeleteClient(Client client, string token)
        {
            if (!IsConnected(token))
            {
                throw new NotConnectedException();
            }

            return ClientDAL.DeleteClient(client);
        }

        public IEnumerable<Client> GetClients(string token)
        {
            if (!IsConnected(token))
            {
                throw new NotConnectedException();
            }

            return ClientDAL.GetClients();
        }

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

        public IEnumerable<CompteRenduActivite> GetCRA(int? consultantId, int? year, int? month, string token)
        {
            if (!IsConnected(token))
            {
                throw new NotConnectedException();
            }

            return ActivityDAL.GetCRA(consultantId, year, month);
        }

        public IEnumerable<ActivityWeek> GetActivities(int craId, string token)
        {
            if (!IsConnected(token))
            {
                throw new NotConnectedException();
            }

            return ActivityDAL.GetActivities(craId);
        }

        public void UpdateActivities(List<ActivityDay> weeks, string token)
        {
            if (!IsConnected(token))
            {
                throw new NotConnectedException();
            }

            ActivityDAL.UpdateActivities(weeks);
        }

        public CompteRenduActivite CreateCRA(int consultantId, int year, int month, string token)
        {
            if (!IsConnected(token))
            {
                throw new NotConnectedException();
            }

            return ActivityDAL.CreateCRA(consultantId, year, month);
        }

        public Holiday AddHoliday(Holiday holiday, string token)
        {
            if (!IsConnected(token))
            {
                throw new NotConnectedException();
            }

            return HolidayDAL.AddHoliday(holiday);
        }

        public Holiday UpdateHoliday(Holiday holiday, string token)
        {
            if (!IsConnected(token))
            {
                throw new NotConnectedException();
            }

            return HolidayDAL.UpdateHoliday(holiday);
        }

        public void DeleteHoliday(Holiday holiday, string token)
        {
            if (!IsConnected(token))
            {
                throw new NotConnectedException();
            }

            HolidayDAL.DeleteHoliday(holiday);
        }

        public IEnumerable<Holiday> GetHolidays(int? consultantId, string token)
        {
            if (!IsConnected(token))
            {
                throw new NotConnectedException();
            }

            return HolidayDAL.GetHolidays(consultantId);
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
                    throw new ConnectionErrorException(String.Format("User not found for user {0}.", email));
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

            LiveInformation infos = await JsonConvert.DeserializeObjectAsync<LiveInformation>(response);

            return infos.Emails.Account;
        }

        public IEnumerable<CompteRenduActivite> UpdateCRA(CompteRenduActivite compteRenduActivite, string token)
        {
            if (!IsConnected(token))
            {
                throw new NotConnectedException();
            }
            CRAPDFGenerator.GeneratePDF(compteRenduActivite);
            var cra = ActivityDAL.UpdateCRA(compteRenduActivite);


            return cra;
        }

        public void CRARequest(string token)
        {
            Mail.Send(null, MailType.CRARequest, null);
        }

        ///// <summary>
        ///// Vérifie la valididé Token et récupère les informations de l'utilisateur
        ///// </summary>
        ///// <param name="accessToken"></param>
        ///// <param name="authenticationToken"></param>
        ///// <returns></returns>
        //private bool AuthenticateLive(string accessToken, string authenticationToken)
        //{
        //    if (!ValidateToken(authenticationToken))
        //    {
        //        return false;
        //    }

        //    try
        //    {
        //        string liveUri = String.Format("https://apis.live.net/v5.0/me?access_token={0}", accessToken);
        //        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(liveUri);
        //        request.Method = "GET";
        //        var response = (HttpWebResponse)request.GetResponse();
        //        using (var reader = new StreamReader(response.GetResponseStream()))
        //        {
        //            string json = reader.ReadToEnd();
        //            var user = JsonConvert.DeserializeObject<User>(json);
        //            if (IsAuthorizedUser(user))
        //            {
        //                HttpContext.Current.Application[accessToken]= new TokenInfo(user);
        //                return true;
        //            }
        //            else
        //                return false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        return false;
        //    }
        //}

        ///// <summary>
        ///// Vérifie si l'utilisateur est autorisé
        ///// </summary>
        ///// <param name="user"></param>
        ///// <returns></returns>
        //private bool IsAuthorizedUser(User user)
        //{
        //    throw new NotImplementedException();
        //}

        ///// <summary>
        ///// Valide la structure du Token par rapport à la clé de l'application 
        ///// </summary>
        ///// <param name="auth_token">authenticationToken du Token Json Live</param>
        ///// <returns></returns>
        //private bool ValidateToken(string auth_token)
        //{
        //    var d = new Dictionary<int, string>();
        //    d.Add(0, APPLICATION_KEY);

        //    try
        //    {
        //        var myJWT = new JsonWebToken(auth_token, d);
        //        string myToken = myJWT.Envelope.ToString();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.Write(ex.Message);
        //        return false;
        //    }
        //}

    }
}
