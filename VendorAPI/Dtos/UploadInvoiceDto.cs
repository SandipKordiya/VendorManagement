using System.ComponentModel.DataAnnotations;

namespace VendorAPI.Dtos
{
    public class UploadInvoiceDto
    {
        [Required]
        public string UserId { get; set; }

        public IFormFile? InvoiceFile { get; set; }

        public string PoNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string InvoiceNumber { get; set; }

        public bool IsSentEmail { get; set; } = false;
    }
}
