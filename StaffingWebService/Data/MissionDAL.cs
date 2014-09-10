using System.Collections.Generic;
using System.Linq;
using Cellenza.Service.Data.DataExtention;
using Cellenza.Service.Model;

namespace Cellenza.Service.Data
{
    public class MissionDAL
    {
        public static IEnumerable<Mission> GetMissions()
        {
            IEnumerable<Mission> missions;
            using (var entity = new StaffingModelContainer())
            {
                var items = entity.MissionTable.ToList();
                missions = items.OrderByDescending(o => o.Actif).Select(o => o.CreateApplicationMission()).ToList();
            }

            return missions;
        }

        public static IEnumerable<Mission> AddMission(Mission newMission)
        {
            using (var entity = new StaffingModelContainer())
            {
                entity.MissionTable.Add(newMission.CreateDatabaseMission());
                entity.SaveChanges();
            }

            return GetMissions();
        }

        public static IEnumerable<Mission> UpdateMission(Mission mission)
        {
            using (var entity = new StaffingModelContainer())
            {
                entity.MissionTable.Single(o => o.ID == mission.Id).Update(mission);
                entity.SaveChanges();
            }

            return GetMissions();
        }

        public static IEnumerable<Mission> DeleteMission(Mission mission)
        {
            using (var entity = new StaffingModelContainer())
            {
                entity.MissionTable.Single(o => o.ID == mission.Id).Actif = false;
                entity.SaveChanges();
            }

            return GetMissions();
        }

        public static Mission GetMission(int missionId)
        {
            using (var entity = new StaffingModelContainer())
            {
                return entity.MissionTable.Single(o => o.ID == missionId).CreateApplicationMission();
            }
        }
    }
}