using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LightSwitchApplication.Poco;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using Microsoft.LightSwitch;
using Microsoft.LightSwitch.Details;
using System.Configuration;

namespace LightSwitchApplication
{
    public partial class DownloadFile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Request.QueryString["id"] != null)
            {
                int RecordId = int.Parse(Request.QueryString["id"].ToString());
                using (ServerApplicationContext ctx = ServerApplicationContext.CreateContext())
                {
                    if (ctx.Application.User.HasPermission(Permissions.CanViewAppData))
                    {
                        var fileDet = ctx.DataWorkspace.ApplicationData.si_application_adv_files_SingleOrDefault(RecordId);
                        if (fileDet == null)
                        {
                            throw new HttpResponseException(HttpStatusCode.NoContent);
                        }
                        else
                        {
                            HttpResponse response = HttpContext.Current.Response;
                            response.ContentType = "application/octet-stream";
                            Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileDet.advFileName);
                            response.TransmitFile(Server.MapPath(fileDet.advFileDirectory+fileDet.advFileName));
                            response.End();
                        }
                    }
                }
            }

        }
    }
}