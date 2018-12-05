using System;
using System.IO;
using System.Collections.Generic;

namespace _2
{
    class Program
    {

        //1
        // static void Main(string[] args)
        // {
        //     int twoCount = 0, threeCount = 0;
        //     using (StreamReader sr = new StreamReader("input.txt"))
        //     {
        //         while (sr.Peek() != -1)
        //         {
        //             String line = sr.ReadLine();

        //             var letterCount = new Dictionary<char, int>();

        //             foreach (char c in line)
        //             {
        //                 // Console.WriteLine($"Char: {c}");

        //                 if(letterCount.ContainsKey(c))
        //                 {
        //                     letterCount[c]++;
        //                 }
        //                 else
        //                 {
        //                     letterCount.Add(c, 1);
        //                 }
        //             }

        //             Console.WriteLine(line);

        //             if(letterCount.ContainsValue(2))
        //             {
        //                 twoCount++;
        //                 Console.WriteLine(2);
        //             }

        //             if(letterCount.ContainsValue(3))
        //             {
        //                 threeCount++;
        //                 Console.WriteLine(3);
        //             }

        //         }
        //     }

        //     Console.WriteLine($"Checksum: {twoCount * threeCount}");
        // }

        // s2
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

            foreach (var l1 in lines)
            {
                foreach (var l2 in lines)
                {
                    if (differentByOne(l1, l2))
                    {
                        Console.WriteLine(l1);
                        Console.WriteLine(l2);
                        return;
                    }
                }
            }
        }

        static bool differentByOne(string i1, string i2)
        {
            if (i1.Length != i2.Length)
            {
                throw new InvalidDataException("Different length");
            }

            int differentCount = 0;

            for (int i = 0; i < i1.Length; i++)
            {
                if (i1[i] != i2[i])
                {
                    differentCount++;
                    if (differentCount > 1)
                    {
                        return false;
                    }
                }
            }
            return (differentCount == 1) ? true : false;
        }
    }
}
