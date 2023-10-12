using Vendor.Database.Identity;
using System.Threading.Tasks;

namespace VendorAPI.Services
{
    public interface ITokenService
    {
        Task<string> CreateToken(User user);
    }
}
