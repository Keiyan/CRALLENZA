using System.Collections.Generic;
using System.Linq;
using StaffingWebService.Data.DataExtention;
using StaffingWebService.Model;

namespace StaffingWebService.Data
{
    internal class ClientDAL
    {
        internal static IEnumerable<Client> GetClients()
        {
            IEnumerable<Client> clients;
            using (var entity = new StaffingModelContainer())
            {
                var items = entity.ClientTable.ToList();
                clients = items.OrderByDescending(o => o.Actif).Select(o => o.CreateApplicationClient()).ToList();
            }

            return clients;
        }

        internal static IEnumerable<Client> AddClient(Client newClient)
        {
            using (var entity = new StaffingModelContainer())
            {
                entity.ClientTable.Add(newClient.CreateDatabaseClient());
                entity.SaveChanges();
            }

            return GetClients();
        }

        public static IEnumerable<Client> UpdateClient(Client client)
        {
            using (var entity = new StaffingModelContainer())
            {
                entity.ClientTable.Single(o => o.ID == client.Id).Update(client);
                entity.SaveChanges();
            }

            return GetClients();
        }

        public static IEnumerable<Client> DeleteClient(Client client)
        {
            using (var entity = new StaffingModelContainer())
            {
                entity.ClientTable.Single(o => o.ID == client.Id).Actif = false;
                entity.SaveChanges();
            }

            return GetClients();
        }
    }
}