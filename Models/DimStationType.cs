//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ATOMv0.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class DimStationType
    {
        public int id { get; set; }
        public string StationTypeName { get; set; }
        public Nullable<int> KeyBucket { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<int> FFInstanceID { get; set; }
        public Nullable<int> Sequence { get; set; }
    }
}
