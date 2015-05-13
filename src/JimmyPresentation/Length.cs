namespace JimmyPresentation
{
    public struct Length
    {
        private readonly double _value;

        private Length(double value)
        {
            _value = value;
        }

        public static Length operator +(Length l1, Length l2)
        {
            return new Length(l1._value + l2._value);
        }

        public static Length operator -(Length l1, Length l2)
        {
            return new Length(l1._value - l2._value);
        }

        public static Length operator *(Length length, double factor)
        {
            return new Length(length._value*factor);
        }

        public static Length operator /(Length length, double factor)
        {
            return new Length(length._value/factor);
        }

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

        public double Yards
        {
            get { return _value / 0.9144; }
        }

        public static Length FromMeters(double value)
        {
            return new Length(value);
        }

        public static Length FromCentimeters(double value)
        {
            return new Length(value/100);
        }

        public static Length FromMillimeters(double value)
        {
            return new Length(value/1000);
        }

        public static Length FromKilometers(double value)
        {
            return new Length(value*1000);
        }

        public static Length FromInches(double value)
        {
            return new Length(value * 0.0254);
        }

        public static Length FromFeet(double value)
        {
            return new Length(value * 0.3048);
        }

        public static Length FromMiles(double value)
        {
            return new Length(value*1609.3440);
        }

        public static Length FromYards(double value)
        {
            return new Length(value*0.9144);
        }

        public static Length FromDpi(double value, double dpi)
        {
            return FromInches(value/dpi);
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("{0:0.000} meters", _value);
        }

        public override bool Equals(object obj)
        {
            if (obj is Length)
                return _value == ((Length) obj)._value;
            return false;
        }

    }

}
