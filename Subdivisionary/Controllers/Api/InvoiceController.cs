using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Subdivisionary.Models;

namespace Subdivisionary.Controllers.Api
{
    /// <summary>
    /// API that has everything to do with Invoices
    /// </summary>
    [Authorize(Roles = EUserRoles.Admin)]
    public class InvoiceController : ApiController
    {

        /// <summary>
        /// Application Context is the portal between the SQL database and the model.
        /// It is the single greatest thing about Entity Framework.
        /// </summary>
        private ApplicationDbContext _context;

        /// <summary>
        /// Init ApplicationDB Context for serialization
        /// </summary>
        public InvoiceController()
        {
            _context = new ApplicationDbContext();
        }
    }
}
