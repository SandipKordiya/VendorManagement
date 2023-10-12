using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vendor.Database.Identity
{
    public class User : IdentityUser<string>
    {

        public User()
        {
            VendorFiles = new HashSet<VendorFile>();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string  State { get; set; }  
        public string CompanyCode { get; set; }
        public string BotEmail { get; set; }
        public bool IsBotEmail { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public string PanNumber { get; set; }
        public DateTime Created { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<VendorFile> VendorFiles { get; set; }

    }
}
