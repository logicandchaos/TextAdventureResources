using System;

namespace TextAdventureLibrary
{
    public class Vector2Double
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Vector2Double() { }
        
        public Vector2Double(double x, double y)
        {
            X = x;
            Y = y;
        }

        public static Vector2Double Add(Vector2Double p1, Vector2Double p2)
        {
            return new Vector2Double(p1.X + p2.X, p1.Y + p2.Y);
        }

        public static Vector2Double Sub(Vector2Double p1, Vector2Double p2)
        {
            return new Vector2Double(p1.X - p2.X, p1.Y - p2.Y);
        }

        public static Vector2Double Multiply(Vector2Double p, int n)
        {
            return new Vector2Double(p.X * n, p.Y * n);
        }

        public static Vector2Double Divide(Vector2Double p, int n)
        {
            return new Vector2Double(p.X / n, p.Y / n);
        }

        public static Vector2Double operator +(Vector2Double p1, Vector2Double p2)
        {
            return Add(p1, p2);
        }

        public static Vector2Double operator -(Vector2Double p1, Vector2Double p2)
        {
            return Sub(p1, p2);
        }

        public static Vector2Double operator *(Vector2Double p, int n)
        {
            return Multiply(p, n);
        }

        public static Vector2Double operator /(Vector2Double p, int n)
        {
            return Divide(p, n);
        }

        public override string ToString()
        {
            return $"{X}/{Y}";
        }

        public bool IsEqual(Vector2Double other)
        {
            return Math.Abs(X - other.X) < 0.0001 && Math.Abs(Y - other.Y) < 0.0001;
        }

        public double DistanceTo(Vector2Double other)
        {
            double dx = X - other.X;
            double dy = Y - other.Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }

        public static double Distance(Vector2Double point1, Vector2Double point2)
        {
            double dx = point2.X - point1.X;
            double dy = point2.Y - point1.Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }

        /*public void MoveTowards(Vector2Double p_destination, int p_amount)
        {
            double distanceToDestination = DistanceTo(p_destination);
            if (distanceToDestination <= p_amount)
            {
                // If the distance to the destination is less than or equal to the speed,
                // simply move the point to the destination
                X = p_destination.X;
                Y = p_destination.Y;
            }
            else
            {
                // Calculate the new X and Y coordinates based on the current position,
                // the destination, and the speed
                double dx = p_destination.X - X;
                double dy = p_destination.Y - Y;
                double angle = Math.Atan2(dy, dx);
                double newX = X + (p_amount * Math.Cos(angle));
                double newY = Y + (p_amount * Math.Sin(angle));

                // Update the X and Y properties to move the point towards the destination
                X = newX;
                Y = newY;
            }
        }*/
        public void MoveTowards(Vector2Double destination, double amount)
        {
            double dx = destination.X - X;
            double dy = destination.Y - Y;
            double distance = Math.Sqrt(dx * dx + dy * dy);

            if (distance <= amount || distance == 0)
            {
                X = destination.X;
                Y = destination.Y;
                return;
            }

            X += dx / distance * amount;
            Y += dy / distance * amount;
        }

        public static Vector2Double GetRandomPointAtDistanceAndAngle(Vector2Double center, double distance, double angleInDegrees)
        {
            // Convert angle from degrees to radians
            double angleInRadians = angleInDegrees * Math.PI / 180.0;

            // Calculate the x and y coordinates of the point using trigonometry
            double x = Math.Round(center.X + distance * Math.Cos(angleInRadians));
            double y = Math.Round(center.Y + distance * Math.Sin(angleInRadians));

            // Create and return a new Point object with the calculated coordinates
            return new Vector2Double(x, y);
        }

        public static Vector2Double GetPointAtAngleAndDistance(Vector2Double startingPoint, double angleDegrees, double distance)
        {
            // Convert angle from degrees to radians
            double angleRadians = angleDegrees * Math.PI / 180.0;

            // Calculate the x and y coordinates of the new point using trigonometry
            double x = startingPoint.X + distance * Math.Cos(angleRadians);
            double y = startingPoint.Y + distance * Math.Sin(angleRadians);

            // Return the new point
            return new Vector2Double(Math.Round(x), Math.Round(y));
        }

    }
}
