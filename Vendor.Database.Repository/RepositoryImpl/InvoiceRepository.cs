using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vendor.Database.Identity;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Vendor.Database.Repository.RepositoryImpl
{
    public class InvoiceRepository : IInvoiceRepository
    {

        #region Variables
        //---------------------------------------------------------------------------
        //inject db contect
        ApplicationDbContext _dbContext;
        //---------------------------------------------------------------------------
        #endregion

        #region Constructor
        //---------------------------------------------------------------------------
        public InvoiceRepository(ApplicationDbContext _db)
        {
            _dbContext = _db;
        }
        #endregion

        #region Methods
        //---------------------------------------------------------------------------
        public async Task<bool> AddVendorInvoice(VendorFile vendorFile)
        {
            if (_dbContext == null)
                return false;
            await _dbContext.VendorFiles.AddAsync(vendorFile);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteInvoice(long id)
        {
            if (_dbContext == null)
                return false;

            var item =await _dbContext.VendorFiles.FirstOrDefaultAsync(x => x.Id == id);
            if(item != null)
            {
                _dbContext.VendorFiles.Remove(item);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        //---------------------------------------------------------------------------

        public async Task<VendorFile> GetInvoice(long id)
        {
            if (_dbContext == null)
                return null;
            var invoice = await _dbContext.VendorFiles.FindAsync(id);
            return invoice;
        }
        //---------------------------------------------------------------------------

        public async Task<IEnumerable<VendorFile>> GetInvoiceByVendorId(string userId)
        {
            if (_dbContext == null)
                return null;

            var tmp = await _dbContext.VendorFiles.Where(c => c.UserId == userId).ToListAsync();
            return tmp;
        }

        public async Task<bool> UpdateInvoice(long id, VendorFile vendorFile)
        {
            if (_dbContext == null)
                return false;
            _dbContext.VendorFiles.Update(vendorFile);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        //---------------------------------------------------------------------------

        #endregion
    }
}
