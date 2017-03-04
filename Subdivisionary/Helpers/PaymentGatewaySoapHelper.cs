﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Xml;
using Subdivisionary.Models;

namespace Subdivisionary.Controllers
{
    public class PaymentGatewaySoapHelper
    {
        private static readonly string SERVICE_URL = "http://devwebtest/ajax/application.asmx";
        //private static readonly string SERVICE_URL = "http://bsmnt/ajax/application.asmx";

        public static string CallWebService(ISoapEnvelope actionEnvelope)
        {
            var url = SERVICE_URL;
            var action = SERVICE_URL + actionEnvelope.Action;

            XmlDocument soapEnvelopeXml = actionEnvelope.GenerateEnvelope();
            HttpWebRequest webRequest = CreateWebRequest(url, action);
            InsertSoapEnvelopeIntoWebRequest(soapEnvelopeXml, webRequest);

            // begin async call to web request.
            IAsyncResult asyncResult = webRequest.BeginGetResponse(null, null);

            // suspend this thread until call is complete. You might want to
            // do something usefull here like update your UI.
            asyncResult.AsyncWaitHandle.WaitOne();

            // get the response from the completed web request.
            string serverResponce;
            using (WebResponse webResponse = webRequest.EndGetResponse(asyncResult))
            {
                using (StreamReader rd = new StreamReader(webResponse.GetResponseStream()))
                {
                    serverResponce = rd.ReadToEnd();
                }
            }
            return serverResponce;
        }

        public static XmlDocument CallWebServiceXml(ISoapEnvelope actionEnvelope)
        {
            XmlDocument answer = new XmlDocument();
            answer.LoadXml(CallWebService(actionEnvelope));
            return answer;
        }

        private static HttpWebRequest CreateWebRequest(string url, string action)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.Headers.Add("SOAPAction", action);
            webRequest.ContentType = "application/soap+xml;charset=\"utf-8\"";
            webRequest.Accept = "application/soap+xml";
            webRequest.Method = "POST";
            return webRequest;
        }
        
        private static void InsertSoapEnvelopeIntoWebRequest(XmlDocument soapEnvelopeXml, HttpWebRequest webRequest)
        {
            using (Stream stream = webRequest.GetRequestStream())
            {
                soapEnvelopeXml.Save(stream);
            }
        }

        public static string XmlToString(XmlDocument xml)
        {
            using (var stringWriter = new StringWriter())
                using (var xmlTextWriter = XmlWriter.Create(stringWriter))
                {
                    xml.WriteTo(xmlTextWriter);
                    xmlTextWriter.Flush();
                    return stringWriter.GetStringBuilder().ToString();
                }
        }
    }
}