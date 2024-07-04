using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_Management_System.Data;
using Project_Management_System.Models;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Project_Management_System.Controllers.FileUploadControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectFileUploadController : ControllerBase
    {
        private readonly DataContext _context;

        public ProjectFileUploadController(DataContext context)
        {
            _context = context;
        }

        private async Task<string> WriteFile(IFormFile file)
        {
            string fileName = "";

            try
            {
                var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                fileName = DateTime.Now.Ticks.ToString() + extension;
                Console.WriteLine("filename: " + fileName);

                var filePath = Path.Combine("C:\\Users\\ranam\\OneDrive\\Desktop\\projectdocs");

                Console.WriteLine("filepath: " + filePath);

                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }

                var exactPath = Path.Combine(filePath, fileName);

                Console.WriteLine("exactpath: " + exactPath);

                using (var stream = new FileStream(exactPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Not uploaded: " + ex.Message);
            }
            return fileName;
        }

        [HttpPost]
        [Route("BasicInfo")]
        public async Task<IActionResult> UploadBasicInfo(IFormFile file, int ProID)
        {
            var result = WriteFile(file);

            var newfile = new FileResource
            {
                FileName = file.FileName,
                FileType = "basic",
                LocalStoragePath = await result,
                UpdatedDate = DateTime.Now,
                ProjectId = ProID,
                TaskId = null
            };

            _context.Add(newfile);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        [Route("TimeLine")]
        public async Task<IActionResult> UploadTimeLine(IFormFile file, int ProID)
        {
            var result = WriteFile(file);

            var newfile = new FileResource
            {
                FileName = file.FileName,
                FileType = "timeline",
                LocalStoragePath = await result,
                UpdatedDate = DateTime.Now,
                ProjectId = ProID,
                TaskId = null
            };

            _context.Add(newfile);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        [Route("BudgetInfo")]
        public async Task<IActionResult> UploadBudgetInfo(IFormFile file, int ProID)
        {
            var result = WriteFile(file);

            var newfile = new FileResource
            {
                FileName = file.FileName,
                FileType = "budgetinfo",
                LocalStoragePath = await result,
                UpdatedDate = DateTime.Now,
                ProjectId = ProID,
                TaskId = null
            };

            _context.Add(newfile);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        [Route("ClientDoc")]
        public async Task<IActionResult> UploadClientDoc(IFormFile file, int ProID)
        {
            var result = WriteFile(file);

            var newfile = new FileResource
            {
                FileName = file.FileName,
                FileType = "clientdoc",
                LocalStoragePath = await result,
                UpdatedDate = DateTime.Now,
                ProjectId = ProID,
                TaskId = null
            };

            _context.Add(newfile);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        [Route("PhysicalPayment")]
        public async Task<IActionResult> UploadPhysicalPayment(IFormFile file, int ProID, DateTime paymentDate, int paymentAmount)
        {
            var result = WriteFile(file);

            var newfile = new FileResource
            {
                FileName = file.FileName,
                FileType = "payment",
                LocalStoragePath = await result,
                UpdatedDate = DateTime.Now,
                ProjectId = ProID,
                TaskId = null
            };

            _context.Add(newfile);
            await _context.SaveChangesAsync();

            var newPayment = new ClientPayment
            {
                Payment = paymentAmount,
                PaymentDate = paymentDate,
                Status = true,
                ProjectId = ProID
            };

            _context.Add(newPayment);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
