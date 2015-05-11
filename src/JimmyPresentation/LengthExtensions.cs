using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JimmyPresentation
{
    public static class LengthExtensions
    {
        public static Length Miles(this double value)
        {
            return Length.FromMiles(value);
        }

        public static Length Miles(this int value)
        {
            return Length.FromMiles(value);
        }

        public static Length Yards(this double value)
        {
            return Length.FromYards(value);
        }

        public static Length Yards(this int value)
        {
            return Length.FromYards(value);
        }

    }
}
