using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Net.Http;
using System.Data.SqlClient;

namespace ATOMv0.Models
{
  public class DimBusinessPartnersController : Controller
  {
    private FFCubeEntities db = new FFCubeEntities();

    // GET: DimBusinessPartners
    public ActionResult Index(string sortOrder, string currentFilter, string columnFilter, string searchString, int? page)
    {
      if (Session["UserName"] != null)
      {
        ViewBag.UserName = Session["UserName"];
      }
      ViewBag.ColumnFilter = (columnFilter != null) ? columnFilter : "ALL";

      ViewBag.CurrentSort = sortOrder;

      if (searchString != null)
      {
        page = 1;
      }
      else
      {
        searchString = currentFilter;
      }

      ViewBag.CurrentFilter = searchString;



      var contacts = db.Database.SqlQuery<GetBusinessPartnerList_Result>("GetBusinessPartnerList").AsQueryable();

      var dimBusinessPartners = db.DimBusinessPartners.AsQueryable();

      ViewBag.BPCodeList = db.DimBusinessPartners.Select(b => b.BPCode).Distinct();

      if (!String.IsNullOrEmpty(searchString))
      {
        dimBusinessPartners = dimBusinessPartners.Where(s => s.BusinessPartnerName.ToUpper().Contains(searchString.ToUpper()) || s.BPCode.ToUpper().Contains(searchString.ToUpper()));
        contacts = contacts.Where(s => s.BusinessPartnerName.ToUpper().Contains(searchString.ToUpper()) || s.BPCode.ToUpper().Contains(searchString.ToUpper()));


      }

      if (!String.IsNullOrEmpty(columnFilter) && !columnFilter.Equals("ALL"))
      {

        dimBusinessPartners = dimBusinessPartners.Where(s => s.BPCode.ToUpper().Contains(columnFilter.ToUpper()));
        contacts = contacts.Where(s => s.BPCode.ToUpper().Contains(columnFilter.ToUpper()));
      }

      switch (sortOrder)
      {

        case "BPName_Desc":
          dimBusinessPartners = dimBusinessPartners.OrderByDescending(s => s.BusinessPartnerName);
          contacts = contacts.OrderByDescending(s => s.BusinessPartnerName);
          break;

        case "BPCode_Desc":
          dimBusinessPartners = dimBusinessPartners.OrderByDescending(s => s.BPCode);
          contacts = contacts.OrderByDescending(s => s.BPCode);
          break;

        case "BPCode":
          dimBusinessPartners = dimBusinessPartners.OrderBy(s => s.BPCode);
          contacts = contacts.OrderBy(s => s.BPCode);
          break;

        default:
          dimBusinessPartners = dimBusinessPartners.OrderBy(s => s.BusinessPartnerName);
          contacts = contacts.OrderBy(s => s.BusinessPartnerName);
          break;
      }
      contacts = contacts.OrderBy(s => s.BusinessPartnerName);
      List<BusinessPartnerCustom> bpList = new List<BusinessPartnerCustom>();
      foreach (var item in contacts)
      {

        BusinessPartnerCustom bp = new BusinessPartnerCustom { BPCode = item.BPCode, BusinessPartnerName = item.BusinessPartnerName, id = item.id, SiteName = item.SiteName, BuildingName = item.BuildingName, IsActive = item.IsActive };
        bpList.Add(bp);
      }
      int pageSize = 10;
      int pageNumber = (page ?? 1);
      return View(bpList.ToPagedList(pageNumber, pageSize));
      //return View(dimBusinessPartners.ToPagedList(pageNumber, pageSize));
    }

    // GET: DimBusinessPartners/Details/5
    public ActionResult Details(int? id)
    {
      if (Session["UserName"] != null)
      {
        ViewBag.UserName = Session["UserName"];
      }
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      //DimBusinessPartner dimBusinessPartner = db.DimBusinessPartners.Find(id);
      //if (dimBusinessPartner == null)
      //{
      //    return HttpNotFound();
      //}

      //var BpID = new SqlParameter("@BpID", id);

      var bpListById = db.Database.SqlQuery<GetBusinessPartnerListByID_Result>("GetBusinessPartnerListByID @BpID", new SqlParameter("@BpID", id)).ToList();

      BusinessPartnerCustom businessParnter = new BusinessPartnerCustom();
      foreach (var item in bpListById)
      {
        businessParnter.id = item.id;
        businessParnter.BPCode = item.BPCode;
        businessParnter.BuildingName = item.BuildingName;
        businessParnter.BusinessPartnerName = item.BusinessPartnerName;
        businessParnter.SiteName = item.SiteName;
        businessParnter.IsActive = item.IsActive;
      }

      return View(businessParnter);
    }

    // GET: DimBusinessPartners/Create
    public ActionResult Create()
    {
      if (Session["UserName"] != null)
      {
        ViewBag.UserName = Session["UserName"];
      }
      ViewBag.KeySite = new SelectList(db.DimFacilities, "id", "SiteName");
      ViewBag.KeyBuilding = new SelectList("", "id", "BuildingName");
      return View();
    }
    public JsonResult GetBuildingData(string filter)
    {
      var querybuildings = db.DimBuildings.Where(x => x.DimFacility.SiteName.Equals(filter)).ToList();
      IEnumerable<DimBuilding> buildings = querybuildings.Select(x => new DimBuilding { BuildingName = x.BuildingName, id = x.id });
      return Json(buildings, JsonRequestBehavior.AllowGet);
    }
    // POST: DimBusinessPartners/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create([Bind(Include = "id,BusinessPartnerName,BPCode,KeySite,IsActive,KeyBuilding")] DimBusinessPartner dimBusinessPartner, FormCollection collection)
    {

      if (string.IsNullOrEmpty(dimBusinessPartner.BusinessPartnerName))
      {
        ModelState.AddModelError("BusinessPartnerName", "Please Enter Business Partner Name");
      }
      if (string.IsNullOrEmpty(dimBusinessPartner.BPCode))
      {
        ModelState.AddModelError("BPCode", "Please Enter Business Partner Code");
      }
      if (string.IsNullOrEmpty(dimBusinessPartner.BPCode))
      {
        ModelState.AddModelError("BPCode", "Please Enter Business Partner Code");
      }

      if (dimBusinessPartner.KeySite == null)
      {
        ModelState.AddModelError("KeySite", "Please select at least one Site");
      }
      if (dimBusinessPartner.KeyBuilding == null)
      {
        ModelState.AddModelError("KeyBuilding", "Please select at least one Building");
      }
     

      if (Session["UserName"] != null)
      {
        ViewBag.UserName = Session["UserName"];
      }
      int siteID = 0;
      int buildingID = 0;
      if (collection["SiteNamesForBP"] != null && collection["SiteNamesForBP"] != "")
      {
        siteID = Convert.ToInt32(collection["SiteNamesForBP"]);
      }

      if (collection["buildingIDToCreate"] != null && collection["buildingIDToCreate"] != "Select a Building")
      {
        buildingID = Convert.ToInt32(collection["buildingIDToCreate"]);
      }

      if (ModelState.IsValid)
      {
        dimBusinessPartner.IsActive = 1;
        db.DimBusinessPartners.Add(dimBusinessPartner);
        db.SaveChanges();
        return RedirectToAction("Index");
      }
      
      ViewBag.KeySite = new SelectList(db.DimFacilities, "id", "SiteName");

      if( dimBusinessPartner.KeySite!=null)
      {
        var siteName = db.DimFacilities.Where(a => a.id == dimBusinessPartner.KeySite).Select(a => a.SiteName);
        string strSiteName = siteName.ToList()[0];
        var querybuildings = db.DimBuildings.Where(x => x.DimFacility.SiteName.Equals(strSiteName)).ToList();
        IEnumerable<DimBuilding> buildings = querybuildings.Select(x => new DimBuilding { BuildingName = x.BuildingName, id = x.id });
        ViewBag.KeyBuilding = new SelectList(buildings, "id", "BuildingName");
      }
      else
      {
        ViewBag.KeyBuilding = new SelectList("", "id", "BuildingName");
      }
     
      return View(dimBusinessPartner);
    }

    // GET: DimBusinessPartners/Edit/5
    public ActionResult Edit(int? id)
    {
      if (Session["UserName"] != null)
      {
        ViewBag.UserName = Session["UserName"];
      }
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      DimBusinessPartner dimBusinessPartner = db.DimBusinessPartners.Find(id);
      if (dimBusinessPartner == null)
      {
        return HttpNotFound();
      }
      return View(dimBusinessPartner);
    }

    // POST: DimBusinessPartners/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit([Bind(Include = "id,BusinessPartnerName,BPCode,KeySite,IsActive,KeyBuilding")] DimBusinessPartner dimBusinessPartner)
    {
      if (Session["UserName"] != null)
      {
        ViewBag.UserName = Session["UserName"];
      }


      if (string.IsNullOrEmpty(dimBusinessPartner.BusinessPartnerName))
      {
        ModelState.AddModelError("BusinessPartnerName", "Please Enter Business Partner Name");
      }
      if (string.IsNullOrEmpty(dimBusinessPartner.BPCode))
      {
        ModelState.AddModelError("BPCode", "Please Enter Business Partner Code");
      }


      if (ModelState.IsValid)
      {
        db.Entry(dimBusinessPartner).State = EntityState.Modified;
        db.SaveChanges();
        return RedirectToAction("Index");
      }
      return View(dimBusinessPartner);
    }

    // GET: DimBusinessPartners/Delete/5
    public ActionResult Delete(int? id)
    {
      if (Session["UserName"] != null)
      {
        ViewBag.UserName = Session["UserName"];
      }
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      DimBusinessPartner dimBusinessPartner = db.DimBusinessPartners.Find(id);
      if (dimBusinessPartner == null)
      {
        return HttpNotFound();
      }
      return View(dimBusinessPartner);
    }

    // POST: DimBusinessPartners/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id)
    {
      if (Session["UserName"] != null)
      {
        ViewBag.UserName = Session["UserName"];
      }
      DimBusinessPartner dimBusinessPartner = db.DimBusinessPartners.Find(id);
      db.DimBusinessPartners.Remove(dimBusinessPartner);
      db.SaveChanges();
      return RedirectToAction("Index");
    }


    // POST: /DimBusinessPartner/Update/
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost, ActionName("Update")]
    public ActionResult Update(DimFFInstanceAJAXModel ffInstanceAJAXModel)
    {
      if (Session["UserName"] != null)
      {
        ViewBag.UserName = Session["UserName"];
      }
      var Building = db.DimBuildings.Where(x => x.BuildingName.Equals(ffInstanceAJAXModel.buildingname));

      if (ffInstanceAJAXModel.assignedcustomers != null)
      {
        foreach (string ffinstance in ffInstanceAJAXModel.assignedcustomers)
        {
          System.Diagnostics.Debug.Write(ffinstance + '\n');

          var bpRow = db.DimBusinessPartners.Where(x => x.BPCode.Equals(ffinstance)).First();

          if (!bpRow.KeyBuilding.HasValue)
          {
            bpRow.KeyBuilding = Building.FirstOrDefault().id;
            System.Diagnostics.Debug.Write(bpRow.KeyBuilding);
          }
          db.SaveChanges();
          System.Diagnostics.Debug.Write("Assigned List Saved" + '\n');
        }
      }

      if (ffInstanceAJAXModel.availablecustomers != null)
      {
        foreach (string ffinstance in ffInstanceAJAXModel.availablecustomers)
        {
          var bpRow = db.DimBusinessPartners.Where(x => x.BPCode.Equals(ffinstance)).First();

          if (bpRow.KeyBuilding.HasValue)
          {
            bpRow.KeyBuilding = null;

            System.Diagnostics.Debug.Write(bpRow.KeyBuilding);

          }
          db.SaveChanges();
        }
      }
      return RedirectToAction("Index");
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        db.Dispose();
      }
      base.Dispose(disposing);
    }
  }
}
