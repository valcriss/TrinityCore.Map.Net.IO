using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using TrinityCore.Map.Net.IO.MmapTile;

namespace TrinityCore.Map.Net.IO.Tools
{
    public static class MmapMeshPolyExtensions
    {
        public static List<Point> ToPoints(this List<MmapMeshPoly> polies)
        {
            List<Point> points = new List<Point>();
            foreach (MmapMeshPoly poly in polies)
            {
                Vector3 center = poly.Center();
                points.Add(new Point(center.Z, center.X, center.Y));
            }
            return points;
        }
    }
}
