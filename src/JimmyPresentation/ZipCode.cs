namespace JimmyPresentation
{
    public struct ZipCode
    {
        private readonly int _value;

        private ZipCode(int value)
        {
            _value = value;
        }

        private ZipCode(string zip)
        {
            if (zip != null)
            {
                int.TryParse(zip.Replace(" ", ""), out _value);
                if (_value < 0 || _value > 99999)
                    _value = 0;
            }
            else
                _value = 0;
        }

        public static implicit operator string(ZipCode rhs)
        {
            return rhs._value != 0
                ? rhs._value.ToString("000 00")
                : string.Empty;
        }

        public static implicit operator int(ZipCode rhs)
        {
            return rhs._value;
        }

        public static implicit operator ZipCode(int rhs)
        {
            return new ZipCode(rhs);
        }

        public static implicit operator ZipCode(string rhs)
        {
            return new ZipCode(rhs);
        }

        public static bool operator ==(ZipCode x, ZipCode y)
        {
            return x._value == y._value;
        }

        public static bool operator !=(ZipCode x, ZipCode y)
        {
            return x._value != y._value;
        }

        public override bool Equals(object obj)
        {
            if (obj is ZipCode)
                return _value == ((ZipCode)obj)._value;
            if (obj is int)
                return _value == (int)obj;
            if (obj is string)
                return Equals(new ZipCode((string)obj));
            return false;
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        public override string ToString()
        {
            return this;
        }

        public string ToString(bool formatted)
        {
            return formatted
                ? ToString()
                : _value.ToString();
        }

    }
}
