using System;
using System.Linq;
using System.Security.Policy;
using Cellenza.Service.Data.DataExtention;
using Cellenza.Service.Model;
using System.Collections.Generic;

namespace Cellenza.Service.Data
{
    public class ConsultantDAL
    {
        public static IEnumerable<Consultant> GetConsultants()
        {
            IEnumerable<Consultant> consultants;
            using (var entity = new StaffingModelContainer())
            {
                var items = entity.ConsultantTable.ToList();
                consultants = items.OrderByDescending(o => o.Actif).Select(o => o.CreateApplicationConsultant()).ToList();
            }

            return consultants;
        }

        public static IEnumerable<Consultant> AddConsultant(Consultant newConsultant)
        {
            using (var entity = new StaffingModelContainer())
            {
                entity.ConsultantTable.Add(newConsultant.CreateDatabaseConsultant());
                entity.SaveChanges();
            }

            return GetConsultants();
        }

        public static IEnumerable<Consultant> UpdateConsultant(Consultant consultant)
        {
            using (var entity = new StaffingModelContainer())
            {
                entity.ConsultantTable.Single(o => o.ID == consultant.Id).Update(consultant);
                entity.SaveChanges();
            }

            return GetConsultants();
        }

        public static IEnumerable<Consultant> DeleteConsultant(Consultant consultant)
        {
            using (var entity = new StaffingModelContainer())
            {
                entity.ConsultantTable.Single(o => o.ID == consultant.Id).Actif = false;
                entity.SaveChanges();
            }

            return GetConsultants();
        }

        public static Consultant GetConsultant(int consultantId)
        {
            using (var entity = new StaffingModelContainer())
            {
                var elt = entity.ConsultantTable.Single(o => o.ID == consultantId && o.Actif);
                return elt.CreateApplicationConsultant();
            }
        }

        public static Consultant GetConsultant(string liveEmail)
        {
            using (var entity = new StaffingModelContainer())
            {
                var elt = entity.ConsultantTable.Single(o => o.LiveEmail == liveEmail && o.Actif);
                return elt.CreateApplicationConsultant();
            }
        }

        public static Consultant GetConsultantByName(string name)
        {
            using (var entity = new StaffingModelContainer())
            {
                var elt = entity.ConsultantTable.SingleOrDefault(o => o.Nom.ToLower() == name.ToLower() && o.Actif);
                return elt != null ? elt.CreateApplicationConsultant() : null;
            }
        }
    }
}