using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vendor.Database.Identity;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Vendor.Database
{
    public class VendorFile
    {
        [Key]
        public long Id { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public DateTime CreationDate { get; set; }
        public string FileUrl { get; set; }
        public string FileName { get; set; }

        public string PoNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string InvoiceNumber { get; set; }

        public bool IsSentEmail { get; set; }


    }
}
