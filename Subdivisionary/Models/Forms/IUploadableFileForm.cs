using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Subdivisionary.Models.Collections;

namespace Subdivisionary.Models.Forms
{
    public interface IUploadableFileForm
    {
        FileUploadProperty[] FileUploadProperties();

        FileUploadList GetFileUploadList(string key);

        void SyncFile(string key, string file);
    }
}
