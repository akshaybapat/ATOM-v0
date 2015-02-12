using ATOMv0.AuthenticationService;
using ATOMv0.Models;
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
          ViewBag.UserName = userRoles.userMaster.firstName + " " + userRoles.userMaster.lastName;
          Session["UserName"] = ViewBag.UserName;
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
      if (Session["UserName"] != null)
      {
        ViewBag.UserName = Session["UserName"];
      }
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
      if (Session["UserName"] != null)
      {
        Session["UserName"] = null;
      }

    }

    private bool ValidateUser(LoginModel model)
    {
      try
      {
        bool isValidUser = false;
        string Roleserrors = string.Empty;
        string MasterDataErrors = string.Empty;
        string strSolutionCode = Convert.ToString(ConfigurationManager.AppSettings["SolutionCode"]);
        string strMasterDataAtomSite = Convert.ToString(ConfigurationManager.AppSettings["ATOMSITE"]);

        #region Authentication Service

        authenticationPortClient authClient = new authenticationPortClient();
        credentialsws autCredentials = new credentialsws();
        autCredentials.username = model.UserName;
        autCredentials.password = model.Password;
        autCredentials.solutionCode = strSolutionCode;
        authenticate authenticate = new authenticate();
        authenticate.credentials = autCredentials;
        flexwaretokenws fxToken = new flexwaretokenws();
        fxToken = authClient.authenticate(autCredentials);
        var userRoles = authClient.getUserRoles(fxToken);

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
          FormsAuthentication.SetAuthCookie(model.UserName + "|" + roles, false);
        }
        else
        {
          isValidUser = false;
        }

        var masterData = authClient.getMasterData(fxToken);

        if (masterData != null)
        {
          foreach (var item in masterData)
          {
            if (item.objects != null)
            {
              if (item.code == strMasterDataAtomSite)
              {
                foreach (var siteItem in item.objects)
                {

                  TempData["SiteName"] = siteItem.name;
                  Session["SiteName"] = siteItem.name;
                }
              }
            }
          }
        }

        #endregion

        return isValidUser;
      }
      catch (Exception ex)
      {
        return isValidUser = false;
      }
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
  }
}