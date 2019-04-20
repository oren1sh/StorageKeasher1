using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Asat_Technion.Areas.Tickets.Data.Models;
using Asat_Technion.Areas.Tickets.Data.Repositories;
using Asat_Technion.Areas.Tickets.Models;
using Asat_Technion.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace Asat_Technion.Areas.Tickets.Controllers
{
    [Area("Tickets")]
    public class TicketAllocationMongosController : Controller
    {
        
        private readonly ApplicationDbContext _context;
        private readonly MongoDbContext mongoDbContext;
        private readonly TicketRepository ticketRepository;
        private readonly TicketAllocationMongoRepository ticketAllocationMongoRepository;
        private readonly TicketAllocationRepository ticketAllocationRepository;
        private readonly IHostingEnvironment hostingEnvironment;
        private string projectRootPath,webRootPath;
        public TicketAllocationMongosController(ApplicationDbContext context,MongoDbContext mongoDbContext, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
           // ticketRepository = new TicketRepository(context);
            this.mongoDbContext = mongoDbContext;
            ticketAllocationMongoRepository = new TicketAllocationMongoRepository(mongoDbContext);
         //   ticketAllocationRepository = new TicketAllocationRepository(context);
            this.hostingEnvironment = hostingEnvironment;
            projectRootPath =  this.hostingEnvironment.ContentRootPath;
            webRootPath = this.hostingEnvironment.WebRootPath;
        }
        public IActionResult Index()
        {
            return View(mongoDbContext.Get());
            //return View(await ticketAllocationMongoRepository.GetAllTicketAllocationsByLandingPageIdAsync("3dc9c4d9-d62a-40fd-81e4-72e7dac8e9d3"));
        }//end index
        public IActionResult Upload()
        {
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> UpdateTicketAllocation([FromBody]TicketAllocationUpdate ticket)
        {
            try
            {
                await ticketRepository.UpdateTicketAllocationIdAsync(ticket.ticketId, ticket.ticketAllocationId);
                return Json(new { success = true });
            }
            catch(Exception e)
            {
                return Json(new { success = false ,msg=e.Message});
            }

        }
        

        [HttpPost]
        public async Task<JsonResult> UploadFile(IFormFile formFile,string landingPageId,string ticketId)
        {
            // full path to file in temp location
            var filePath = Path.Combine(projectRootPath, "Data");
            filePath = Path.Combine(filePath,"Uploads");
            filePath = Path.Combine(filePath, "Excels");
            //string guid = Guid.NewGuid().ToString() + DateTime.Now.ToString("dd-mm-yyyy-HH:MM");
            string guid = Guid.NewGuid().ToString();
            filePath = Path.Combine(filePath, guid);
            string fileName="";
            if (formFile.Length > 0)
            {
                fileName = formFile.FileName;
                filePath += formFile.FileName;
                filePath = filePath.Replace(" ", "");
                using (var stream = new FileStream(filePath,FileMode.Create))
                {
                    await formFile.CopyToAsync(stream);
                    //System.IO.File.WriteAllBytes(filePath, stream.ToArray());
                }
            }
            else
                return Json(new { faild = true });
            TicketAllocation ticketAllocation = await ticketAllocationRepository.AddNewAllocationAsync(fileName, landingPageId);
            await LoadExcelAsync(filePath);
            //update the sql
            await ticketRepository.UpdateTicketAllocationIdAsync(ticketId,ticketAllocation.TicketAllocationId);
            return Json(new { fileName = ticketAllocation.TicketAllocationName});

        }//end upload post

        async Task LoadExcelAsync(string filePath)
        {
            List<TicketAllocationMongo> list = new List<TicketAllocationMongo>();
            var dtContent = GetDataTableFromExcel(filePath);
            TicketAllocationMongo current;
            string identifier;
            int amount;
            foreach (DataRow dr in dtContent.Rows)
            {
                identifier = dr[0].ToString();
                amount = int.Parse(dr[1].ToString());
                current = new TicketAllocationMongo {
                    LandingPageId = "fd8c412b-c1ef-4cb0-bcc8-b8b744b8b900",
                    TicketAllocationId = "a2b210bf-d84a-40c1-b100-710978d3a56f	".Trim(),
                    IdentifierId = identifier,
                    BeginAmount = amount,
                    CurrentAmount = amount
                };
                //Debug.WriteLine(dr[0].ToString());
                list.Add(current);
            }
            await mongoDbContext.TicketAllocationCollection.InsertManyAsync(list);
            Debug.WriteLine("total = " + list.Count);
            list.Clear();
        }
        private static DataTable GetDataTableFromExcel(string path, bool hasHeader = true)
        {
            using (var pck = new OfficeOpenXml.ExcelPackage())
            {
                using (var stream = System.IO.File.OpenRead(path))
                {
                    pck.Load(stream);
                }
                var ws = pck.Workbook.Worksheets.First();
                DataTable tbl = new DataTable();
                foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
                {
                    tbl.Columns.Add(hasHeader ? firstRowCell.Text : string.Format("Column {0}", firstRowCell.Start.Column));
                }
                var startRow = hasHeader ? 2 : 1;
                for (int rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
                {
                    var wsRow = ws.Cells[rowNum, 1, rowNum, ws.Dimension.End.Column];
                    DataRow row = tbl.Rows.Add();
                    foreach (var cell in wsRow)
                    {
                        row[cell.Start.Column - 1] = cell.Text;
                    }
                }
                return tbl;
            }
        }
    }//end controller
    public class TicketAllocationUpdate
    {
        public string ticketId { get; set; }
        public string ticketAllocationId { get; set; }
    }
}//end namespace