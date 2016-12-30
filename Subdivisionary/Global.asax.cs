using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using Subdivisionary.App_Start;
using Subdivisionary.DAL;
using Subdivisionary.Models.ProjectInfos;
using Subdivisionary.ViewModels;

namespace Subdivisionary
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Mapper.Initialize(config => config.AddProfile<MappingProfile>());
            GlobalConfiguration.Configure(WebApiConfig.Register);
            AreaRegistration.RegisterAllAreas();
            ModelBinders.Binders.Add(typeof(NewApplicationViewModel), new ProjectInfoModelBinder());
            ModelBinders.Binders.Add(typeof(EditFormViewModel), new FormModelBinder());
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
