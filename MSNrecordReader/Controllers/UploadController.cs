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

namespace MSNrecordReader.Controllers
{
    [Route("api/[controller]")]
    public class UploadController : Controller
    {
        private readonly string _uploadFolder;

        public UploadController (IHostingEnvironment hostingEnvironment)
        {
            _uploadFolder = $"{hostingEnvironment.WebRootPath}\\upload";
        }

        public IActionResult Post(List<IFormFile> files)
        {
            var size = files.Sum(x => x.Length);
            string jsonText = string.Empty;
            List<MsnViewModel> viewResult = new List<MsnViewModel>();
            foreach (var item in files)
            {
                if (item.Length > 0)
                {
                    if (Path.GetExtension(item.FileName) == ".xml")
                    {
                        try
                        {
                            XElement root = XElement.Load(item.OpenReadStream());
                            XmlDocument xmlDoc = new XmlDocument();
                            xmlDoc.LoadXml(root.ToString());
                            XmlNode resultLog = xmlDoc.SelectSingleNode("/Log");
                            jsonText = JsonConvert.SerializeXmlNode(resultLog).Replace("@", "").Replace("#","");
                            MsnParseModel Result = JsonConvert.DeserializeObject<MsnParseModel>(jsonText);
                            foreach (var obj in Result.LOG.MESSAGE)
                            {
                                viewResult.Add(new MsnViewModel { Date = obj.DATE, Time = obj.TIME, From = obj.FROM.USER.FriendlyName, Text = obj.TEXT.text });
                            }

                        }
                        catch (Exception ex)
                        {
                            ex.ToString();
                        }
                    }
                }
            }
            return Json( viewResult );
        }


    }
}