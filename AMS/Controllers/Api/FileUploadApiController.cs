using AMS.Models.Requests.FileUpload;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace AMS.Controllers.Api
{
    public class FileUploadApiController : ApiController
    {
        [System.Web.Http.HttpPost]
        public Task<object> UploadFile()
        {
            var resp = new FileUploadResponse();
            List<string> savedFilePath = new List<string>();
            resp.savedFilePath = new List<string>();
            // Check if the request contains multipart/form-data
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
            //Get the path of folder where we want to upload all files.
            string rootPath = HttpContext.Current.Server.MapPath("~/documents");
            var provider = new MultipartFileStreamProvider(rootPath);
            // Read the form data.
            //If any error(Cancelled or any fault) occurred during file read , return internal server error
            var task = Request.Content.ReadAsMultipartAsync(provider).
                ContinueWith<object>(t =>
                {
                    if (t.IsCanceled || t.IsFaulted)
                    {
                        Request.CreateErrorResponse(HttpStatusCode.InternalServerError, t.Exception);
                    }
                    foreach (MultipartFileData dataitem in provider.FileData)
                    {
                        try
                        {
                            //Replace / from file name
                            string name = dataitem.Headers.ContentDisposition.FileName.Replace("\"", "");
                            //Create New file name using GUID to prevent duplicate file name
                            string newFileName = Guid.NewGuid() + Path.GetExtension(name);
                            //Move file from current location to target folder.
                            File.Move(dataitem.LocalFileName, Path.Combine(rootPath, newFileName));
                            resp.savedFilePath.Add(rootPath + "\\" + newFileName);

                        }
                        catch (Exception ex)
                        {
                            string message = ex.Message;
                        }
                    }
                    Request.CreateResponse(HttpStatusCode.Created, savedFilePath);
                    return resp;
                });
            return task;
        }
    }
}