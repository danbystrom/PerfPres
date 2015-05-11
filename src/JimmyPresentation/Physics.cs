using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JimmyPresentation
{
    public static class Physics
    {
        // version 1 - hur säkerställer vi att ingen skickar in en längd i en annan enhet
        public static string ReportDistance(double distanceInMeters)
        {
            return string.Format("Distance is {0:0.0} kilometers or {1:0.0} miles", distanceInMeters / 1000, distanceInMeters / 1609.344);
        }

        // version 2
        public static string ReportDistance(Length distance)
        {
            return string.Format("Distance is {0:0.0} kilometers or {1:0.0} miles", distance.Kilometers, distance.Miles);
        }

    }
}
