using LightSwitchApplication.Poco;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using Microsoft.LightSwitch;
using Microsoft.LightSwitch.Details;
using System.Web;
using System.Configuration;

namespace LightSwitchApplication
{
    public class FileController : LightSwitchApiControllerbase
    {
        const int maxSize = 100 * 1024 * 1024;

        [HttpPost]
        public HttpResponseMessage Upload(FileUploadRequestParameters requestParameters)
        {
            string rootDir = ConfigurationManager.AppSettings["rootDir"];
            FilePathInfo fs = new FilePathInfo();
            FileUploadResponseParameters responseParameters = new FileUploadResponseParameters();

            using (var ctx = this.Context) //make sure to always put the context in a using !!!
            {
                var fileMetaData = ctx.DataWorkspace.ApplicationData.si_application_adv_files_SingleOrDefault(requestParameters.RecordId);
                var check = ctx.DataWorkspace.ApplicationData.si_application_adv_details_SingleOrDefault(requestParameters.RecordId);
                if (fileMetaData == null)
                {
                    fileMetaData = ctx.DataWorkspace.ApplicationData.si_application_adv_files.AddNew();
                    fileMetaData.application_adv_detail = ctx.DataWorkspace.ApplicationData.si_application_adv_details_SingleOrDefault(requestParameters.RecordId);
                }


                IEntitySet entitySet
                    = ctx.DataWorkspace.ApplicationData.Details.Properties[requestParameters.ReferencedEntitySet].Value
                    as IEntitySet;
                if (entitySet != null)
                {
                    string entityTypeName = entitySet.Details.EntityType.Name;

                    ICreateQueryMethod query
                        = ctx.DataWorkspace.ApplicationData.
                        Details.Methods[entitySet.Details.Name + "_SingleOrDefault"] as ICreateQueryMethod;
                    if (query != null)
                    {

                        object[] keySegment = new object[] { requestParameters.RecordId };
                        ICreateQueryMethodInvocation invocation = query.CreateInvocation(keySegment);
                        var myvalue = invocation.Execute() as IEntityObject;

                        string dir = String.Format("{0}{1}/", rootDir, requestParameters.ReferenceId);
                        //try
                        //{
                        fs = saveFileToDirectory(dir, requestParameters.FileName, requestParameters.BinaryData);
                        fileMetaData.advFileName = fs.fileName;
                        fileMetaData.advFileDirectory = fs.fileDirectory;
                        
                        ctx.DataWorkspace.ApplicationData.SaveChanges();
                        responseParameters.UploadStatus = "ok";
                        responseParameters.FileSize = "";
                        //}
                        //catch (IOException ex)
                        //{
                        //    responseParameters.FileSize = "";
                        //    responseParameters.UploadStatus = "fail";
                        //}                     


                        return Request.CreateResponse<FileUploadResponseParameters>(HttpStatusCode.Accepted, responseParameters);
                    }
                    else
                    {
                        throw new HttpResponseException(HttpStatusCode.NotFound);
                    }

                }
                else
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }
            }
        }

        [HttpGet]
        public HttpResponseMessage Download() //(int id)
        {
            int id = 1;
            MediaTypeHeaderValue _mediaType = MediaTypeHeaderValue.Parse("application/octet-stream");
            try
            {
                using (var ctx = this.Context)
                {
                    var myFileStoreEntry = ctx.DataWorkspace.ApplicationData.si_application_adv_files_SingleOrDefault(id);

                    if (myFileStoreEntry == null)
                    {
                        throw new HttpResponseException(HttpStatusCode.NotFound);
                    }

                    else
                    {
                        //string test2 = HttpContext.Current.Server.MapPath(myFileStoreEntry.filePath+myFileStoreEntry.fileName);
                        //var test = File.Exists(HttpContext.Current.Server.MapPath(myFileStoreEntry.filePath + myFileStoreEntry.fileName));
                        string tempt = HttpContext.Current.Server.MapPath(myFileStoreEntry.advFileDirectory + myFileStoreEntry.advFileName);
                        if (File.Exists(HttpContext.Current.Server.MapPath(myFileStoreEntry.advFileDirectory + myFileStoreEntry.advFileName)))
                        {
                            byte[] myBytes = File.ReadAllBytes(tempt);
                            string fileName = myFileStoreEntry.advFileName;
                            MemoryStream memStream = new MemoryStream(myBytes);
                            HttpResponseMessage fullResponse = Request.CreateResponse(HttpStatusCode.OK);
                            fullResponse.Content = new StreamContent(memStream);
                            fullResponse.Content.Headers.ContentType = _mediaType;
                            fullResponse.Content.Headers.ContentDisposition
                                = new ContentDispositionHeaderValue("fileName") { FileName = fileName };
                            return fullResponse;
                        }
                        else
                            return (HttpResponseMessage)Request.CreateResponse(HttpStatusCode.BadRequest);

                    }

                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

        }

        private double ConvertBytesToMegabytes(long bytes)
        {
            return (bytes / 1024f) / 1024f;
        }
        public FilePathInfo saveFileToDirectory(string directory, string fileName, byte[] BinaryData)
        {
            FilePathInfo savedFile = new FilePathInfo();
            string newFileName = String.Format("{0}_{1}_{2}"
                , Path.GetFileNameWithoutExtension(fileName)
                , DateTime.Now.ToString("ddMMMyy")
                , Path.GetExtension(fileName)
                );

            //currentFile.fileDirectory = directory;
            //currentFile.fileName = fileName;
            //currentFile.fullFilePath = directory + fileName;

            var test = Directory.Exists(HttpContext.Current.Server.MapPath(directory));
            var test2 = HttpContext.Current.Server.MapPath(directory);
            if (!Directory.Exists(HttpContext.Current.Server.MapPath(directory)))
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath(directory));


            File.WriteAllBytes(HttpContext.Current.Server.MapPath(directory) + newFileName, BinaryData);

            savedFile.fileDirectory = directory;
            savedFile.fileName = newFileName;
            return savedFile;
        }


    }
}