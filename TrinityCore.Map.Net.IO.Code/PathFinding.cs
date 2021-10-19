using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using TrinityCore.Map.Net.IO.MmapTile;
using TrinityCore.Map.Net.IO.Tools;

namespace TrinityCore.Map.Net.IO
{
    public class PathFinding
    {
        private MmapFilesCollection Collection { get; set; }
        public PathFinding(MmapFilesCollection collection)
        {
            Collection = collection;
        }

        public Path FindPath(int mapId, Vector3 start, Vector3 end, float speed, int maxPathLength = 50)
        {
            MmapFile mmap = Collection.GetMap(mapId);

            MmapTileFile startTile = mmap.GetMmapTileFileFromVector3(start);
            if (startTile == null) return null;
            MmapTileFile endTile = mmap.GetMmapTileFileFromVector3(end);
            if (endTile == null) return null;

            if (startTile.Key != endTile.Key) return null;

            MmapMeshPoly startPoly = startTile.GetNearestPoly(start);
            if (startPoly == null) return null;
            MmapMeshPoly endPoly = endTile.GetNearestPoly(end);
            if (endPoly == null) return null;

            List<string> done = new List<string>();
            List<MmapMeshPoly> mmapMeshPolies = new List<MmapMeshPoly>() { startPoly };
            Queue<List<MmapMeshPoly>> queue = new Queue<List<MmapMeshPoly>>();
            queue.Enqueue(mmapMeshPolies);

            DateTime s = DateTime.Now;

            while (queue.Count > 0)
            {
                List<MmapMeshPoly> current = queue.Dequeue();
                MmapMeshPoly last = current[current.Count - 1];

                foreach (MmapMeshPoly poly in last.GetNeighbors(startTile))
                {
                    if (done.Contains(poly.Key)) continue;

                    if (poly.Key == endPoly.Key)
                    {
                        current.Add(poly);
                        System.Diagnostics.Debug.WriteLine("Duration :" + (DateTime.Now.Subtract(s).TotalMilliseconds));
                        return new Path(current.ToPoints(), speed, mapId);
                    }

                    done.Add(poly.Key);

                    if (current.Count >= maxPathLength) continue;
                    List<MmapMeshPoly> clone = current.ToArray().ToList();
                    clone.Add(poly);
                    queue.Enqueue(clone);
                }
            }

            return null;
        }
    }


    public class Path
    {
        public Point[] Points { get; set; }
        private int NextPointIndex { get; set; }

        public Point CurrentPosition
        {
            get => _currentPosition;
            private set => _currentPosition = value;
        }
        Point _currentPosition;

        public float CurrentOrientation
        {
            get
            {
                if (NextPointIndex < Points.Length)
                    return (Points[NextPointIndex] - CurrentPosition).DirectionOrientation;
                else
                    return (Points[NextPointIndex - 1] - Points[NextPointIndex - 2]).DirectionOrientation;
            }
        }

        public static float GetOrientation(float x1, float y1, float z1, float x2, float y2, float z2)
        {
            return (new Point(x2, y2, z2) - new Point(x1, y1, z1)).DirectionOrientation;
        }

        public float Speed
        {
            get;
            set;
        }

        public int MapId
        {
            get;
            private set;
        }

        public Point Destination
        {
            get
            {
                return Points.Last();
            }
        }

        Point _previousPosition;
        int _closePositionCounter;
        static readonly int MaxClosePositionCounter = 4;

        public Path(List<Point> points, float speed, int mapId)
        {
            if (points == null || points.Count < 2)
                throw new ArgumentException("Argument cannot be null or a list with just 1 point", "points");
            this.Points = points.ToArray();

            for (var index = 0; index < Points.Length; index++)
            {
                Points[index] = new Point(Points[index].X, Points[index].Y, Points[index].Z);
            }

            if (speed <= 0.0f)
                throw new ArgumentException("Argument must be a positive number", "speed");
            this.Speed = speed;

            this.CurrentPosition = this.Points[0];
            this.NextPointIndex = 1;
            this.MapId = mapId;
            this._previousPosition = CurrentPosition;
            this._closePositionCounter = 0;
        }

        public Point MoveAlongPath(float deltaTime)
        {
            float totalDistance = deltaTime * Speed;

            if (Points.Length <= NextPointIndex) return Points[Points.Length - 1];

            float distanceToNextPoint = (Points[NextPointIndex] - _currentPosition).Length;
            if (totalDistance < distanceToNextPoint)
            {
                Point result = _currentPosition + (Points[NextPointIndex] - _currentPosition).Direction * totalDistance;
                _currentPosition = result;
            }
            else
            {
                NextPoint(totalDistance, distanceToNextPoint);
            }

            if ((_currentPosition - _previousPosition).Length < 1f)
            {
                _closePositionCounter++;
                if (_closePositionCounter >= MaxClosePositionCounter)
                {
                    _closePositionCounter = 0;
                    NextPoint(totalDistance, distanceToNextPoint);
                }
            }
            else
            {
                _previousPosition = _currentPosition;
                _closePositionCounter = 0;
            }

            return _currentPosition;
        }

        private void NextPoint(float totalDistance, float distanceToNextPoint)
        {
            NextPointIndex++;
            if (NextPointIndex >= Points.Length - 1)
                _currentPosition = Points.Last();
            else
            {
                float remainingTime = (totalDistance - distanceToNextPoint) / Speed;
                _currentPosition = MoveAlongPath(remainingTime);
            }
        }
    }
}
