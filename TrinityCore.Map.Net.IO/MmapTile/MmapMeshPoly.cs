using TrinityCore.Map.Net.IO.Tools;

namespace TrinityCore.Map.Net.IO.MmapTile
{
    public class MmapMeshPoly
    {
        #region Public Properties

        public byte AreaAndtype { get; set; }
        public uint FirstLink { get; set; }
        public ushort Flags { get; set; }
        public ushort[] Neis { get; set; }
        public byte VertCount { get; set; }
        public ushort[] Verts { get; set; }

        #endregion Public Properties

        #region Public Fields

        public const uint DT_NULL_LINK = 0xffffffff;

        #endregion Public Fields

        #region Public Methods

        public static MmapMeshPoly LoadBinaryReader(BinaryReader reader)
        {
            MmapMeshPoly poly = new MmapMeshPoly();
            poly.FirstLink = reader.ReadUint();
            poly.Verts = reader.ReadUShorts(6);
            poly.Neis = reader.ReadUShorts(6);
            poly.Flags = reader.ReadUShort();
            poly.VertCount = reader.ReadByte();
            poly.AreaAndtype = reader.ReadByte();

            return poly;
        }

        #endregion Public Methods
    }
}