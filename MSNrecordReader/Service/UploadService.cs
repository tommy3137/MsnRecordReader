using Microsoft.AspNetCore.Http;
using MSNrecordReader.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace MSNrecordReader.Service
{
    public class UploadService : IUploadService
    {
        public List<MsnViewModel> UploadXml(IFormFile file)
        {
            List<MsnViewModel> result = new List<MsnViewModel>();

            string jsonText = string.Empty;
            XElement root = XElement.Load(file.OpenReadStream());
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(root.ToString());
            XmlNode resultLog = xmlDoc.SelectSingleNode("/Log");
            jsonText = JsonConvert.SerializeXmlNode(resultLog).Replace("@", "").Replace("#", "");
            MsnParseModel Result = JsonConvert.DeserializeObject<MsnParseModel>(jsonText);
            foreach (var obj in Result.LOG.MESSAGE)
            {
                result.Add(new MsnViewModel { Date = obj.DATE, Time = obj.TIME, From = obj.FROM.USER.FriendlyName, Text = obj.TEXT.text });
            }

            return result;

        }

    }
}
