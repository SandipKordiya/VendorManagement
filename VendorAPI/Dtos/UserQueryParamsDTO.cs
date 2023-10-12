using Microsoft.AspNetCore.Mvc;

namespace VendorAPI.Dtos
{
    public class UserQueryParamsDTO
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        [FromQuery] public string? Search { get; set; }
    }
}
