using Microsoft.LightSwitch;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System;

namespace LightSwitchApplication
{
    public partial class si_application_adv_detail
    {
        partial void fileName_Compute(ref string result)
        {
            // Set result to the desired field value
            if (this.si_application_adv_file != null)
            {
                result = this.si_application_adv_file.advFileName;
            }
            else
                result = " ";

        }
    }
}