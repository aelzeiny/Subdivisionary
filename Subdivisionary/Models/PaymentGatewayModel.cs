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
    /// http://devwebtest/ajax/application.asmx
    /// Based on the specifications provided by Geoffery
    /// </summary>
    public interface ISoapEnvelope
    {
        string Action { get; }
        XmlDocument GenerateEnvelope();
    }

    public sealed class EPaymentAccounts
    {
        public string Description { get; set; }
        public string AccountNum { get; set; }

        public static readonly EPaymentAccounts EcpFee = new EPaymentAccounts("ECP Fee Item", "29618");
        public static readonly EPaymentAccounts MonumentFee = new EPaymentAccounts("Monument Item", "76742");
        public static readonly EPaymentAccounts MappingItem = new EPaymentAccounts("Mapping Item - Application Fees ", "76806");
        public static readonly EPaymentAccounts MonumentReferenceFee = new EPaymentAccounts("Monument Reference Item", "76888");
        public static readonly EPaymentAccounts MonumentPreservationFee = new EPaymentAccounts("Monument Preservation Item", "76889");

        public EPaymentAccounts(string descr, string accountNum)
        {
            this.AccountNum = accountNum;
            this.Description = descr;
        }

        public override string ToString()
        {
            return Description;
        }
    }

    #region Envelopes are used to call Soap Actions
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
        public string Action => "?op=ReadInvoice";

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

    public class ReadInvoiceUrlEnvelope : ISoapEnvelope
    {
        public string Action => "?op=ReadInvoiceURL";

        public int InvoiceId { get; set; }

        public ReadInvoiceUrlEnvelope(int invoiceId)
        {
            this.InvoiceId = invoiceId;
        }

        public XmlDocument GenerateEnvelope()
        {
            XmlDocument soapEnvelope = new XmlDocument();
            soapEnvelope.LoadXml($@"
<soap12:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap12=""http://www.w3.org/2003/05/soap-envelope"">
  <soap12:Body>
    <ReadInvoiceURL xmlns=""http://sfdpw.org/"">
      <invoiceid>{InvoiceId}</invoiceid>
    </ReadInvoiceURL>
  </soap12:Body>
</soap12:Envelope>");
            return soapEnvelope;
        }
    }


    public class VoidInvoiceEnvelope : ISoapEnvelope
    {
        public string Action => "?op=VoidInvoice";

        public int InvoiceId { get; set; }
        public string UserName { get; set; }
        public string Reason { get; set; }

        public VoidInvoiceEnvelope(int invoiceId, string username, string reason)
        {
            this.InvoiceId = invoiceId;
            this.UserName = username;
            this.Reason = reason;
        }

        public XmlDocument GenerateEnvelope()
        {
            XmlDocument soapEnvelope = new XmlDocument();
            soapEnvelope.LoadXml($@"
<soap12:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap12=""http://www.w3.org/2003/05/soap-envelope"">
  <soap12:Body>
    <VoidInvoice xmlns=""http://sfdpw.org/"">
      <username>{UserName}</username>
      <invoiceid>{InvoiceId}</invoiceid>
      <Reason>{Reason}</Reason>
    </VoidInvoice>
  </soap12:Body>
</soap12:Envelope>");
            return soapEnvelope;
        }
    }
    #endregion

    #region Xmls are used to parse Soap Results
    public class InvoiceTypeXml : XmlDocument
    {
        public static readonly string INVOICE_TYPE_ECP = "ECP";
        public static readonly string INVOICE_TYPE_GENERAL = "Mapping";
        public static readonly string INVOICE_TYPE_MONUMENT = "Monument";
        public static readonly string INVOICE_TYPE_CONVERSION = "Conversions";

        public InvoiceTypeXml(string response) : base()
        {
            this.LoadXml(response);
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

    public abstract class InvoiceXml : XmlDocument
    {
        public int Id { get; set; }
        public string Payurl { get; set; }
        public string Printurl { get; set; }

        public abstract string ParentNodeName { get; }

        public InvoiceXml(string response)
        {
            this.LoadXml(response);
            var node = GetElementsByTagName(ParentNodeName)[0];
            this.Id = int.Parse(node["invoiceid"].InnerText);
            this.Payurl = node["PayURL"].InnerText;
            this.Printurl = node["PrintURL"].InnerText;
        }
    }

    public class CreateInvoiceXml : InvoiceXml
    {
        public override string ParentNodeName => "CreateInvoiceResult";

        public CreateInvoiceXml(string response) : base(response)
        {
        }
    }

    public class ReadInvoiceUrlXml : InvoiceXml
    {
        public override string ParentNodeName => "ReadInvoiceURLResult";

        public ReadInvoiceUrlXml(string response) : base(response)
        {
        }
    }

    public class VoidInvoiceXml : XmlDocument
    {
        public string Result { get; set; }

        public VoidInvoiceXml(string response)
        {
            this.LoadXml(response);
            this.Result = GetElementsByTagName("VoidInvoiceResult")[0].InnerText;
        }
    }

    public class ReadInvoiceXml : XmlDocument
    {
        public DateTime Created { get; set; }
        public string Reference { get; set; }
        public bool IsPaid { get; set; }
        public string Balance { get; set; }
        public string AmountDue { get; set; }
        public string DocNo { get; set; }
        public string CustomerName { get; set; }
        public int Id { get; set; }
        public string Memo { get; set; }
        public string DueDate { get; set; }
        public bool Voided { get; set; }
        public string Receipt { get; set; }
        public string PayCode { get; set; }
        public string ActivityId { get; set; }
        public string RequestedBy { get; set; }
        public string TransactionId { get; set; }
        public string RcString { get; set; }
        public string PayStatus { get; set; }
        public string AuthCode { get; set; }
        public string Waived { get; set; }
        public string Payment { get; set; }
        public string Refund { get; set; }
        public string StatusText { get; set; }
        public bool BlockPayment { get; set; }
        public string BlockPaymentMsg { get; set; }
        public string CompanyId { get; set; }
        public string ProcessingCode { get; set; }
        public string InvoiceType { get; set; }

        public ReadInvoiceXml(string response)
        {
            this.LoadXml(response);
            var node = GetElementsByTagName("ReadInvoiceResult")[0];
            Created = DateTime.Parse(node["created"]?.InnerText);
            Reference = (node["reference"]?.InnerText);  //reference
            IsPaid = bool.Parse(node["ispaid"]?.InnerText);   //ispaid
            Balance = (node["balance"]?.InnerText);//balance
            AmountDue = (node["amountdue"]?.InnerText); //amountdue
            DocNo = (node["documentnumber"]?.InnerText);   //documentnumber
            CustomerName = (node["customername"]?.InnerText); //customername
            Id = int.Parse(node["id"]?.InnerText);   //id
            Memo = (node["memo"]?.InnerText);   //memo
            DueDate = (node["duedate"]?.InnerText);   //duedate
            Voided = bool.Parse(node["isvoided"]?.InnerText);   //isvoided
            Receipt = (node["receipt"]?.InnerText);   //receipt
            PayCode = (node["paycode"]?.InnerText);   //paycode
            ActivityId = (node["activityid"]?.InnerText);   //activityid
            RequestedBy = (node["requestedby"]?.InnerText);   //requestedby
            TransactionId = (node["transactionid"]?.InnerText);   //transactionid
            RcString = (node["rcstring"]?.InnerText);   //rc
            PayStatus = (node["paystatus"]?.InnerText);   //paystatus
            AuthCode = (node["authcode"]?.InnerText);   //authcode
            Waived = (node["waived"]?.InnerText);   //waived
            Payment = (node["payment"]?.InnerText);   //payment
            Refund = (node["refund"]?.InnerText);   //refund
            StatusText = (node["statustext"]?.InnerText);   //statustext
            BlockPayment = bool.Parse(node["blockpayment"]?.InnerText);   //blockpayment
            BlockPaymentMsg = (node["blockpaymentmsg"]?.InnerText);   //blockpaymentmsg
            CompanyId = (node["companyid"]?.InnerText);   //companyid
            ProcessingCode = (node["processingcode"]?.InnerText);   //processingcode
            InvoiceType = (node["InvoiceType"]?.InnerText);   //InvoiceType
        }
    }

    #endregion
}