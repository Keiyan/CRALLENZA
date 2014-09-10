namespace StaffingWebService.Model
{
    public class Client
    {
        public int Id { get; set; }
        public string RaisonSociale { get; set; }
        public string CodeClient { get; set; }
        public string Image { get; set; }
        public bool IsActive { get; set; }
        public string AdresseCodePostal { get; set; }
        public string AdresseComplement { get; set; }
        public string AdresseRue { get; set; }
        public string AdresseVille { get; set; }
        public string AdresseFacturationCodePostal { get; set; }
        public string AdresseFacturationComplement { get; set; }
        public string AdresseFacturationRue { get; set; }
        public string AdresseFacturationVille { get; set; }

        public Client()
        {
            Id = 0;
            RaisonSociale = string.Empty;
            CodeClient = string.Empty;
            AdresseCodePostal = string.Empty;
            AdresseComplement = string.Empty;
            AdresseRue = string.Empty;
            AdresseVille = string.Empty;
            AdresseFacturationCodePostal = string.Empty;
            AdresseFacturationComplement = string.Empty;
            AdresseFacturationRue = string.Empty;
            AdresseFacturationVille = string.Empty;
        }
    }
}
