﻿using System.Linq;
using StaffingWebService.Data.DataExtention;
using StaffingWebService.Model;
using System.Collections.Generic;

namespace StaffingWebService.Data
{
    internal class ConsultantDAL
    {
        internal static IEnumerable<Consultant> GetConsultants()
        {
            IEnumerable<Consultant> consultants;
            using (var entity = new StaffingModelContainer())
            {
                var items = entity.ConsultantTable.ToList();
                consultants = items.OrderByDescending(o => o.Actif).Select(o => o.CreateApplicationConsultant()).ToList();
            }

            return consultants;
        }

        internal static IEnumerable<Consultant> AddConsultant(Consultant newConsultant)
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
    }
}