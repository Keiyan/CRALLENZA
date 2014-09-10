using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using Cellenza.Service.Model;

namespace Cellenza.Service
{
    [ServiceContract]
    public interface IStaffingWS
    {
        [OperationContract]
        Consultant ConnectUser(string token);

        [OperationContract]
        IEnumerable<Consultant> AddConsultant(Consultant consultant, string token);

        [OperationContract]
        IEnumerable<Consultant> UpdateConsultant(Consultant consultant, string token);

        [OperationContract]
        IEnumerable<Consultant> DeleteConsultant(Consultant consultant, string token);

        [OperationContract]
        IEnumerable<Consultant> GetConsultants(string token);

        [OperationContract]
        IEnumerable<Client> AddClient(Client client, string token);

        [OperationContract]
        IEnumerable<Client> UpdateClient(Client client, string token);

        [OperationContract]
        IEnumerable<Client> DeleteClient(Client client, string token);

        [OperationContract]
        IEnumerable<Client> GetClients(string token);

        [OperationContract]
        IEnumerable<Mission> AddMission(Mission mission, string token);

        [OperationContract]
        IEnumerable<Mission> UpdateMission(Mission mission, string token);

        [OperationContract]
        IEnumerable<Mission> DeleteMission(Mission mission, string token);

        [OperationContract]
        IEnumerable<Mission> GetMissions(string token);

        [OperationContract]
        IEnumerable<CompteRenduActivite> GetCRA(int? consultantId, int? year, int? month, string token);

        [OperationContract]
        IEnumerable<CompteRenduActivite> UpdateCRA(CompteRenduActivite compteRenduActivite, string token);

        [OperationContract]
        CompteRenduActivite CreateCRA(int consultantId, int year, int month, string token);

        [OperationContract]
        IEnumerable<ActivityWeek> GetActivities(int craId, string token);

        [OperationContract]
        void UpdateActivities(List<ActivityDay> weeks, string token);

        [OperationContract]
        Holiday AddHoliday(Holiday holiday, string token);

        [OperationContract]
        Holiday UpdateHoliday(Holiday holiday, string token);

        [OperationContract]
        void DeleteHoliday(Holiday holiday, string token);

        [OperationContract]
        IEnumerable<Holiday> GetHolidays(int? consultantId, string token);

        [OperationContract]
        void CRARequest(string token);
    }
}
