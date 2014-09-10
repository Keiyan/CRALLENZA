using System.Collections.Generic;

namespace Cellenza.Service.Model
{
    public class ActivityMonth
    {
        public int MonthNumber { get; set; }
        public List<ActivityDay> Activities { get; set; }

        public ActivityMonth()
        {
            Activities=new List<ActivityDay>();
        }
    }
}