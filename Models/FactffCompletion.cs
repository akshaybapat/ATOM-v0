//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ATOMv0.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class FactffCompletion
    {
        public int id { get; set; }
        public Nullable<int> KeyStartTime { get; set; }
        public Nullable<int> KeyFFInstance { get; set; }
        public Nullable<int> OrderTotal { get; set; }
        public Nullable<int> InProgress { get; set; }
        public Nullable<int> Finished { get; set; }
        public Nullable<System.DateTime> DueDate { get; set; }
        public string Color { get; set; }
        public string ProductionOrder { get; set; }
        public string ProductionLine { get; set; }
    }
}
