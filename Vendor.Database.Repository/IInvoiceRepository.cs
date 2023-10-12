using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vendor.Database.Identity;

namespace Vendor.Database.Repository
{
    public interface IInvoiceRepository
    {
        Task<bool> AddVendorInvoice(VendorFile vendorFile);

        Task<IEnumerable<VendorFile>> GetInvoiceByVendorId(string userId);

        Task<VendorFile> GetInvoice(long id);
        Task<bool> DeleteInvoice(long id);
        Task<bool> UpdateInvoice(long id, VendorFile vendorFile);
    }
}
