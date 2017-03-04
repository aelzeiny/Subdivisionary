﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Subdivisionary.Models.Forms;

namespace Subdivisionary.Models.Collections
{
    [ComplexType]
    public class AdditionalDocsList : SerializableList<AdditionalDocumentInfo>
    {
    }
}