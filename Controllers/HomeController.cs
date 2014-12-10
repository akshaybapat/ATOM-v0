using ATOMv0.Models;
using Flextronics.QMSCC.Commons.SystemIntegrations.FlexWare.AuthenticationServices;
using Flextronics.QMSCC.Commons.SystemIntegrations.FlexWare.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ATOMv0.Controllers
{
  public class HomeController : Controller
  {
    bool isValidUser = false;
    [AuthorizeEnum(RolesEnum.Roles.AtomAdministrator, RolesEnum.Roles.SiteAdministrator)]
    public ActionResult Index()
    {
      ViewBag.Title = "Home Page";

      //userrolesws userRoles = (userrolesws)TempData["Roles"];
      userrolesws userRoles = (userrolesws)Session["Roles"];


      if (userRoles != null)
      {

        //if (userRoles.roles[0].name == AuthorizeEnumAttribute.GetEnumDescription(RolesEnum.Roles.AtomAdministrator))
        //{

        //  return RedirectToAction("Index", "FFSite");

        //}
        //else if (userRoles.roles[0].name == AuthorizeEnumAttribute.GetEnumDescription(RolesEnum.Roles.SiteAdministrator))
        //{
        //  return RedirectToAction("Index", "FFSite");
        //}
        //else
        //{
        //  return RedirectToAction("Login");
        //}

        if (userRoles.roles.Count() > 0)
        {
          foreach (var item in userRoles.roles)
          {
            if (item.name == AuthorizeEnumAttribute.GetEnumDescription(RolesEnum.Roles.AtomAdministrator))
            {
              ViewBag.AtomAdmin = true;
            }
            else if (item.name == AuthorizeEnumAttribute.GetEnumDescription(RolesEnum.Roles.SiteAdministrator))
            {
              ViewBag.SiteAdmin = true;
            }
          }
        }
        return View("LandingPage");
      }
      else
      {
        return RedirectToAction("Login");
      }

    }
    [AuthorizeEnum(RolesEnum.Roles.AtomAdministrator)]
    public ActionResult DimNavigation()
    {
      return View();
    }

    public ActionResult LandingAction()
    {
      return RedirectToAction("Index", "FFSite");
    }

    public ActionResult About()
    {
      ViewBag.Message = "Your application description page.";

      return View();
    }

    public ActionResult Contact()
    {
      ViewBag.Message = "Your contact page.";

      return View();
    }
    [AllowAnonymous]
    public ActionResult LoginRedirect()
    {
      return RedirectToAction("Login");
    }
    [AllowAnonymous]
    public ActionResult Login()
    {
      return View();
    }
    [HttpPost]
    public ActionResult Login(LoginModel model)
    {
      isValidUser = ValidateUser(model);
      if (isValidUser)
      {
        return RedirectToAction("Index");

      }

      else
      {
        ModelState.AddModelError("Error", "Please enter valid user name or password");
        return View();
      }

    }
    public ActionResult Logout()
    {
      if (Request.IsAuthenticated)
      {
        FormsAuthentication.SignOut();
        ClearSessions();
      }

      return RedirectToAction("Login");
    }

    private void ClearSessions()
    {
      if (Session["Roles"] != null)
      {
        Session["Roles"] = null;
      }
      if (Session["SiteName"] != null)
      {
        Session["SiteName"] = null;
      }
    }

    private bool ValidateUser(LoginModel model)
    {
      try
      {
        bool isValidUser = false;
        string Roleserrors = string.Empty;
        string MasterDataErrors = string.Empty;
        string url = ConfigurationManager.AppSettings["FlexwareUrlQA"];
        string strSolutionCode = Convert.ToString(ConfigurationManager.AppSettings["SolutionCode"]);



        #region Authentication Service DONT DELETE


        //ATOMv0.AuthService.authenticationPortClient cl = new AuthService.authenticationPortClient();
        //ATOMv0.AuthService.credentialsws crd = new AuthService.credentialsws();
        //crd.username = model.UserName;
        //crd.password = model.Password;
        //crd.solutionCode = strSolutionCode;
        //ATOMv0.AuthService.authenticate auth = new AuthService.authenticate();
        //auth.credentials = crd;

        //ATOMv0.AuthService.flexwaretokenws fxToken = new AuthService.flexwaretokenws();

        //fxToken = cl.authenticate(crd);

        //var userRoles = cl.getUserRoles(fxToken);

        #endregion


        #region Request to Role service DONT DELETE

        //NOTE:- DONT DELETE THIS IT WILL REPLACE WITH REAL DATA

        //ATOMv0.ServiceReference1.importUserRolePortClient clientImportRole = new ServiceReference1.importUserRolePortClient();
        //ATOMv0.ServiceReference1.importUserRoleData importRoleData = new ServiceReference1.importUserRoleData();
        //ATOMv0.ServiceReference1.flexwaretokenws impDataToken = new ServiceReference1.flexwaretokenws();
        //impDataToken.token = fxToken.token;

        //ATOMv0.ServiceReference1.usermasterws impUserMaster = new ATOMv0.ServiceReference1.usermasterws();
        //impUserMaster.userName = model.UserName;

        //ATOMv0.ServiceReference1.masterrolews impMasterRole = new ServiceReference1.masterrolews();
        //impMasterRole.code = "METRIC01";

        //ATOMv0.ServiceReference1.solutionrolews[] impSolutionRole = new ATOMv0.ServiceReference1.solutionrolews[]
        //{
        //    new ATOMv0.ServiceReference1.solutionrolews{ code = "METRIC01"}
        //};

        //impMasterRole.solutionroles = impSolutionRole;

        //ATOMv0.ServiceReference1.masterdataobjectws impMasterData = new ATOMv0.ServiceReference1.masterdataobjectws();
        //impMasterData.code = "ATOMSITE";

        //ATOMv0.ServiceReference1.masterdataelementws subMasteraData = new ServiceReference1.masterdataelementws();
        //subMasteraData.objects = new ServiceReference1.masterdataobjectws[]
        //{

        //  new ATOMv0.ServiceReference1.masterdataobjectws{code = "BUDAPEST"}
        //};

        //ATOMv0.ServiceReference1.masterdataelementws[] objMasterData = new ServiceReference1.masterdataelementws[]
        //{
        //   new ATOMv0.ServiceReference1.masterdataelementws{code = "ATOMSITE", objects = subMasteraData.objects}
        //};

        //ATOMv0.ServiceReference1.userroledataws[] userRoleDataList = new ServiceReference1.userroledataws[]
        //{
        //   new ATOMv0.ServiceReference1.userroledataws{masterData=objMasterData,masterRole=impMasterRole, requestApprovals="Y",status="true",userMaster=impUserMaster}
        //};

        //importRoleData.flexwareToken = impDataToken;
        //importRoleData.userRoleDataList = userRoleDataList;
        //clientImportRole.Open();
        //var result = clientImportRole.importUserRole(importRoleData);

        #endregion


        Authentication authentication = new Authentication(url);
        string tokenAuthentication = authentication.AuthenticateUser(model.UserName, model.Password, strSolutionCode);
        userrolesws userRoles = FlexWareGetUserRoles(authentication);

        if (userRoles != null)
        {
          isValidUser = true;
          FormsAuthentication.SetAuthCookie(model.UserName, false);
          TempData["Roles"] = userRoles;
          Session["Roles"] = userRoles;
          string roles = "";



          foreach (var item in userRoles.roles)
          {
            if (roles == "")
            {
              roles = item.name;
            }
            else
            {
              roles = roles + ";" + item.name;
            }
          }
          //roles = "ATOM_ADMINISTRATOR;SALES_USER";
          FormsAuthentication.SetAuthCookie(model.UserName + "|" + roles, false);

        }
        else
        {
          isValidUser = false;
        }

        masterdataelementws[] masterData = GetMasterData(authentication, out MasterDataErrors);

        if (masterData != null)
        {
          foreach (var item in masterData)
          {
            if (item.objects != null)
            {
              foreach (var siteItem in item.objects)
              {
                TempData["SiteName"] = siteItem.name;
                Session["SiteName"] = siteItem.name;
              }
            }

          }
        }
        return isValidUser;
      }
      catch (Exception ex)
      {
        return isValidUser = false;
      }
    }

    private masterdataelementws[] GetMasterData(Authentication authentication, out string errors)
    {
      try
      {
        errors = string.Empty;
        return authentication.GetMasterData();
      }
      catch (Exception ex)
      {
        errors = "Error on MasterData: " + ReadException(ex);
      }

      return null;
    }

    private string ReadException(Exception ex)
    {
      if (ex.InnerException != null)
      {
        return ReadException(ex.InnerException);
      }
      else
      {
        return ex.Message;
      }
    }

    private userrolesws FlexWareGetUserRoles(Authentication authentication)
    {
      try
      {
        // errors = string.Empty;
        return authentication.GetUserRoles();
      }
      catch (Exception ex)
      {
        // errors = "Error on GetRoles: " + ReadException(ex);
      }
      return null;
    }
  }
}