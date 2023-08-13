using System;

namespace TextAdventureLibrary
{
    public class Point2D
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point2D(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static Point2D Add(Point2D p1, Point2D p2)
        {
            return new Point2D(p1.X + p2.X, p1.Y + p2.Y);
        }

        public static Point2D Multiply(Point2D p, int n)
        {
            return new Point2D(p.X * n, p.Y * n);
        }

        public static Point2D Divide(Point2D p, int n)
        {
            return new Point2D(p.X / n, p.Y / n);
        }

        public static Point2D operator +(Point2D p1, Point2D p2)
        {
            return Add(p1, p2);
        }

        public static Point2D operator *(Point2D p, int n)
        {
            return Multiply(p, n);
        }

        public static Point2D operator /(Point2D p, int n)
        {
            return Divide(p, n);
        }

        public override string ToString()
        {
            return $"{X}/{Y}";
        }

        public double DistanceTo(Point2D other)
        {
            int dx = X - other.X;
            int dy = Y - other.Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }

        public void MoveTowards(Point2D p_destination, int p_amount)
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
                int newX = X + (int)(p_amount * Math.Cos(angle));
                int newY = Y + (int)(p_amount * Math.Sin(angle));

                // Update the X and Y properties to move the point towards the destination
                X = newX;
                Y = newY;
            }
        }

        public static Point2D GetRandomPointAtDistanceAndAngle(Point2D center, double distance, double angleInDegrees)
        {
            // Convert angle from degrees to radians
            double angleInRadians = angleInDegrees * Math.PI / 180.0;

            // Calculate the x and y coordinates of the point using trigonometry
            int x = (int)Math.Round(center.X + distance * Math.Cos(angleInRadians));
            int y = (int)Math.Round(center.Y + distance * Math.Sin(angleInRadians));

            // Create and return a new Point object with the calculated coordinates
            return new Point2D(x, y);
        }

        public static Point2D GetPointAtAngleAndDistance(Point2D startingPoint, double angleDegrees, double distance)
        {
            // Convert angle from degrees to radians
            double angleRadians = angleDegrees * Math.PI / 180.0;

            // Calculate the x and y coordinates of the new point using trigonometry
            double x = startingPoint.X + distance * Math.Cos(angleRadians);
            double y = startingPoint.Y + distance * Math.Sin(angleRadians);

            // Return the new point
            return new Point2D((int)Math.Round(x), (int)Math.Round(y));
        }

    }
}
