using Microsoft.LightSwitch;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System;

namespace LightSwitchApplication
{
    public partial class si_meeting_decision
    {
        partial void ApplicationID_Compute(ref string result)
        {
            result = this.si_applications.application_id;

        }
    }
}