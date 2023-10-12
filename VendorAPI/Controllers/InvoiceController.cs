using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http.Headers;
using Vendor.Database;
using Vendor.Database.Identity;
using Vendor.Database.Repository;
using Vendor.Database.Repository.RepositoryImpl;
using VendorAPI.Dtos;

namespace VendorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly ILogger<InvoiceController> _logger;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly string InvoiceUploadPath = "InvoiceFiles";

        public InvoiceController(ILogger<InvoiceController> logger, IWebHostEnvironment hostingEnvironment, IInvoiceRepository invoiceRepository)
        {
            _logger = logger;
            _hostingEnvironment = hostingEnvironment;
            _invoiceRepository = invoiceRepository;
        }

        [HttpGet(nameof(GetInvoices) + "/{userId}")]
        public async Task<ActionResult> GetInvoices(string userId)
        {
            try
            {
                var result = await _invoiceRepository.GetInvoiceByVendorId(userId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetInvoices error: {Message}", ex.Message);
                return BadRequest();
            }
        }

        [DisableRequestSizeLimit]
        public async Task<ActionResult<VendorFile>> UploadInvoice([FromForm] UploadInvoiceDto uploadInvoiceDto)
        {
            if (uploadInvoiceDto.InvoiceFile == null)
                return BadRequest("No files found in the request");

            try
            {
                string webRootPath = _hostingEnvironment.WebRootPath;
                string uploadsDir = Path.Combine(webRootPath, InvoiceUploadPath);

                // wwwroot/uploads/
                if (!Directory.Exists(uploadsDir))
                    Directory.CreateDirectory(uploadsDir);

                string fileName = ContentDispositionHeaderValue.Parse(uploadInvoiceDto.InvoiceFile.ContentDisposition).FileName.Trim('"');
                string RenameFile = Convert.ToString(Guid.NewGuid()) + "." + fileName.Split('.').Last();
                string fullPath = Path.Combine(uploadsDir, RenameFile);

                //OLD FILE UPLOAD CODE

                //var buffer = 1024 * 1024;
                //using var stream = new FileStream(fullPath, FileMode.Create, FileAccess.Write, FileShare.None, buffer, useAsync: false);
                //await uploadInvoiceDto.InvoiceFile.CopyToAsync(stream);
                //await stream.FlushAsync();


                using (var stream = System.IO.File.Create(fullPath))
                {
                    await uploadInvoiceDto.InvoiceFile.CopyToAsync(stream);
                }

                var vendorFile = new VendorFile
                {
                    UserId = uploadInvoiceDto.UserId,
                    FileUrl = Path.Combine(InvoiceUploadPath, RenameFile),
                    FileName = RenameFile,
                    CreationDate = DateTime.Now,
                    PoNumber = uploadInvoiceDto.PoNumber,
                    InvoiceDate = uploadInvoiceDto.InvoiceDate == null ? DateTime.Now : uploadInvoiceDto.InvoiceDate,
                    InvoiceNumber = uploadInvoiceDto.InvoiceNumber,
                    IsSentEmail = uploadInvoiceDto.IsSentEmail
                };

                await _invoiceRepository.AddVendorInvoice(vendorFile);

                return Ok(vendorFile);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Upload failed: " + ex.Message);
            }

        }

        [DisableRequestSizeLimit]
        [HttpPut("update/{id}")]
        public async Task<ActionResult<VendorFile>> UpdateInvoice(long id, [FromForm] UploadInvoiceDto uploadInvoiceDto)
        {
           
            try
            {
                var invoice = await _invoiceRepository.GetInvoice(id);

                if (invoice == null) return BadRequest("Invoice not found");

                if (uploadInvoiceDto.InvoiceFile != null)
                {
                    string webRootPath = _hostingEnvironment.WebRootPath;
                    string uploadsDir = Path.Combine(webRootPath, InvoiceUploadPath);

                    // wwwroot/uploads/
                    if (!Directory.Exists(uploadsDir))
                        Directory.CreateDirectory(uploadsDir);

                    string fileName = ContentDispositionHeaderValue.Parse(uploadInvoiceDto.InvoiceFile.ContentDisposition).FileName.Trim('"');
                    string RenameFile = Convert.ToString(Guid.NewGuid()) + "." + fileName.Split('.').Last();
                    string fullPath = Path.Combine(uploadsDir, RenameFile);

                    using (var stream = System.IO.File.Create(fullPath))
                    {
                        await uploadInvoiceDto.InvoiceFile.CopyToAsync(stream);
                    }

                    invoice.FileUrl = Path.Combine(InvoiceUploadPath, RenameFile);
                    invoice.FileName = RenameFile;

                }

                //OLD FILE UPLOAD CODE

                //var buffer = 1024 * 1024;
                //using var stream = new FileStream(fullPath, FileMode.Create, FileAccess.Write, FileShare.None, buffer, useAsync: false);
                //await uploadInvoiceDto.InvoiceFile.CopyToAsync(stream);
                //await stream.FlushAsync();
                invoice.PoNumber = uploadInvoiceDto.PoNumber;
                invoice.InvoiceDate = uploadInvoiceDto.InvoiceDate == null ? uploadInvoiceDto.InvoiceDate : uploadInvoiceDto.InvoiceDate;
                invoice.InvoiceNumber = uploadInvoiceDto.InvoiceNumber;
                invoice.IsSentEmail = uploadInvoiceDto.IsSentEmail;

                await _invoiceRepository.UpdateInvoice(id, invoice);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Upload failed: " + ex.Message);
            }

        }

        [HttpGet("view/{id}")]
        public async Task<ActionResult> ExportView(long id)
        {
            try
            {
                var vendorFile = await _invoiceRepository.GetInvoice(id);
                string webRootPath = _hostingEnvironment.WebRootPath;
                string uploadsDir = Path.Combine(webRootPath, InvoiceUploadPath);
                string fullPath = Path.Combine(uploadsDir, vendorFile.FileName);
                byte[] bytes = System.IO.File.ReadAllBytes(fullPath);
                return File(bytes, "application/pdf");
            }
            catch (Exception ex) 
            {
                throw;
            }
            
        }

        [HttpGet("download/{id}")]
        public async Task<ActionResult> ExportDownload(long id)
        {
            try
            {
                var vendorFile = await _invoiceRepository.GetInvoice(id);
                string webRootPath = _hostingEnvironment.WebRootPath;
                string uploadsDir = Path.Combine(webRootPath, InvoiceUploadPath);
                string fullPath = Path.Combine(uploadsDir, vendorFile.FileName);
                byte[] bytes = System.IO.File.ReadAllBytes(fullPath);
                return File(bytes, "application/pdf", vendorFile.FileName);
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteInvoice(long id)
        {
            try
            {
                await _invoiceRepository.DeleteInvoice(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
