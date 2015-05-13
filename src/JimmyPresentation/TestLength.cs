using System.Collections.Generic;
using System.Linq;

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

        public static void FillDoublePlusUnitArrayWithMeters(DoublePlusUnit[] array)
        {
            for (var i = 0; i < array.Length; i++)
                array[i] = new DoublePlusUnit {Unit = Units.Meters, Value = i};
        }

        public static IEnumerable<double> ConvertToMiles(IEnumerable<double> meters)
        {
            return meters.Select(_ => _/1609.3440);
        }

        public static IEnumerable<double> ConvertToMiles(IEnumerable<Length> lengths)
        {
            return lengths.Select(_ => _.Miles);
        }

        public static IEnumerable<double> ConvertToMiles(IEnumerable<LengthClass> lengths)
        {
            return lengths.Select(_ => _.Miles);
        }

        public static IEnumerable<double> ConvertToMiles(IEnumerable<DoublePlusUnit> lengths)
        {
            var result = new List<double>();
            foreach (var dpu in lengths)
            {
                if (dpu.Unit == Units.Meters)
                    result.Add(dpu.Value / 1609.3440);
                else
                    result.Add(dpu.Value);
            }
            return result;
        }

    }

}
