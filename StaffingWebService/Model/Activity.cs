using System;

namespace Cellenza.Service.Model
{
    public class Activity
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int ConsultantId { get; set; }
        public int? MissionId { get; set; }
        public ActivityType ActivityType { get; set; }
        public string Comment { get; set; }
        public bool IsSelected { get; set; }
    }
}
