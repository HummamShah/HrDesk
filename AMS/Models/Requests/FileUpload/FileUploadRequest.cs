using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMS.Models.Requests.FileUpload
{
    public class FileUploadRequest
    {
    }
    public class FileUploadResponse
    {
        public bool Success { get; set; }
        public string Url { get; set; }
        public List<string> savedFilePath { get; set; }
        public string Path { get; set; }
        public List<string> ValidationErrors { get; set; }
    }
}