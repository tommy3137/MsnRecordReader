using Microsoft.AspNetCore.Http;
using MSNrecordReader.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSNrecordReader.Service
{
    public interface IUploadService
    {
        List<MsnViewModel> UploadXml(IFormFile file);
    }
}
