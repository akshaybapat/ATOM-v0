using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATOMv0.Models
{
  public class BusinessPartnerCustom
  {
   
    public int id { get; set; }
    public string BusinessPartnerName { get; set; }
    public string BPCode { get; set; }
    public string SiteName { get; set; }
    public string BuildingName { get; set; }
    public int? IsActive { get; set; }
  }
}