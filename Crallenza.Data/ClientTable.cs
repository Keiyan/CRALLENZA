//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace StaffingWebService.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class ClientTable
    {
        public ClientTable()
        {
            this.MissionTable = new HashSet<MissionTable>();
        }
    
        public int ID { get; set; }
        public string RaisonSociale { get; set; }
        public string CodeClient { get; set; }
        public string Image { get; set; }
        public bool Actif { get; set; }
    
        public virtual ICollection<MissionTable> MissionTable { get; set; }
    }
}
