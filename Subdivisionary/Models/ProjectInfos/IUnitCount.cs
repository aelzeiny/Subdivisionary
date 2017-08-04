using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subdivisionary.Models.ProjectInfos
{
    interface IUnitCount
    {
        int UnitCount { get; }
        bool IsFinalMap();
    }
}
