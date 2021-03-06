﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace ATOMv0
{
  public class MvcApplication : System.Web.HttpApplication
  {
    protected void Application_Start()
    {
      AreaRegistration.RegisterAllAreas();
      FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
      RouteConfig.RegisterRoutes(RouteTable.Routes);
      BundleConfig.RegisterBundles(BundleTable.Bundles);
    }

    protected void FormsAuthentication_OnAuthenticate(Object sender, FormsAuthenticationEventArgs e)
    {
      if (FormsAuthentication.CookiesSupported == true)
      {
        if (Request.Cookies[FormsAuthentication.FormsCookieName] != null)
        {
          try
          {
            string userDetails = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;
            string roles = "";

            string[] userName;
            if (userDetails != null && userDetails.Length > 0)
            {
              userName = userDetails.Split('|');
              HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(
                    new System.Security.Principal.GenericIdentity(userName[0], "Forms"), userName[1].Split(';'));
            }

          }
          catch (Exception ex)
          {
            throw ex;
          }
        }
      }
    }
  }
}
