using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace Subdivisionary.Models.Collections
{
    [ComplexType]
    public class AddressList : SerializableList<ParcelInfo>
    {
    }

    public class ParcelInfo
    {
        [Required]
        [RegularExpression(@"^[A-Za-z0-9]*[A-Za-z0-9][A-Za-z0-9]*$", ErrorMessage = "Only letters and numbers are allowed in this field.")]
        public string Block { get; set; }
        [Required]
        [RegularExpression(@"^[A-Za-z0-9]*[A-Za-z0-9][A-Za-z0-9]*$", ErrorMessage = "Only letters and numbers are allowed in this field.")]
        public string Lot { get; set; }
        [Required]
        [DisplayName("Range Start")]
        [RegularExpression(@"^[A-Za-z0-9]*[A-Za-z0-9][A-Za-z0-9]*$", ErrorMessage = "Only letters and numbers are allowed in this field.")]
        public string AddressRangeStart { get; set; }
        [Required]
        [DisplayName("Range End")]
        [RegularExpression(@"^[A-Za-z0-9]*[A-Za-z0-9][A-Za-z0-9]*$", ErrorMessage = "Only letters and numbers are allowed in this field.")]
        public string AddressRangeEnd { get; set; }
        [Required]
        [DisplayName("Street Name")]
        [RegularExpression(@"^[A-Za-z0-9 ]*[A-Za-z0-9][A-Za-z0-9 ]*$", ErrorMessage = "Only letters, numbers, and spaces are allowed in this field.")]
        public string AddressStreet { get; set; }

        public override string ToString()
        {
            return string.Format("({0}/{1}) {2}-{3} {4}", Block, Lot, AddressRangeStart, AddressRangeEnd, AddressStreet);
        }
    }
}