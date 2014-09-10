using System;

namespace StaffingWebService.Model
{
    public class Mission
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ClientId { get; set; }
        public int ConsultantId { get; set; }
        public DateTime? DateDebut { get; set; }
        public DateTime? DateFin { get; set; }
        public string ReferenceFacturation { get; set; }
        public DateTime? DateFacturation { get; set; }
        public int? Facturation { get; set; }
        public string Image { get; set; }
        public bool IsActive { get; set; }
        public string InterlocuteurEmail { get; set; }
        public string InterlocuteurNom { get; set; }
        public string InterlocuteurTelephone { get; set; }

        public Mission()
        {
            Id = 0;
            Title = string.Empty;
            ClientId = 0;
            ConsultantId = 0;
            ReferenceFacturation = string.Empty;
            Facturation = null;
            Image = string.Empty;
            InterlocuteurEmail = string.Empty;
            InterlocuteurNom = string.Empty;
            InterlocuteurTelephone = string.Empty;
        }
    }
}
