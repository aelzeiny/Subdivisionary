using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Subdivisionary.Models;
using Subdivisionary.Models.Applications;

namespace Subdivisionary.ExternalApis
{
    /// <summary>
    /// Class that facilitates external Web API calls. Takes care of all the back-end 
    /// technicals with calling a web-api using C#. Also is the base-class for all 
    /// SAS triggers to external APIs
    /// See Microsoft's Web-Api Documentation Here:
    /// https://docs.microsoft.com/en-us/aspnet/web-api/overview/advanced/calling-a-web-api-from-a-net-client
    /// </summary>
    public abstract class ExternalApi
    {
        protected HttpClient _client;
        public ExternalApi(HttpClient client)
        {
            _client = new HttpClient();
        }

        public abstract Task InvoiceGenerated(Application application, InvoiceInfo invoice);

        public abstract Task StatusChanged(Application application);
    }
}