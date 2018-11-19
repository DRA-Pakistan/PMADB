using Microsoft.LightSwitch.Presentation.Extensions;
using Microsoft.LightSwitch.Presentation;
using Microsoft.LightSwitch.Framework.Client;
using Microsoft.LightSwitch;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.IO;
using System.Linq;
using System;

namespace LightSwitchApplication
{
    public partial class MeetingsDetails
    {
        private ModalWindow addMeetingDetHelper;
        partial void MeetingsDetails_InitializeDataWorkspace(List<IDataService> saveChangesTo)
        {
            this.addMeetingDetHelper = new ModalWindow(si_meeting_decisions, "AddMeetingDetailsModal", "Meeting Detail");

            if (!this.Application.User.HasPermission(Permissions.AddAppData))
            {
                this.FindControl("si_meeting_decisions_AddAndEditNew").IsEnabled = false;
            }

        }

        partial void si_meeting_decisionsAddAndEditNew_CanExecute(ref bool result)
        {
            result = this.si_meetings.SelectedItem != null && this.addMeetingDetHelper.CanAdd();

        }

        partial void si_meeting_decisionsAddAndEditNew_Execute()
        {
            this.remainingApplications.Load();
            this.addMeetingDetHelper.AddEntity();

        }

        partial void si_meeting_decisionsEditSelected_CanExecute(ref bool result)
        {
            result = this.si_meetings.SelectedItem != null && this.addMeetingDetHelper.CanView() && this.Application.User.HasPermission(Permissions.EditAppData);

        }

        partial void si_meeting_decisionsEditSelected_Execute()
        {
            this.addMeetingDetHelper.ViewEntity();

        }

        partial void AddMetDet_Execute()
        {
            if (this.Details.ValidationResults.HasErrors == false)
            {
                this.addMeetingDetHelper.DialogOk();
                this.DataWorkspace.ApplicationData.SaveChanges();
            }
            else
            {
                string error = "";
                foreach (var msg in this.Details.ValidationResults)
                {
                    error = error + msg.Property.DisplayName + ":  " + msg.Message + System.Environment.NewLine;
                }
                this.ShowMessageBox(error, "Validation Error", MessageBoxOption.Ok);
            }

        }

        partial void CancelMetDet_Execute()
        {
            this.addMeetingDetHelper.DialogCancel();

        }

        partial void MeetingsDetails_Created()
        {
            this.addMeetingDetHelper.Initialise();

        }
    }
}