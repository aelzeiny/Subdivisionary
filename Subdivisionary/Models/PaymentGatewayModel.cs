using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml;

namespace Subdivisionary.Models
{
    
    /// <summary>
    /// http://devwebtest/ajax/application.asmx?op=CreateInvoice
    /// Based on the specifications provided by Geoffery
    /// </summary>
    public interface ISoapEnvelope
    {
        string Action { get; }
        XmlDocument GenerateEnvelope();
    }

    /// <summary>
    /// &lt;username&gt;string&lt;/username&gt;
    /// &lt;invoiceid&gt;int&lt;/invoiceid&gt;
    /// &lt;companyid&gt;int&lt;/companyid&gt;
    /// &lt;amount&gt;double&lt;/amount&gt;
    /// &lt;reference&gt;string&lt;/reference&gt;
    /// &lt;memo&gt;string&lt;/memo&gt;
    /// &lt;accntcode&gt;string&lt;/accntcode&gt;
    /// &lt;accountdesc&gt;string&lt;/accountdesc&gt;
    /// &lt;invoice_type&gt;int&lt;/invoice_type&gt;
    /// </summary>
    public class CreateInvoiceEnvelope : ISoapEnvelope
    {
        public string Action => "?op=CreateInvoice";

        public string UserName { get; set; }
        public int InvoiceId { get; set; }
        public int InvoiceType { get; set; }
        public int CompanyId { get; set; }
        public double Amount { get; set; }
        public string Reference { get; set; }
        public string Memo { get; set; }
        public string AccountCode { get; set; }
        public string AccountDesc { get; set; }

        public static readonly string ACCOUNT_MAPPING_ITEM = "29618";
        public static readonly string ACCOUNT_ECP_FEE_ITEM = "76742";
        public static readonly string ACCOUNT_MONUMENT_FEE_ITEM = "76806";
        public static readonly string ACCOUNT_MONUMENT_PRESERVATION_FEE_ITEM = "76888";
        public static readonly string ACCOUNT_MONUMENT_REFERENCE_FEE_ITEM = "76889";

        public CreateInvoiceEnvelope(string username, double amount, string shortDescrRef, string longDescrMemo, string accntCode, string accntDesc, int invoiceType)
        {
            // Default Params
            this.InvoiceId = 0;
            this.CompanyId = 29783;
            // Vars
            this.UserName = username;
            this.Amount = amount;
            this.Reference = shortDescrRef;
            this.Memo = longDescrMemo;
            this.AccountCode = accntCode;
            this.AccountDesc = accntDesc;
            this.InvoiceType = invoiceType;
        }

        public XmlDocument GenerateEnvelope()
        {
            XmlDocument soapEnvelope = new XmlDocument();
            soapEnvelope.LoadXml($@"
<soap12:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap12=""http://www.w3.org/2003/05/soap-envelope"">
  <soap12:Body>
    <CreateInvoice xmlns=""http://sfdpw.org/"">
      <username>{UserName}</username>
      <invoiceid>{InvoiceId}</invoiceid>
      <companyid>{CompanyId}</companyid>
      <amount>{Amount:N2}</amount>
      <reference>{Reference}</reference>
      <memo>{Memo}</memo>
      <accntcode>{AccountCode}</accntcode>
      <accountdesc>{AccountDesc}</accountdesc>
      <invoice_type>{InvoiceType}</invoice_type>
    </CreateInvoice>
  </soap12:Body>
</soap12:Envelope>");
            return soapEnvelope;
        }
    }

    /// <summary>
    /// No parameters. Returns list of all invoice types
    /// </summary>
    public class ListInvoiceEnvelope : ISoapEnvelope
    {
        public string Action => "?op=ListInvoiceTypes";
        public XmlDocument GenerateEnvelope()
        {
            XmlDocument soapEnvelope = new XmlDocument();
            soapEnvelope.LoadXml(@"
<soap12:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap12=""http://www.w3.org/2003/05/soap-envelope"">
  <soap12:Body>
    <ListInvoiceTypes xmlns=""http://sfdpw.org/"" />
  </soap12:Body>
</soap12:Envelope>");
            return soapEnvelope;
        }
    }

    public class ReadInvoiceEnvelope : ISoapEnvelope
    {
        public virtual string Action => "?op=ReadInvoice";

        public int InvoiceId { get; set; }

        public ReadInvoiceEnvelope(int invoiceId)
        {
            this.InvoiceId = invoiceId;
        }

        public XmlDocument GenerateEnvelope()
        {
            XmlDocument soapEnvelope = new XmlDocument();
            soapEnvelope.LoadXml($@"
<soap12:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap12=""http://www.w3.org/2003/05/soap-envelope"">
  <soap12:Body>
    <ReadInvoice xmlns=""http://sfdpw.org/"">
      <invoiceid>{InvoiceId}</invoiceid>
    </ReadInvoice>
  </soap12:Body>
</soap12:Envelope>");
            return soapEnvelope;
        }
    }

    public class ReadInvoiceUrlEnvelope : ReadInvoiceEnvelope
    {
        public override string Action => "?op=ReadInvoiceURL";

        public ReadInvoiceUrlEnvelope(int invoiceId) : base(invoiceId)
        {
        }
    }

    public enum PaymentAccounts
    {
        [Display(Name = "Mapping Item")]
        MappingItem = 29618,
        [Display(Name = "ECP Fee")]
        EcpFee = 76742,
        [Display(Name = "Monument Fee")]
        MonumentFee = 76806,
        [Display(Name = "Monument Preservation Fee")]
        MonumentPreservationFee = 76888,
        [Display(Name = "Monument Reference Fee")]
        MonumentReferenceFee = 76889
    }

    public class InvoiceTypeXml : XmlDocument
    {
        public static readonly string INVOICE_TYPE_ECP = "ECP";
        public static readonly string INVOICE_TYPE_GENERAL = "Mapping";
        public static readonly string INVOICE_TYPE_MONUMENT = "Monument";
        public static readonly string INVOICE_TYPE_CONVERSION = "Conversions";

        public InvoiceTypeXml(string responce) : base()
        {
            this.LoadXml(responce);
        }

        public InvoiceType FindInvoiceByType(string type)
        {
            var tables = this.GetElementsByTagName("Table");
            for(int i=0;i<tables.Count;i++)
            {
                if (tables[i]["Invoice_Type_Name_Text"].InnerText == type)
                    return new InvoiceType(tables[i]);
            }
            return null;
        }

        public class InvoiceType
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string AgencyName { get; set; }

            public InvoiceType(XmlNode node)
            {
                Id = int.Parse(node["Invoice_Type_ID"].InnerText);
                Name = node["Invoice_Type_Name_Text"].InnerText;
                AgencyName = node["Invoice_Type_Agency_Name"].InnerText;
            }
        }
    }
}