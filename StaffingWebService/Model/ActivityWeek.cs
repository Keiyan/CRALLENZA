using System.Collections.Generic;

namespace Cellenza.Service.Model
{
    public class ActivityWeek
    {
        public int WeekNumber { get; set; }
        public List<ActivityDay> Activities { get; set; }

        public ActivityWeek()
        {
            Activities=new List<ActivityDay>();
        }
    }
}