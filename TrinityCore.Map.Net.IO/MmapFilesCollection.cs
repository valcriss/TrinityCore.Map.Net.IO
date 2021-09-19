using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TrinityCore.Map.Net.IO.Exceptions;

namespace TrinityCore.Map.Net.IO
{
    public class MmapFilesCollection
    {
        public List<MmapFile> MmapFiles { get; set; }

        public MmapFilesCollection()
        {
            MmapFiles = new List<MmapFile>();
        }

        public MmapFile GetMap(int mapId)
        {
            return MmapFiles.FirstOrDefault(c => c.MapId == mapId);
        }

        public static MmapFilesCollection Load(string mmapsDirectory)
        {
            MmapFilesCollection collection = new MmapFilesCollection();

            if(!System.IO.Directory.Exists(mmapsDirectory) || !System.IO.File.Exists(System.IO.Path.Combine(mmapsDirectory,"000.mmap")))
            {
                throw new DirectoryInvalidException();
            }

            foreach(string file in System.IO.Directory.GetFiles(mmapsDirectory,"*."+Constants.MMAP_FILE_EXTENSION))
            {
                collection.MmapFiles.Add(MmapFile.Load(file));
            }

            return collection;
        }
    }
}
