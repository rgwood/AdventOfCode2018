using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace _3_1
{
    public class Inch
    {
        public List<int> Claims = new List<int>();
        public void StakeClaim(int id)
        {
            Claims.Add(id);
        }

        public int ClaimCount => Claims.Count;
    }

    class Program
    {
        const int FabricSizeInInches = 2000;
        static void Main(string[] args)
        {

            var lines = new List<string>();
            using (StreamReader sr = new StreamReader("input.txt"))
            {
                while (sr.Peek() != -1)
                {
                    String line = sr.ReadLine();
                    lines.Add(line);
                }
            }

            var fabric = new Inch[FabricSizeInInches, FabricSizeInInches];
            initializeFabric(fabric);

            var uncontestedClaims = new List<int>();

            foreach (var line in lines)
            {
                var id = int.Parse(line.Substring(1, line.IndexOf(' ') -1));
                uncontestedClaims.Add(id);
                var lineWithoutIdOrSpaces = line.Substring(line.IndexOf('@') + 2).Replace(" ", "");
                string[] separatingChars = { ",", ":", "x" };
                var splitLine = lineWithoutIdOrSpaces.Split(separatingChars, StringSplitOptions.RemoveEmptyEntries);

                var x = int.Parse(splitLine[0]);
                var y = int.Parse(splitLine[1]);
                var x_len = int.Parse(splitLine[2]);
                var y_len = int.Parse(splitLine[3]);


                var contested = incrementClaimAndReturnContestedClaims(fabric, id, x, y, x_len, y_len);

                uncontestedClaims = uncontestedClaims.Except(contested).ToList();
            }


            var contestedInchCount = 0;
            for (int i = 0; i < FabricSizeInInches; i++)
            {
                for (int j = 0; j < FabricSizeInInches; j++)
                {
                    if (fabric[i, j].ClaimCount > 1)
                    {
                        contestedInchCount++;
                    }

                }
            }

            Console.WriteLine($"Contested square inches: {contestedInchCount}");
            foreach (var id in uncontestedClaims)
            {
                Console.WriteLine($"Uncontested: id {id}");
            }
        }

        static List<int> incrementClaimAndReturnContestedClaims(Inch[,] fabric, int id, int x, int y, int x_len, int y_len)
        {
            var contested = new List<int>();
            for (int i = x; i < x + x_len; i++)
            {
                for (int j = y; j < y + y_len; j++)
                {
                    var inch = fabric[i, j];
                    if(inch.ClaimCount > 0)
                    {
                        contested.AddRange(inch.Claims);
                        contested.Add(id);
                    }
                    inch.StakeClaim(id);
                }
            }

            return contested.Distinct().ToList();
        }

        static void initializeFabric(Inch[,] fabric)
        {
            for (int i = 0; i < FabricSizeInInches; i++)
            {
                for (int j = 0; j < FabricSizeInInches; j++)
                {
                    fabric[i, j] = new Inch();
                }
            }
        }
    }
}
