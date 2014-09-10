using Cellenza.Service.Model;

namespace Cellenza.Service.Data.DataExtention
{
    internal static class ConsultantDataExtention
    {
        internal static Consultant CreateApplicationConsultant(this ConsultantTable consultantsTable)
        {
            return new Consultant
            {
                DateArrivee = consultantsTable.DateArrivee,
                DateNaissance = consultantsTable.DateNaissance,
                DateDepart = consultantsTable.DateDepart,
                Email = consultantsTable.Email,
                Id = consultantsTable.ID,
                NoSecuriteSociale = consultantsTable.NoSecu,
                Nom = consultantsTable.Nom,
                Prenom = consultantsTable.Prenom,
                Image = consultantsTable.Image,
                IsAdmin = consultantsTable.Admin,
                IsActive = consultantsTable.Actif,
                Poste = consultantsTable.Poste,
                AdresseCodePostal = consultantsTable.AdresseCodePostal,
                AdresseComplement = consultantsTable.AdresseComplement,
                AdresseRue = consultantsTable.AdresseRue,
                AdresseVille = consultantsTable.AdresseVille
            };
        }

        internal static void Update(this ConsultantTable consultantsTable, Consultant consultant)
        {
            consultantsTable.DateArrivee = consultant.DateArrivee;
            consultantsTable.DateNaissance = consultant.DateNaissance;
            consultantsTable.DateDepart = consultant.DateDepart;
            consultantsTable.Email = consultant.Email;
            consultantsTable.NoSecu = consultant.NoSecuriteSociale;
            consultantsTable.Nom = consultant.Nom;
            consultantsTable.Prenom = consultant.Prenom;
            consultantsTable.Image = consultant.Image;
            consultantsTable.Admin = consultant.IsAdmin;
            consultantsTable.Actif = consultant.IsActive;
            consultantsTable.Poste = consultant.Poste;
            consultantsTable.AdresseCodePostal = consultant.AdresseCodePostal;
            consultantsTable.AdresseComplement = consultant.AdresseComplement;
            consultantsTable.AdresseRue = consultant.AdresseRue;
            consultantsTable.AdresseVille = consultant.AdresseVille;
        }

        internal static ConsultantTable CreateDatabaseConsultant(this Consultant consultant)
        {
            return new ConsultantTable
                       {
                           DateArrivee = consultant.DateArrivee,
                           DateNaissance = consultant.DateNaissance,
                           DateDepart = consultant.DateDepart,
                           Email = consultant.Email,
                           NoSecu = consultant.NoSecuriteSociale,
                           Nom = consultant.Nom,
                           Prenom = consultant.Prenom,
                           Image = consultant.Image,
                           Actif = consultant.IsActive,
                           Admin = consultant.IsAdmin,
                           Poste = consultant.Poste,
                           AdresseCodePostal = consultant.AdresseCodePostal,
                           AdresseComplement = consultant.AdresseComplement,
                           AdresseRue = consultant.AdresseRue,
                           AdresseVille = consultant.AdresseVille
                       };
        }
    }
}