using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace TrinityCore.Map.Net.IO.Tools
{
    public static class Vector3Extensions
    {
        public static Vector3 ToFileFormat(this Vector3 position)
        {
            return new Vector3(position.Y, position.Z, position.X);
        }
    }
}
