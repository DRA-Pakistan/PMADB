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
    public partial class Application
    {
        partial void ViewAuditTrail_CanRun(ref bool result)
        {
            result = this.User.HasPermission(Permissions.CanViewAudit);

        }

        partial void AdvertisementApplications_CanRun(ref bool result)
        {
            result = this.User.HasPermission(Permissions.CanViewAppData);
        }

        partial void MeetingsDetails_CanRun(ref bool result)
        {
            result = this.User.HasPermission(Permissions.CanViewAppData);

        }


        ////////////
        /// Screen Permission for Lookup Tables
        
        partial void AdvertisementPurposeGrid_CanRun(ref bool result)
        {
            result = this.User.HasPermission(Permissions.CanViewLookup);
        }

        partial void MediaTypesGrid_CanRun(ref bool result)
        {
            result = this.User.HasPermission(Permissions.CanViewLookup);
        }

        partial void ProductGrid_CanRun(ref bool result)
        {
            result = this.User.HasPermission(Permissions.CanViewLookup);
        }

        partial void ApplicantGrid_CanRun(ref bool result)
        {
            result = this.User.HasPermission(Permissions.CanViewLookup);

        }

        ///////////////////////////////////////////
    }
}