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

        public static readonly string RECORDED_ALPHA_KEY = "recordedMapsAlphaId";
        public static readonly string RECORDED_ASSESSOR_KEY = "recordedMapsAssessorId";
        public static readonly string RECORDED_KEYMAPS_KEY = "recordedMapsKeymapsId";
        public static readonly string RECORDED_HISTORIC_KEY = "recordedMapsHistoricId";
        public static readonly string RECORDED_RECORDEDMAPS_KEY = "recordedMapsRecordedId";
        public static readonly string RECORDED_OTHER_MAPS_KEY = "recordedMapsOtherId";
        public static readonly string RECORDED_MAPS_ON_BLOCK_DIRECTORY = "Recorded Maps On Block";

        public override FileUploadProperty[] FileUploadProperties => new[]
        {
            new FileUploadProperty(this.Id, RECORDED_ALPHA_KEY, RECORDED_MAPS_ON_BLOCK_DIRECTORY, "Alpha Map", false, false),
            new FileUploadProperty(this.Id, RECORDED_ASSESSOR_KEY, RECORDED_MAPS_ON_BLOCK_DIRECTORY, "Assessor Block Map", false, false),
            new FileUploadProperty(this.Id, RECORDED_KEYMAPS_KEY, RECORDED_MAPS_ON_BLOCK_DIRECTORY, "Key Map", false, false),
            new FileUploadProperty(this.Id, RECORDED_HISTORIC_KEY, RECORDED_MAPS_ON_BLOCK_DIRECTORY, "Historic Block Diagram", false, false),
            new FileUploadProperty(this.Id, RECORDED_RECORDEDMAPS_KEY, RECORDED_MAPS_ON_BLOCK_DIRECTORY, "Recorded Map", false, false),
            new FileUploadProperty(this.Id, RECORDED_OTHER_MAPS_KEY, RECORDED_MAPS_ON_BLOCK_DIRECTORY, "Other Map", false, false),
        };
    }
}