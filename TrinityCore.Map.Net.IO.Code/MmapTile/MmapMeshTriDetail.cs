﻿using TrinityCore.Map.Net.IO.Tools;

namespace TrinityCore.Map.Net.IO.MmapTile
{
    public class MmapMeshTriDetail
    {
        #region Public Properties

        public byte[] Tri { get; set; }

        #endregion Public Properties

        #region Public Methods

        public static MmapMeshTriDetail LoadBinaryReader(BinaryReader reader)
        {
            MmapMeshTriDetail triDetail = new MmapMeshTriDetail();
            triDetail.Tri = reader.ReadBytes(4);
            return triDetail;
        }

        #endregion Public Methods
    }
}