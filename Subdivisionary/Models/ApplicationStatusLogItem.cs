using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Subdivisionary.Models
{
    public class ApplicationStatusLogItem
    {
        public int Id { get; set; }

        public EApplicationStatus Status { get; set; }
        public DateTime DateTime { get; set; }
        public string Comment { get; set; }

        public static ApplicationStatusLogItem FactoryCreate(EApplicationStatus status, string comment ="")
        {
            return new ApplicationStatusLogItem()
            {
                Status = status,
                DateTime = DateTime.Now,
                Comment = comment
            };
        }
    }
}