using Spectre.Console;
using System;
using System.Globalization;
using System.Numerics;
using TrinityCore.Map.Net.IO.MmapTile;
using TrinityCore.Map.Net.IO.Tools;

namespace TrinityCore.Map.Net.IO.Sample
{
    class Program
    {
        static void Main()
        {
            string mmapsDirectory = @"C:\Users\silve\Documents\Trinity\Maps\wowData\3.3.5\mmaps";

            // Creating the MMAPS File collection
            MmapFilesCollection collection = MmapFilesCollection.Load(mmapsDirectory);

            DisplayClosestTile(collection);
            Console.WriteLine();
            /*
            DisplayMMAPSFiles(collection);
            Console.WriteLine();
            DisplayMMAPTilesGeneralInformation(collection);
            Console.WriteLine();
            DisplayMMAPTilesHeaderInformation(collection);
            Console.WriteLine();
            DisplayMMAPTilesMeshInformation(collection);
            Console.WriteLine();
          */
            Console.WriteLine();

            AnsiConsole.MarkupLine("[bold springgreen3_1] Process completed[/]");
            Console.ReadKey();
        }

        private static void DisplayClosestTile(MmapFilesCollection collection)
        {
            AnsiConsole.MarkupLine("[bold springgreen3_1] Closest Tile[/]");

            Vector3 start = new Vector3(-4059.2102f, -13821.921f, 70.808365f);
            Vector3 end = new Vector3(-4036.181f, -13778.89f, 74.75214f);

            collection.PathFinding.FindPath(530, start, end, 0.01f);




        }

        private static void DisplayMMAPSFiles(MmapFilesCollection collection)
        {
            AnsiConsole.MarkupLine("[bold springgreen3_1] MMAPS FILES[/]");
            var table = new Table();
            table.Width(120);
            table.AddColumns(new string[] { "File", "MapId", "MaxPoly", "MaxTiles", "Origin X", "Origin Y", "Origin Z", "TileHeight", "TileWidth" });

            for (int c = 1; c <= 8; c++)
            {
                table.Columns[c].RightAligned();
            }

            foreach (MmapFile mmap in collection.MmapFiles)
            {
                table.AddRow(new string[] { System.IO.Path.GetFileName(mmap.File), mmap.MapId.ToString(), mmap.MaxPoly.ToString(), mmap.MaxTiles.ToString(), mmap.Origin.X.ToString("F", CultureInfo.InvariantCulture), mmap.Origin.Y.ToString("F", CultureInfo.InvariantCulture), mmap.Origin.Z.ToString("F", CultureInfo.InvariantCulture), mmap.TileHeight.ToString(), mmap.TileWidth.ToString() });
            }

            AnsiConsole.Render(table);
        }

        private static void DisplayMMAPTilesGeneralInformation(MmapFilesCollection collection)
        {
            foreach (MmapFile mmap in collection.MmapFiles)
            {
                AnsiConsole.MarkupLine("[bold springgreen3_1] MMAP FILE : " + System.IO.Path.GetFileName(mmap.File) + "[/]");
                var table = new Table();
                table.Width(120);
                table.AddColumns(new string[] { "File", "MapId", "Tile X", "Tile Y" });
                for (int c = 1; c <= 3; c++)
                {
                    table.Columns[c].RightAligned();
                }

                foreach (MmapTileFile mmapTile in mmap.GetMmapTiles())
                {
                    table.AddRow(new string[] { System.IO.Path.GetFileName(mmapTile.File), mmapTile.MapId.ToString(), mmapTile.TileX.ToString(), mmapTile.TileY.ToString() });
                }

                AnsiConsole.Render(table);
            }
        }

        private static void DisplayMMAPTilesHeaderInformation(MmapFilesCollection collection)
        {
            foreach (MmapFile mmap in collection.MmapFiles)
            {
                AnsiConsole.MarkupLine("[bold springgreen3_1] MMAP FILE : " + System.IO.Path.GetFileName(mmap.File) + "[/]");
                var table = new Table();
                table.Width(120);
                table.AddColumns(new string[] { "File", "MapId", "MMap Version", "Mesh Version", "Tile Magic", "Use Liquid" });
                for (int c = 1; c <= 5; c++)
                {
                    table.Columns[c].RightAligned();
                }

                foreach (MmapTileFile mmapTile in mmap.GetMmapTiles())
                {
                    table.AddRow(new string[] { System.IO.Path.GetFileName(mmapTile.File), mmapTile.MapId.ToString(), mmapTile.Header.MMapVersion.ToString(), mmapTile.Header.NavMeshVersion.ToString(), mmapTile.Header.TileMagic.ToString(), mmapTile.Header.UseLiquids.ToString() });
                }

                AnsiConsole.Render(table);
            }
        }

        private static void DisplayMMAPTilesMeshInformation(MmapFilesCollection collection)
        {
            foreach (MmapFile mmap in collection.MmapFiles)
            {
                AnsiConsole.MarkupLine("[bold springgreen3_1] MMAP FILE : " + System.IO.Path.GetFileName(mmap.File) + "[/]");
                var table = new Table();
                table.Width(120);
                table.AddColumns(new string[] { "File", "Tile Magic", "Tile Version", "X", "Y", "Layer" });
                for (int c = 1; c <= 5; c++)
                {
                    table.Columns[c].RightAligned();
                }

                foreach (MmapTileFile mmapTile in mmap.GetMmapTiles())
                {
                    table.AddRow(new string[] { System.IO.Path.GetFileName(mmapTile.File), mmapTile.Mesh.Header.TileMagic.ToString(), mmapTile.Mesh.Header.TileVersion.ToString(), mmapTile.Mesh.Header.X.ToString(), mmapTile.Mesh.Header.Y.ToString(), mmapTile.Mesh.Header.Layer.ToString() });
                }

                AnsiConsole.Render(table);
            }
        }
    }
}
