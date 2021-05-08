
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Application.Helpers;
using Application.Users;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using OfficeOpenXml;

namespace API.Controllers
{
    public class ImportExportController : BaseApiController
    {

        private readonly IHostEnvironment _hostingEnvironment;

        public ImportExportController(IHostEnvironment hostingEnvironment)
        {
            this._hostingEnvironment = hostingEnvironment;
        }


        [HttpGet("export")]
        public async Task<TResponse<string>> Export(CancellationToken cancellationToken)
        {
            string folder = _hostingEnvironment.ContentRootPath + "/Media";
            string excelName = $"UserList-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";
            string downloadUrl = string.Format("{0}://{1}/Media/{2}", Request.Scheme, Request.Host, excelName);
            FileInfo file = new FileInfo(Path.Combine(folder, excelName));
            if (file.Exists)
            {
                file.Delete();
                file = new FileInfo(Path.Combine(folder, excelName));
            }

            var list = await Mediator.Send(new List.Query());

            using (var package = new ExcelPackage(file))
            {
                var workSheet = package.Workbook.Worksheets.Add("Sheet1");
                workSheet.Cells.LoadFromCollection(list, true);
                package.Save();
            }

            return TResponse<string>.GetResult(0, "OK", downloadUrl);
        }

        [HttpGet("exportFile")]
        public async Task<IActionResult> ExportFile(CancellationToken cancellationToken)
        {
            var list = await Mediator.Send(new List.Query());
            var stream = new MemoryStream();

            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add("Sheet1");
                workSheet.Cells.LoadFromCollection(list, true);
                package.Save();
            }
            stream.Position = 0;
            string excelName = $"UserList-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";

            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }

        [HttpPost("import")]
        public async Task<TResponse<List<User>>> Import(IFormFile formFile, CancellationToken cancellationToken)
        {
            if (formFile == null || formFile.Length <= 0)
            {
                return TResponse<List<User>>.GetResult(-1, "formfile is empty");
            }

            if (!Path.GetExtension(formFile.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
            {
                return TResponse<List<User>>.GetResult(-1, "Not Support file extension");
            }

            var list = new List<User>();

            using (var stream = new MemoryStream())
            {
                await formFile.CopyToAsync(stream, cancellationToken);

                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    var rowCount = worksheet.Dimension.Rows;

                    for (int row = 2; row <= rowCount; row++)
                    {
                        list.Add(new User
                        {
                            FirstName = worksheet.Cells[row, 3].Value.ToString().Trim(),
                            Surname = worksheet.Cells[row, 2].Value.ToString().Trim(),
                            Country = worksheet.Cells[row, 5].Value.ToString().Trim(),
                            City = worksheet.Cells[row, 6].Value.ToString().Trim(),

                        });
                    }
                }
            }

            return TResponse<List<User>>.GetResult(0, "OK", list);
        }
    }
}