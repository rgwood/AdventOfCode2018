using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace _6
{
    class Coordinate
    {
        public Coordinate(int id, int x, int y)
        {
            ID = id;
            X = x;
            Y = y;
        }

        public int ID {get;}
        public int X {get;}
        public int Y {get;}
    }

    class Program
    {
        static void Main(string[] args)
        {
            var originalInput = new List<char>();

            var coords = new List<Coordinate>();

            using (StreamReader sr = new StreamReader("input.txt"))
            {
                while (sr.Peek() != -1)
                {
                    String line = sr.ReadLine();
                    var vals = line.Split(", ");
                    var nextId = coords.Count + 1;
                    coords.Add(new Coordinate(nextId, int.Parse(vals[0]), int.Parse(vals[1])));
                }
            }

            var minX = coords.Min(c => c.X);
            var maxX = coords.Max(c => c.X);
            var minY = coords.Min(c => c.Y);
            var maxY = coords.Max(c => c.Y);

            bool IsInfinite(Coordinate c)
            {
                return (c.X == minX || c.X == maxX) || (c.Y == minY || c.Y == maxY);
            }

            //this would hella break if we ever encountered negative numbers...
            var grid = new Coordinate[maxX,maxY];

            for (int x = 0; x < maxX; x++)
            {
                for (int y = 0; y < maxY; y++)
                {
                    var withDistance = coords.Select(c => new {Distance =  ManhattanDistance(x, y, c), Coordinate = c});
                    var groupedByDistance = withDistance.GroupBy(c => c.Distance);
                    var closest = groupedByDistance.OrderBy(g => g.Key).First();

                    if(closest.Count() == 1)
                    {
                        grid[x,y] = closest.Single().Coordinate;
                    }
                }
            }

            var enumerableGrid = grid.Cast<Coordinate>();
            var nonNull = enumerableGrid.Where(c => c != null);
            var nonInfinite = nonNull.Where(c => !IsInfinite(c));
            var biggestAreaCount = nonInfinite.GroupBy(c => c.ID).Max(c => c.Count());

            Console.WriteLine($"Biggest count: {biggestAreaCount}");
        }

        private static int ManhattanDistance(int x, int y, Coordinate c)
        {
            return Math.Abs(x - c.X) + Math.Abs(y - c.Y);
        }
    }
}
