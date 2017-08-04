using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Subdivisionary.Models
{
    /// <summary>
    /// Like an Enum w/ String but since that's not native to C# we gotta improvise.
    /// </summary>
    public static class EUserRoles
    {
        public const string Applicant = "ApplicantRole";
        public const string Agency = "AgencyRole";
        public const string Bsm = "BsmRole";
        public const string Admin = "AdminRole";

        public static IList<string> ToList()
        {
            FieldInfo[] fieldInfos = typeof(EUserRoles).GetFields(BindingFlags.Public |
                 BindingFlags.Static | BindingFlags.FlattenHierarchy);

            return fieldInfos.Where(fi => fi.IsLiteral && !fi.IsInitOnly).Select(x=>(string)x.GetRawConstantValue()).ToList();
        }
    }
}