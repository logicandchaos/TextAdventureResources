namespace TextAdventureLibrary
{
    public class Vector3Float
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public Vector3Float(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public static Vector3Float Add(Vector3Float p1, Vector3Float p2)
        {
            return new Vector3Float(p1.X + p2.X, p1.Y + p2.Y, p1.Z + p2.Z);
        }

        public static Vector3Float Multiply(Vector3Float p, int n)
        {
            return new Vector3Float(p.X * n, p.Y * n, p.Z * n);
        }

        public static Vector3Float Divide(Vector3Float p, int n)
        {
            return new Vector3Float(p.X / n, p.Y / n, p.Z / n);
        }

        public static Vector3Float operator +(Vector3Float p1, Vector3Float p2)
        {
            return Add(p1, p2);
        }

        public static Vector3Float operator *(Vector3Float p, int n)
        {
            return Multiply(p, n);
        }

        public static Vector3Float operator /(Vector3Float p, int n)
        {
            return Divide(p, n);
        }

        public override string ToString()
        {
            return $"{X}/{Y}/{Z}";
        }

        public float Cubed()
        {
            return X * Y * Z;
        }

    }
}
