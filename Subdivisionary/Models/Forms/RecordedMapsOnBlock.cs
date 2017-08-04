using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Subdivisionary.Models.Forms
{
    public class RecordedMapsOnBlock : UploadableFileForm
    {
        public override string DisplayName => "Recorded Maps on Block";
        
        public static readonly string MAPS_REFERENCED_KEY = "recordedMapsRefId";
        public static readonly string MAPS_FIELDNOTES_KEY = "recordedMapsFieldId";

        public static readonly string RECORDED_MAPS_ON_BLOCK_DIRECTORY = "Recorded Maps On Block";

        public override FileUploadProperty[] FileUploadProperties => new[]
        {
            new FileUploadProperty(this.Id, MAPS_REFERENCED_KEY, RECORDED_MAPS_ON_BLOCK_DIRECTORY, "Referenced Maps", false, false),
            new FileUploadProperty(this.Id, MAPS_FIELDNOTES_KEY, RECORDED_MAPS_ON_BLOCK_DIRECTORY, "Field Notes", false, false)
        };
    }
}