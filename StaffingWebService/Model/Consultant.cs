using System;

namespace StaffingWebService.Model
{
    public class Consultant
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Poste { get; set; }
        public DateTime? DateNaissance { get; set; }
        public string NoSecuriteSociale { get; set; }
        public string Image { get; set; }
        public string Email { get; set; }
        public DateTime? DateArrivee { get; set; }
        public DateTime? DateDepart { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsActive { get; set; }
        public string AdresseCodePostal { get; set; }
        public string AdresseComplement { get; set; }
        public string AdresseRue { get; set; }
        public string AdresseVille { get; set; }

        public Consultant()
        {
            Id = 0;
            Nom = string.Empty;
            Prenom = string.Empty;
            Poste = string.Empty;
            NoSecuriteSociale = string.Empty;
            Email = string.Empty;
            AdresseCodePostal = string.Empty;
            AdresseComplement = string.Empty;
            AdresseRue = string.Empty;
            AdresseVille = string.Empty;
        }
    }
}
