using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Subdivisionary.Models
{
    /// <summary>
    /// Like an Enum w/ String but since that's not native to C# we gotta improvise.
    /// </summary>
    public sealed class EUserRoles
    {
        public static readonly EUserRoles Applicant = new EUserRoles("ApplicantRole");
        public static readonly EUserRoles Agency = new EUserRoles("AgencyRole");
        public static readonly EUserRoles Bsm = new EUserRoles("BsmRole");
        public static readonly EUserRoles Admin = new EUserRoles("AdminRole");

        private readonly string _name;

        private EUserRoles(string name)
        {
            this._name = name;
        }

        public override string ToString()
        {
            return _name;
        }

    }
}