using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Web;
using Subdivisionary.Models;
using Subdivisionary.Models.Applications;
using System.Threading.Tasks;

namespace Subdivisionary.ExternalApis
{
    /// <summary>
    /// Creates a new thread that runs concurrently to (possibly) trigger a POST/PUT
    /// request from an external API. Note that these operation runs parrallel to our
    /// main thread, and the result doesn't inform the SAS. So we set a thread lose into 
    /// the world, knowing that we don't care it ever returns. 
    /// </summary>
    public class ExternalApiManager : IDisposable
    {
        /// <summary>
        /// A list of external apis to trigger
        /// </summary>
        private readonly ExternalApi[] _apis;

        /// <summary>
        /// The HttpClient handling all post/put requests
        /// </summary>
        private readonly HttpClient _client;

        /// <summary>
        /// Timeout for each API call until a timeout exception is thrown
        /// </summary>
        private static readonly int TIMEOUT_SECONDS = 300; // 5 Minutes

        /// <summary>
        /// Constructor declares client & list of external apis. If you want to add an external api
        /// to the list of notified apis, then create a class that inherients from 'ExternalApi' 
        /// and add it here.
        /// </summary>
        public ExternalApiManager()
        {
            _client = new HttpClient();
            // Do you want other apis to be triggered on key application events?
            // Create an ExternalApi class and add it to the _api array.
            _apis = new ExternalApi[]
            {
                new SiltExternalApi(_client)
            };
        }

        /// <summary>
        /// Trigger External API when an invoice is generated
        /// </summary>
        /// <param name="application">Application that Invoice was generated for</param>
        /// <param name="invoice">Invoice that was generated</param>
        private async void InvoiceGeneratedAsync(Application application, InvoiceInfo invoice)
        {
            foreach (ExternalApi api in _apis)
            {
                try
                {
                    await TimeoutAfter(api.InvoiceGenerated(application, invoice), TimeSpan.FromSeconds(TIMEOUT_SECONDS));
                }
                catch (TimeoutException ex)
                {
                    // Log error into elmah for future review.
                    var ext = new TimeoutException($"External API timeout - external API of type {typeof(ExternalApi).Name} " +
                                                   "could not register InvoiceGeneratedAsync call in time. " +
                                                   $"Application Id: {application.Id}. Invoice Id: {invoice.Id}");
                    Elmah.ErrorSignal.FromCurrentContext().Raise(ext);
                }
            }
                
        }

        /// <summary>
        /// Trigger External API when a status change has occured (these could include payments)
        /// </summary>
        /// <param name="application"></param>
        private async void StatusChangedAsync(Application application)
        {
            foreach (ExternalApi api in _apis)
            {
                try
                {
                    await TimeoutAfter(api.StatusChanged(application), TimeSpan.FromSeconds(TIMEOUT_SECONDS));
                }
                catch (Exception ex)
                {
                    // Log error into elmah for future review.
                    if(ex is TimeoutException)
                        ex = new TimeoutException($"External API timeout - external API of type {typeof(ExternalApi).Name} " +
                                                   "could not register StatusChangedAsync call in time. " +
                                                   $"Application Id: {application.Id}. Status Id: {application.CurrentStatusLog.Id}");
                    Elmah.ErrorLog.GetDefault(HttpContext.Current).Log(new Elmah.Error(ex));
                    //Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                }
            }
        }

        /// <summary>
        /// Task that returns either when a specified task completes or when a timeout occurs. If the 
        /// returning task is the source task, then the value is returned. Otherwise an exception occurs
        /// when the timer runs out
        /// </summary>
        /// <param name="task">Task being performed</param>
        /// <param name="timeout">Timeout timespan before an error is thrown</param>
        /// <param name="applicationId">Application ID for the purposes of logging the error.</param>
        /// <returns></returns>
        private async Task TimeoutAfter(Task task, TimeSpan timeout)
        {

            var timeoutCancellationTokenSource = new CancellationTokenSource();

            var completedTask = await Task.WhenAny(task, Task.Delay(timeout, timeoutCancellationTokenSource.Token));
            if (completedTask == task)
            {
                timeoutCancellationTokenSource.Cancel();
                await task;  // Very important in order to propagate exceptions
            }
            else
            {
                throw new TimeoutException("The operation has timed out");
            }
        }

        /// <summary>
        /// Concurrent thread that calls InvoiceGenerated() function on all external APIs.
        /// </summary>
        /// <param name="application">Application where event occured</param>
        /// <param name="invoice">Invoice where event occured</param>
        public static void InvoiceGeneratedTriggerInBackground(Application application, InvoiceInfo invoice)
        {
            Task.Run(() => {
                using (ExternalApiManager manager = new ExternalApiManager())
                {
                    manager.InvoiceGeneratedAsync(application, invoice);
                }
            });
        }

        /// <summary>
        /// Concurrent thread that calls StatusChanged() function on all external APIs.
        /// </summary>
        /// <param name="application"></param>
        public static void StatusChangedTriggerInBackground(Application application)
        {
            Task.Run(() => {
                using (ExternalApiManager manager = new ExternalApiManager())
                {
                    manager.StatusChangedAsync(application);
                }
            });
        }

        /// <summary>
        /// Disposes of the HttpClient resource
        /// </summary>
        public void Dispose()
        {
            _client.Dispose();
        }
    }
}