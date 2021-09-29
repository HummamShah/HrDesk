using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMS.Models.Requests.FileUpload
{
    public class Document
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Url { get; set; }
        public DateTime? UploadedOn{ get; set; }
        public int? UploadedBy{ get; set; }
    }
}