using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Subdivisionary.Dtos;
using Subdivisionary.Models;
using Subdivisionary.Models.Applications;
using Subdivisionary.Models.ProjectInfos;

namespace Subdivisionary.ExternalApis
{
    public class SiltExternalApi : ExternalApi
    {
        private static readonly string SILT_URL = "http://localhost:49634";
        private static readonly string ENDPOINT_CREATE_APP = "/api/Projects";
        private static TokenModel _token;

        public SiltExternalApi(HttpClient client) : base(client)
        {
        }
        
        public override async Task InvoiceGenerated(Application application, InvoiceInfo invoice)
        {
        }

        public override async Task StatusChanged(Application application)
        {
            if (!(application is SidewalkLegislation))
                return;
            if (_token == null || _token.Expiration <= DateTime.Now)
                _token = await Login();
            SetupClientHeader();
            if (application.CurrentStatusLog.Status == EApplicationStatus.Fresh)
            {
                // Create Application
                string convertedObj = JsonConvert.SerializeObject(Mapper.Map<ExtendedProjectInfoDto>(application.ProjectInfo),
                    Formatting.None, new JsonSerializerSettings {NullValueHandling = NullValueHandling.Ignore});
                var content = new StringContent(convertedObj, Encoding.UTF8, "application/json");
                await _client.PostAsync(ENDPOINT_CREATE_APP, content);
            }
        }

        protected void SetupClientHeader()
        {
            if(_client.BaseAddress == null)
                _client.BaseAddress = new Uri(SILT_URL);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + _token.Token);
        }

        protected async Task<TokenModel> Login()
        {
            var task = GetApiToken("subdivision.mapping@sfdpw.org", "keepsfclean1", SILT_URL);
            return await task;
        }

        protected async Task<TokenModel> GetApiToken(string userName, string password, string apiBaseUri)
        {
            //setup client
            _client.BaseAddress = new Uri(apiBaseUri);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //setup login data
            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("username", userName),
                new KeyValuePair<string, string>("password", password),
            });
            //send request
            HttpResponseMessage responseMessage = await _client.PostAsync("/Token", formContent);

            //get access token from response body
            var responseJson = await responseMessage.Content.ReadAsStringAsync();
            var jObject = JObject.Parse(responseJson);
            double secondsTillExp = Math.Max(0, double.Parse(jObject.GetValue("expires_in").ToString()) - 5);
            return new TokenModel()
            {
                Token = jObject.GetValue("access_token").ToString(),
                Expiration = DateTime.Now + TimeSpan.FromSeconds(secondsTillExp)
            };
        }

        protected class TokenModel
        {
            public string Token { get; set; }
            public DateTime Expiration { get; set; }
        }
    }
}