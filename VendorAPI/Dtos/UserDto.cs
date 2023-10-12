using System.Collections.Generic;

namespace VendorAPI.Dtos
{
    public class UserDto
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }
        public string Roles { get; set; }
    }
}
