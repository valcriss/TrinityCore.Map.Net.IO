using System.Numerics;
using TrinityCore.Map.Net.IO.Tools;

namespace TrinityCore.Map.Net.IO.MmapTile
{
    public class MmapMeshVert
    {
        #region Public Properties

        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public Vector3 Vector3 { get; set; }

        #endregion Public Properties

        #region Public Methods

        public static MmapMeshVert LoadBinaryReader(BinaryReader reader)
        {
            MmapMeshVert vert = new MmapMeshVert();
            vert.X = reader.ReadFloat();
            vert.Y = reader.ReadFloat();
            vert.Z = reader.ReadFloat();
            vert.Vector3 = new Vector3(vert.X, vert.Y, vert.Z);
            return vert;
        }

        #endregion Public Methods
    }
}