using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AMS.Controllers
{
    public class FileUploadController : Controller
    {
        // GET: FileUpload
        public ActionResult Index()
        {
            return View();
        }
        public class ImageUploadResponse
        {
            public bool Success { get; set; }
            public List<string> ValidationErrors { get; set; }
            public List<string> Urls { get; set; }
        }
        [HttpPost]
        public object UploadImages()
        {
            var resp = new ImageUploadResponse();
            resp.ValidationErrors = new List<string>();
            resp.Success = true;
            resp.Urls = new List<String>();
            string newFileName = "";
            try
            {
                var path = Server.MapPath("~/Uploads/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                foreach (string key in Request.Files)
                {

                    HttpPostedFileBase postedFiles = Request.Files[key];
                    // postedFiles.FileName = postedFiles.FileName + Guid
                    // postedFiles.SaveAs(path + postedFiles.FileName);
                    string extension = Path.GetExtension(postedFiles.FileName);
                    newFileName = Guid.NewGuid() + extension;
                    string filePath = Path.Combine(path, newFileName);
                    postedFiles.SaveAs(filePath);
                    var url = Request.Url.Scheme + "://" + HttpContext.Request.Url.Authority + "/Uploads/" + newFileName;
                    resp.Urls.Add(url);
                }
            }
            catch (Exception e)
            {
                resp.Success = false;
                resp.ValidationErrors.Add(e.Message);
            }

            return Json(resp);
        }
    }
}