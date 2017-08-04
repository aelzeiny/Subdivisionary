using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Subdivisionary.Models;

namespace Subdivisionary.Dtos
{
    /// <summary>
    /// Data-Transfer Object, that is much like Application Status Log Item
    /// but without the ID parameter.
    /// </summary>
    public class ApplicationStatusLogItemDtoSet
    {
        /// <summary>
        /// Item Status
        /// </summary>
        [Required]
        public EApplicationStatus Status { get; set; }
        /// <summary>
        /// Date of Item Creation
        /// </summary>
        public DateTime DateTime { get; set; }
        /// <summary>
        /// Additional Info
        /// </summary>
        public string Comment { get; set; }
    }

    /// <summary>
    /// Data-Transfer Object, that is much like Application Status Log Item
    /// but without the ID parameter.
    /// </summary>
    public class ApplicationStatusLogItemDtoGet
    {
        /// <summary>
        /// Id of Log Item (because it has its own table)
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Status of the designated application
        /// </summary>
        public EApplicationStatus Status { get; set; }

        /// <summary>
        /// Date of Item Creation
        /// </summary>
        public DateTime DateTime { get; set; }

        /// <summary>
        /// Additional Information
        /// </summary>
        public string Comment { get; set; }
    }
}