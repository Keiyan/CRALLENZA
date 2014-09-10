using System;

namespace Cellenza.Service.Model
{
    public class Holiday
    {
        public int Id { get; set; }
        public int ConsultantId { get; set; }
        public HolidayType Type { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }
        public string Motif { get; set; }
        public string Commentaire { get; set; }
        public ItemStatus Status { get; set; }
        public bool IsActive { get; set; }
    }
}