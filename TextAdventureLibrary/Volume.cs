namespace TextAdventureLibrary
{
    public class Volume
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public Volume(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public static Volume Add(Volume p1, Volume p2)
        {
            return new Volume(p1.X + p2.X, p1.Y + p2.Y, p1.Z + p2.Z);
        }

        public static Volume Multiply(Volume p, int n)
        {
            return new Volume(p.X * n, p.Y * n, p.Z * n);
        }

        public static Volume Divide(Volume p, int n)
        {
            return new Volume(p.X / n, p.Y / n, p.Z / n);
        }

        public static Volume operator +(Volume p1, Volume p2)
        {
            return Add(p1, p2);
        }

        public static Volume operator *(Volume p, int n)
        {
            return Multiply(p, n);
        }

        public static Volume operator /(Volume p, int n)
        {
            return Divide(p, n);
        }

        public override string ToString()
        {
            return $"{X}/{Y}/{Z}";
        }

        public float GetVolumeCubed()
        {
            return X * Y * Z;
        }

    }
}
