using System.Numerics;
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
        public MmapMeshTriangle[] Triangles { get; set; }

        #endregion Public Properties

        #region Public Fields

        public const uint DT_NULL_LINK = 0xffffffff;

        #endregion Public Fields

        #region Public Methods

        public static MmapMeshPoly LoadBinaryReader(MmapMesh mesh, BinaryReader reader)
        {
            MmapMeshPoly poly = new MmapMeshPoly();
            poly.FirstLink = reader.ReadUint();
            poly.Verts = reader.ReadUShorts(6);
            poly.Neis = reader.ReadUShorts(6);
            poly.Flags = reader.ReadUShort();
            poly.VertCount = reader.ReadByte();
            poly.AreaAndtype = reader.ReadByte();


            switch (poly.VertCount)
            {
                case 3:
                    poly.Triangles = new MmapMeshTriangle[1];
                    poly.Triangles[0] = new MmapMeshTriangle()
                    {
                        Vertices = new ushort[] { poly.Verts[0], poly.Verts[1], poly.Verts[2] },
                        Points = new Vector3[] { mesh.Verts[poly.Verts[0]].Vector3, mesh.Verts[poly.Verts[1]].Vector3, mesh.Verts[poly.Verts[2]].Vector3 }
                    };
                    break;
                case 4:
                    poly.Triangles = new MmapMeshTriangle[2];
                    poly.Triangles[0] = new MmapMeshTriangle()
                    {
                        Vertices = new ushort[] { poly.Verts[0], poly.Verts[1], poly.Verts[2] },
                        Points = new Vector3[] { mesh.Verts[poly.Verts[0]].Vector3, mesh.Verts[poly.Verts[1]].Vector3, mesh.Verts[poly.Verts[2]].Vector3 }
                    };
                    poly.Triangles[1] = new MmapMeshTriangle()
                    {
                        Vertices = new ushort[] { poly.Verts[0], poly.Verts[2], poly.Verts[3] },
                        Points = new Vector3[] { mesh.Verts[poly.Verts[0]].Vector3, mesh.Verts[poly.Verts[2]].Vector3, mesh.Verts[poly.Verts[3]].Vector3 }
                    };
                    break;
                case 5:
                    poly.Triangles = new MmapMeshTriangle[3];
                    poly.Triangles[0] = new MmapMeshTriangle()
                    {
                        Vertices = new ushort[] { poly.Verts[0], poly.Verts[1], poly.Verts[2] },
                        Points = new Vector3[] { mesh.Verts[poly.Verts[0]].Vector3, mesh.Verts[poly.Verts[1]].Vector3, mesh.Verts[poly.Verts[2]].Vector3 }
                    };
                    poly.Triangles[1] = new MmapMeshTriangle()
                    {
                        Vertices = new ushort[] { poly.Verts[0], poly.Verts[2], poly.Verts[3] },
                        Points = new Vector3[] { mesh.Verts[poly.Verts[0]].Vector3, mesh.Verts[poly.Verts[2]].Vector3, mesh.Verts[poly.Verts[3]].Vector3 }
                    };
                    poly.Triangles[2] = new MmapMeshTriangle()
                    {
                        Vertices = new ushort[] { poly.Verts[4], poly.Verts[2], poly.Verts[3] },
                        Points = new Vector3[] { mesh.Verts[poly.Verts[4]].Vector3, mesh.Verts[poly.Verts[2]].Vector3, mesh.Verts[poly.Verts[3]].Vector3 }
                    };
                    break;
                case 6:
                    poly.Triangles = new MmapMeshTriangle[4];
                    poly.Triangles[0] = new MmapMeshTriangle()
                    {
                        Vertices = new ushort[] { poly.Verts[0], poly.Verts[1], poly.Verts[2] },
                        Points = new Vector3[] { mesh.Verts[poly.Verts[0]].Vector3, mesh.Verts[poly.Verts[1]].Vector3, mesh.Verts[poly.Verts[2]].Vector3 }
                    };
                    poly.Triangles[1] = new MmapMeshTriangle()
                    {
                        Vertices = new ushort[] { poly.Verts[0], poly.Verts[2], poly.Verts[3] },
                        Points = new Vector3[] { mesh.Verts[poly.Verts[0]].Vector3, mesh.Verts[poly.Verts[2]].Vector3, mesh.Verts[poly.Verts[3]].Vector3 }
                    };
                    poly.Triangles[2] = new MmapMeshTriangle()
                    {
                        Vertices = new ushort[] { poly.Verts[4], poly.Verts[2], poly.Verts[3] },
                        Points = new Vector3[] { mesh.Verts[poly.Verts[4]].Vector3, mesh.Verts[poly.Verts[2]].Vector3, mesh.Verts[poly.Verts[3]].Vector3 }
                    };
                    poly.Triangles[3] = new MmapMeshTriangle()
                    {
                        Vertices = new ushort[] { poly.Verts[5], poly.Verts[2], poly.Verts[4] },
                        Points = new Vector3[] { mesh.Verts[poly.Verts[5]].Vector3, mesh.Verts[poly.Verts[2]].Vector3, mesh.Verts[poly.Verts[4]].Vector3 }
                    };
                    break;
            }



            return poly;
        }

        #endregion Public Methods
    }
}