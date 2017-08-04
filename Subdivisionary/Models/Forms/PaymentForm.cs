using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using Subdivisionary.Models.Collections;
using Subdivisionary.Models.Validation;

namespace Subdivisionary.Models.Forms
{
    public class PaymentForm : UploadableFileForm, ICollectionForm
    {
        public override ICollection<FileUploadInfo> FileUploads { get; set; }

        public CheckList ChecksList { get; set; }

        [Display(Name = "I would rather mail in check(s)?")]
        public bool PaidWithChecks { get; set; }
        
        public int InvoiceId { get; set; }
        public virtual InvoiceInfo Invoice { get; set; }

        public override string DisplayName => "Application Fees";

        public static readonly string CHECK_DIRECTORY = "App Fees";
        public static readonly string CHECK_KEY = "appFeesId";
        public static readonly string CHECKCOLL_KEY = "checkCollFeesId";

        public string[] Keys => new[] { CHECKCOLL_KEY };

        public PaymentForm()
        {
            ChecksList = new CheckList();
        }

        public override FileUploadProperty[] FileUploadProperties => new[]
        {
            new FileUploadProperty(this.Id, CHECK_KEY, CHECK_DIRECTORY, "Check") {IsSingleUpload = false, IsRequiredUpload = PaidWithChecks}
        };

        public override bool CanCopyProperty(PropertyInfo prop)
        {
            return base.CanCopyProperty(prop) && prop.Name != nameof(InvoiceId) && prop.Name != nameof(Invoice);
        }

        /***** ICollectionForm Implementation *****/
        public ICollectionAdd GetListCollection(string key)
        {
            return ChecksList;
        }

        public object GetEmptyItem(string key)
        {
            return new CheckInfo();
        }
    }
}