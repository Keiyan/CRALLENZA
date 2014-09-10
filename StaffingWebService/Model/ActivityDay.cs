using System;

namespace StaffingWebService.Model
{
    public class ActivityDay
    {
        public DateTime Date { get; set; }
        public int ConsultantId { get; set; }
        public bool IsWorkingDay { get; set; }

        public Activity MorningActivity { get; set; }
        public Activity AfternoonActivity { get; set; }
    }
}
