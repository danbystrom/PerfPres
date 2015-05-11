using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JimmyPresentation
{
    public static class TestLength
    {
        public static void FillMetersArray(double[] array)
        {
            for (var i = 0; i < array.Length; i++)
                array[i] = i;
        }

        public static void FillLengthArray(Length[] array)
        {
            for (var i = 0; i < array.Length; i++)
                array[i] = Length.FromMeters(i);
        }

        public static void FillLengthClassArray(LengthClass[] array)
        {
            for (var i = 0; i < array.Length; i++)
                array[i] = LengthClass.FromMeters(i);
        }

        public static IEnumerable<double> ConverToMiles(IEnumerable<double> meters)
        {
            return meters.Select(_ => _/1609.3440);
        }

        public static IEnumerable<double> ConverToMiles(IEnumerable<Length> lengths)
        {
            return lengths.Select(_ => _.Miles);
        }

        public static IEnumerable<double> ConverToMiles(IEnumerable<LengthClass> lengths)
        {
            return lengths.Select(_ => _.Miles);
        }

    }

}
