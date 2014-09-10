using Cellenza.Service.Model;

namespace Cellenza.Service.Data.DataExtention
{
    internal static class MissionDataExtention
    {
        internal static Mission CreateApplicationMission(this MissionTable missionTable)
        {
            return new Mission
            {
                ClientId = missionTable.ClientTableID,
                ConsultantId = missionTable.ConsultantTableID,
                DateDebut = missionTable.DateDebut,
                DateFacturation = missionTable.DateFacturation,
                DateFin = missionTable.DateFin,
                Facturation = missionTable.Facturation,
                Id = missionTable.ID,
                Image = missionTable.Image,
                Title = missionTable.Intitule,
                ReferenceFacturation = missionTable.ReferenceFacturation,
                IsActive = missionTable.Actif,
                InterlocuteurEmail = missionTable.InterlocuteurEmail,
                InterlocuteurNom = missionTable.InterlocuteurNom,
                InterlocuteurTelephone = missionTable.InterlocuteurTelephone
            };
        }

        internal static void Update(this MissionTable missionTable, Mission mission)
        {
            missionTable.ClientTableID = mission.ClientId;
            missionTable.ConsultantTableID = mission.ConsultantId;
            missionTable.DateDebut = mission.DateDebut;
            missionTable.DateFacturation = mission.DateFacturation;
            missionTable.DateFin = mission.DateFin;
            missionTable.Facturation = mission.Facturation;
            missionTable.ID = mission.Id;
            missionTable.Image = mission.Image;
            missionTable.Intitule = mission.Title;
            missionTable.ReferenceFacturation = mission.ReferenceFacturation;
            missionTable.Actif = mission.IsActive;
            missionTable.InterlocuteurEmail = mission.InterlocuteurEmail;
            missionTable.InterlocuteurNom = mission.InterlocuteurNom;
            missionTable.InterlocuteurTelephone = mission.InterlocuteurTelephone;
        }

        internal static MissionTable CreateDatabaseMission(this Mission mission)
        {
            return new MissionTable
            {
                ClientTableID = mission.ClientId,
                ConsultantTableID = mission.ConsultantId,
                DateDebut = mission.DateDebut,
                DateFacturation = mission.DateFacturation,
                DateFin = mission.DateFin,
                Facturation = mission.Facturation,
                ID = mission.Id,
                Image = mission.Image,
                Intitule = mission.Title,
                ReferenceFacturation = mission.ReferenceFacturation,
                Actif = mission.IsActive,
                InterlocuteurEmail = mission.InterlocuteurEmail,
                InterlocuteurNom = mission.InterlocuteurNom,
                InterlocuteurTelephone = mission.InterlocuteurTelephone
            };
        }
    }
}