using System;
using System.Collections.Generic;
using System.Text;

namespace TrinityCore.Map.Net.IO
{
    public class MmapFilesCollection
    {
        public List<MmapFile> MmapFiles { get; set; }

        public MmapFilesCollection()
        {
            MmapFiles = new List<MmapFile>();
        }

        public static MmapFilesCollection Load(string mmapsDirectory)
        {
            MmapFilesCollection collection = new MmapFilesCollection();

            foreach(string file in System.IO.Directory.GetFiles(mmapsDirectory,"*."+Constants.MMAP_FILE_EXTENSION))
            {
                collection.MmapFiles.Add(MmapFile.Load(file));
            }

            return collection;
        }
    }
}
