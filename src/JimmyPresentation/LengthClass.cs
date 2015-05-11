namespace JimmyPresentation
{
    public class LengthClass
    {
        private double _value;

        public double Meters
        {
            get { return _value; }
        }

        public double Centimeters
        {
            get { return _value * 100; }
        }

        public double Millimeters
        {
            get { return _value * 1000; }
        }

        public double Kilometers
        {
            get { return _value / 1000; }
        }

        public double Inches
        {
            get { return _value / 0.0254; }
        }

        public double Feet
        {
            get { return _value / 0.3048; }
        }

        public double Miles
        {
            get { return _value / 1609.3440; }
        }

        private LengthClass(double value)
        {
            _value = value;
        }

        public static LengthClass FromMeters(double value)
        {
            return new LengthClass(value);
        }

        public static LengthClass FromCentimeters(double value)
        {
            return new LengthClass(value * 100);
        }

        public static LengthClass FromMillimeters(double value)
        {
            return new LengthClass(value * 1000);
        }

        public static LengthClass FromKilometers(double value)
        {
            return new LengthClass(value / 1000);
        }

        public static LengthClass FromInches(double value)
        {
            return new LengthClass(value * 0.0254);
        }

        public static LengthClass FromFeet(double value)
        {
            return new LengthClass(value * 0.3048);
        }

        public static LengthClass FromMiles(double value)
        {
            return new LengthClass(value * 1609.3440);
        }

    }

}
