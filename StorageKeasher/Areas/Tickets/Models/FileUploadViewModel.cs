using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asat_Technion.Areas.Tickets.Models
{
    public class FileUploadViewModel
    {
        public IFormFile File { get; set; }
        public long Size { get; set; }
        public string Extension { get; set; }
        public string Source { get; set; }
    }//end class
}//end namespace
