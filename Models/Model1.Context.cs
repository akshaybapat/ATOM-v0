﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class FFCubeEntities : DbContext
    {
      public FFCubeEntities()
            : base("name=FFCube2Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<DimBucket> DimBuckets { get; set; }
        public virtual DbSet<DimBuilding> DimBuildings { get; set; }
        public virtual DbSet<DimBusinessUnit> DimBusinessUnits { get; set; }
        public virtual DbSet<DimCostCenter> DimCostCenters { get; set; }
        public virtual DbSet<DimCountry> DimCountries { get; set; }
        public virtual DbSet<DimDatetime> DimDatetimes { get; set; }
        public virtual DbSet<DimFacility> DimFacilities { get; set; }
        public virtual DbSet<DimFFInstance> DimFFInstances { get; set; }
        public virtual DbSet<DimFinancialCompany> DimFinancialCompanies { get; set; }
        public virtual DbSet<DimModule> DimModules { get; set; }
        public virtual DbSet<DimPhysicalStation> DimPhysicalStations { get; set; }
        public virtual DbSet<DimProcessType> DimProcessTypes { get; set; }
        public virtual DbSet<DimProductLine> DimProductLines { get; set; }
        public virtual DbSet<DimProductNumber> DimProductNumbers { get; set; }
        public virtual DbSet<DimRegion> DimRegions { get; set; }
        public virtual DbSet<DimStationType> DimStationTypes { get; set; }
        public virtual DbSet<FactffFirstPass> FactffFirstPasses { get; set; }
        public virtual DbSet<CISTestResultSet> CISTestResultSets { get; set; }
        public virtual DbSet<ffPart> ffParts { get; set; }
        public virtual DbSet<ffStation> ffStations { get; set; }
        public virtual DbSet<ffStationType> ffStationTypes { get; set; }
        public virtual DbSet<fsSite> fsSites { get; set; }
        public virtual DbSet<DataPollingExecutionTime> DataPollingExecutionTimes { get; set; }
        public virtual DbSet<FactffCompletion> FactffCompletions { get; set; }
        public virtual DbSet<FactffDefect> FactffDefects { get; set; }
        public virtual DbSet<RTYbyDay> RTYbyDays { get; set; }
        public virtual DbSet<SpeedMeasure> SpeedMeasures { get; set; }
        public virtual DbSet<Metric> Metrics { get; set; }
        public virtual DbSet<MetricRoleType> MetricRoleTypes { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<MetricConfiguration> MetricConfigurations { get; set; }
        public virtual DbSet<DimBusinessPartner> DimBusinessPartners { get; set; }
    
        public virtual ObjectResult<GetBusinessPartnerList_Result> GetBusinessPartnerList()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetBusinessPartnerList_Result>("GetBusinessPartnerList");
        }
    
        public virtual ObjectResult<GetBusinessPartnerListByID_Result> GetBusinessPartnerListByID(Nullable<int> bpID)
        {
            var bpIDParameter = bpID.HasValue ?
                new ObjectParameter("BpID", bpID) :
                new ObjectParameter("BpID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetBusinessPartnerListByID_Result>("GetBusinessPartnerListByID", bpIDParameter);
        }
    }
}
