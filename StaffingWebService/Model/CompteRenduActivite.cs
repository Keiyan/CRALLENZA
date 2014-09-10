using System.Collections.Generic;

namespace Cellenza.Service.Model
{
    public class CompteRenduActivite
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int ConsultantId { get; set; }
        public ItemStatus Status { get; set; }
        public List<ActivityWeek> ActivityWeeks { get; set; }
    }
}