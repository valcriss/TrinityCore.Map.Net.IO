using System;
using System.Collections.Generic;
using System.Text;

namespace TrinityCore.Map.Net.IO
{
    public class Point
    {
        public float X;
        public float Y;
        public float Z;

        public Point(float x, float y, float z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public float Length
        {
            get { return (float)Math.Sqrt(X * X + Y * Y + Z * Z); }
        }

        public Point Direction
        {
            get
            {
                float length = Length;
                return new Point(X / length, Y / length, Z / length);
            }
        }

        public float DirectionOrientation
        {
            get { var dir = Direction; double orientation = Math.Atan2(dir.Y, dir.X); if (orientation < 0) orientation += 2.0 * Math.PI; return (float)orientation; }
        }

        public static Point operator +(Point a, Point b)
        {
            return new Point(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }

        public static Point operator -(Point a, Point b)
        {
            return new Point(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }

        public static Point operator *(Point point, float scale)
        {
            return new Point(point.X * scale, point.Y * scale, point.Z * scale);
        }

        public static bool operator ==(Point a, Point b)
        {
            return a.X == b.X && a.Y == b.Y && a.Z == b.Z;
        }

        public static bool operator !=(Point a, Point b)
        {
            return a.X != b.X || a.Y != b.Y || a.Z != b.Z;
        }

        public override string ToString()
        {
            return "X: " + X + " | Y: " + Y + " | Z: " + Z;
        }

        public override bool Equals(object obj)
        {
            return (Point)obj == this;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
