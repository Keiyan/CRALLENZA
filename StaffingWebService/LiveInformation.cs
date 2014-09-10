using System;

namespace Cellenza.Service
{
    public class LiveInformation
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Link { get; set; }
        public string Gender { get; set; }
        public LiveEmails Emails { get; set; }
        public string Locale{ get; set; }
        public DateTime UpdatedTime{ get; set; }
    }
}