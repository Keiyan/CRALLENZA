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
    
    public partial class CompteRenduActiviteTable
    {
        public CompteRenduActiviteTable()
        {
            this.ActiviteTable = new HashSet<ActiviteTable>();
        }
    
        public int ID { get; set; }
        public int ConsultantTableID { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Status { get; set; }
    
        public virtual ICollection<ActiviteTable> ActiviteTable { get; set; }
        public virtual ConsultantTable ConsultantTable { get; set; }
    }
}