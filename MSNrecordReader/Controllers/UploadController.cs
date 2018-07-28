using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using MSNrecordReader.Models;
using System.Xml;
using Newtonsoft.Json;
using MSNrecordReader.Service;

namespace MSNrecordReader.Controllers
{
    [Route("api/[controller]")]
    public class UploadController : Controller
    {
        private readonly IUploadService _uploadservice;

        public UploadController(IUploadService UploadService)
        {
            _uploadservice = UploadService;
        }

        public IActionResult UploadXml(List<IFormFile> files)
        {
            List<MsnViewModel> viewResult = null;
            foreach (var item in files)
            {
                if (item.Length > 0)
                {
                    if (Path.GetExtension(item.FileName) == ".xml")
                    {
                        viewResult = _uploadservice.UploadXml(item);
                    }
                }
            }
           
            return Json(viewResult);
         
        }

    }
}