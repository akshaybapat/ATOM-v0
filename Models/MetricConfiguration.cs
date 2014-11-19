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
    
    public partial class MetricConfiguration
    {
        public int id { get; set; }
        public Nullable<int> KeyMetricID { get; set; }
        public Nullable<int> KeyMetricRoleTypeID { get; set; }
        public Nullable<int> KeySiteID { get; set; }
        public Nullable<int> KeyBusinessPartnerID { get; set; }
        public Nullable<int> KeyFFInstanceID { get; set; }
        public string Goal { get; set; }
        public string Red { get; set; }
        public string Green { get; set; }
        public string Alert { get; set; }
        public string Alert_MasterDataChange { get; set; }
        public string Alert_SystemErrors { get; set; }
        public string MetricManagerValidationStatus { get; set; }
        public string MetricOwnerValidationStatus { get; set; }
        public string Status { get; set; }
        public string MetricManager { get; set; }
        public string MetricOwner { get; set; }
    
        public virtual DimBusinessPartner DimBusinessPartner { get; set; }
        public virtual DimFacility DimFacility { get; set; }
        public virtual DimFFInstance DimFFInstance { get; set; }
        public virtual Metric Metric { get; set; }
        public virtual MetricRoleType MetricRoleType { get; set; }
    }
}