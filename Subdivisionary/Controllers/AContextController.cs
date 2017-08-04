using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Subdivisionary.Models;

namespace Subdivisionary.Controllers
{
    public abstract class AContextController : Controller
    {
        protected ApplicationDbContext _context;

        public AContextController()
        {
            _context = new ApplicationDbContext();
            _context.Configuration.ValidateOnSaveEnabled = false;
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        protected virtual Applicant GetCurrentApplicant()
        {
            var user = GetCurrentUser();
            return _context.Applicants.Where(app => app.Id == user.DataId)
                .Include(appl => appl.Applications)
                .Include(appl => appl.Notifications)
                .FirstOrDefault();
        }

        protected virtual ApplicationUser GetCurrentUser()
        {
            return System.Web.HttpContext.Current.GetOwinContext()
                .GetUserManager<ApplicationUserManager>()
                .FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
        }
    }
}