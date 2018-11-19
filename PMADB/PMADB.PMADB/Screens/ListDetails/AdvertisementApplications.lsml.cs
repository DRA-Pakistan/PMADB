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
using System.Windows.Browser;

namespace LightSwitchApplication
{
    public partial class AdvertisementApplications
    {
        

        private void SelectFileWindow_Closed(object sender, EventArgs e)
        {
            SelectFileWindow selectFileWindow = (SelectFileWindow)sender;
            string fileName = String.Empty;

            if (selectFileWindow.DialogResult == true && selectFileWindow.DocumentStream != null)
            {
                try
                {
                    byte[] fileData = new byte[selectFileWindow.DocumentStream.Length];
                    using (StreamReader streamReader = new StreamReader(selectFileWindow.DocumentStream))
                    {
                        for (int i = 0; i < selectFileWindow.DocumentStream.Length; i++)
                        {
                            fileData[i] = (byte)selectFileWindow.DocumentStream.ReadByte();
                        }
                    }
                    fileName = selectFileWindow.FileName;
                    if (fileData != null)
                        this.StartWebApiCommand<FileUploadResponseParameters>("api/File/Upload",
                                           new FileUploadRequestParameters { BinaryData = fileData, FileName = fileName, ReferenceId = this.si_applications.SelectedItem.application_id, ReferencedEntitySet = "si_application_adv_details", RecordId = this.si_application_mediaCollection.SelectedItem.Id},
                                           (error, responseParams) =>
                                           {
                                           //IsBusy = false;
                                           //SelectedFileName = fileName;
                                           this.si_application_mediaCollection.Refresh();

                                               if (error != null || responseParams.UploadStatus != "ok")
                                                   this.ShowMessageBox(error.ToString());//"Something went wrong...\nUpload Failed...");
                                           //    SelectedFileName = "Something went wrong...";
                                       });
                }
                catch (IOException ex)
                {
                    this.ShowMessageBox(ex.Message, "I/O Error", MessageBoxOption.Ok);
                }
                

            }
        }

        partial void FileUpload_Execute()
        {
            Dispatchers.Main.Invoke(() =>
            {
                SelectFileWindow selectFileWindow = new SelectFileWindow();
                selectFileWindow.Closed += SelectFileWindow_Closed;
                selectFileWindow.Show();
            }
                );

        }

        partial void FileDownload_Execute()
        {
            //Dispatchers.Main.Invoke(() =>
            //{
            //    Uri baseAddress = new Uri(new Uri(System.Windows.Application.Current.Host.Source.AbsoluteUri), "../../"); //works both in debug and deployed !
            //    string url = string.Format(@"{0}api/File/Download?Id={1}", baseAddress.AbsoluteUri, this.si_application_mediaCollection.SelectedItem.Id);
            //    this.ShowMessageBox(url);
            //    HtmlPage.Window.Navigate(new Uri(url), "_blank");
            //});
            
            Dispatchers.Main.Invoke(() =>
            {
                Uri baseAddress = new Uri(new Uri(System.Windows.Application.Current.Host.Source.AbsoluteUri), "../../"); //works both in debug and deployed !
                int id = this.si_application_mediaCollection.SelectedItem.si_application_adv_file.Id;
                HtmlPage.Window.Navigate(new Uri(string.Format(@"{0}DownloadFile.aspx?id={1}", baseAddress.AbsoluteUri, id)), "_blank");
            });

        }
    }
}