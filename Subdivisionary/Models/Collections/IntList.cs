using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Subdivisionary.Models.Collections
{
    [ComplexType]
    public class IntList : SerializableList<int>
    {
    }
}