using Microsoft.LightSwitch.Presentation.Extensions;
using Microsoft.LightSwitch.Presentation;
using Microsoft.LightSwitch.Threading;
using System.Runtime.InteropServices.Automation;
using System.Windows;
using Microsoft.LightSwitch.Framework.Client;
using Microsoft.LightSwitch;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.IO;
using System.Linq;
using System;
using System.Windows.Controls;
using LightSwitchApplication.Poco;
using LightSwitchApplication.UserCode;

namespace LightSwitchApplication
{
    public partial class Applications
    {
        partial void Applications_Created()
        {
            //var mediaGridControl = this.FindControl("si_application_mediaCollection");
            //mediaGridControl.ControlAvailable += MediaGridControl_ControlAvailable;    


        }

        partial void FileDownload_Execute()
        {

            Dispatchers.Main.Invoke(()=>
            {
                SelectFileWindow selectFileWindow = new SelectFileWindow();
                selectFileWindow.Closed += SelectFileWindow_Closed;
                selectFileWindow.Show();
            }
                );

        }

        private void SelectFileWindow_Closed(object sender, EventArgs e)
        {
            SelectFileWindow selectFileWindow = (SelectFileWindow)sender;
            byte[] fileData = new byte[selectFileWindow.DocumentStream.Length];
            string fileName = String.Empty;

            if (selectFileWindow.DialogResult == true && selectFileWindow.DocumentStream != null)
            {                
                using (StreamReader streamReader = new StreamReader(selectFileWindow.DocumentStream))
                {
                    for (int i = 0; i < selectFileWindow.DocumentStream.Length; i++)
                    {
                        fileData[i] = (byte)selectFileWindow.DocumentStream.ReadByte();
                    }
                }
                
            }
            fileName = selectFileWindow.FileName;
            if (fileData != null)
                this.StartWebApiCommand<FileUploadResponseParameters>("api/File/Upload",
                                   new FileUploadRequestParameters { BinaryData = fileData, FileName = fileName, ReferenceId = this.si_applications.SelectedItem.application_id, ReferencedEntitySet = "si_application_mediaSet" , RecordId = this.si_application_mediaCollection.SelectedItem.Id},
                                   (error, responseParams) =>
                                   {
                                       //IsBusy = false;
                                       //SelectedFileName = fileName;
                                       this.si_application_mediaCollection.Refresh();

                                       if (error != null || responseParams.UploadStatus != "ok")
                                           this.ShowMessageBox("Something went wrong...");
                                       //    SelectedFileName = "Something went wrong...";
                                   });
        }

        
    }
}