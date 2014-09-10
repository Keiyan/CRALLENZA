using StaffingWebService.Model;

namespace StaffingWebService.Data.DataExtention
{
    internal static class ClientDataExtention
    {
        internal static Client CreateApplicationClient(this ClientTable clientTable)
        {
            return new Client
                       {
                           Id = clientTable.ID,
                           CodeClient = clientTable.CodeClient,
                           RaisonSociale = clientTable.RaisonSociale,
                           Image = clientTable.Image,
                           IsActive = clientTable.Actif,
                           //AdresseCodePostal = clientTable.AdresseCodePostal,
                           //AdresseComplement = clientTable.AdresseComplement,
                           //AdresseRue = clientTable.AdresseRue,
                           //AdresseVille = clientTable.AdresseVille,
                           //AdresseFacturationCodePostal = clientTable.AdresseFacturationCodePostal,
                           //AdresseFacturationComplement = clientTable.AdresseFacturationComplement,
                           //AdresseFacturationRue = clientTable.AdresseFacturationRue,
                           //AdresseFacturationVille = clientTable.AdresseFacturationVille,
                       };
        }

        internal static void Update(this ClientTable clientTable, Client client)
        {
            clientTable.CodeClient = client.CodeClient;
            clientTable.RaisonSociale = client.RaisonSociale;
            clientTable.Image = client.Image;
            clientTable.Actif = client.IsActive;
            //clientTable.AdresseCodePostal = client.AdresseCodePostal;
            //clientTable.AdresseComplement = client.AdresseComplement;
            //clientTable.AdresseRue = client.AdresseRue;
            //clientTable.AdresseVille = client.AdresseVille;
            //clientTable.AdresseFacturationCodePostal = client.AdresseFacturationCodePostal;
            //clientTable.AdresseFacturationComplement = client.AdresseFacturationComplement;
            //clientTable.AdresseFacturationRue = client.AdresseFacturationRue;
            //clientTable.AdresseFacturationVille = client.AdresseFacturationVille;
        }

        internal static ClientTable CreateDatabaseClient(this Client client)
        {
            return new ClientTable
                       {
                           CodeClient = client.CodeClient,
                           RaisonSociale = client.RaisonSociale,
                           Image = client.Image,
                           Actif = client.IsActive,
                           //AdresseCodePostal = client.AdresseCodePostal,
                           //AdresseComplement = client.AdresseComplement,
                           //AdresseRue = client.AdresseRue,
                           //AdresseVille = client.AdresseVille,
                           //AdresseFacturationCodePostal = client.AdresseFacturationCodePostal,
                           //AdresseFacturationComplement = client.AdresseFacturationComplement,
                           //AdresseFacturationRue = client.AdresseFacturationRue,
                           //AdresseFacturationVille = client.AdresseFacturationVille,
                       };
        }
    }
}