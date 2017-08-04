using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using Subdivisionary.DAL;
using Subdivisionary.FileUploaders;
using Subdivisionary.Models.Forms;
using Subdivisionary.Models.ProjectInfos;
using Subdivisionary.ViewModels;

namespace Subdivisionary
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Mapper.Initialize(config => config.AddProfile<MappingProfile>());
            AreaRegistration.RegisterAllAreas();

            GoogleDriveUploader.InitializeCredentials(Server.MapPath("~/App_Data/SAS-GoogleDrive-Key.json"));

            ModelBinders.Binders.Add(typeof(IForm), new FormModelBinder());
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
