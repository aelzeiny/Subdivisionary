using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Subdivisionary.Models.Forms;

namespace Subdivisionary.Models.Collections
{
    /*
        public string Title { get; set; }
        public string ScanPath { get; set; }
    */
    [ComplexType]
    public class AdditionalDocsList : CsvList<AdditionalDocumentInfo>
    {
        protected override int ParamCount => 2;

        protected override AdditionalDocumentInfo ParseObject(string[] param)
        {
            return new AdditionalDocumentInfo()
            {
                Title = param[0],
                ScanPath = param[1]
            };
        }

        protected override string[] SerializeObject(AdditionalDocumentInfo serialize)
        {
            return new string[]
            {
                serialize.Title,
                serialize.ScanPath
            };
        }
    }
}