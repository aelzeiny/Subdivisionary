using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Subdivisionary.Models.Forms
{
    public interface IForm
    {
        string DisplayName { get; }
        string PropertyName { get; }

        bool IsAssigned { get; set; }
    }
}
