using System;
using System.IO;
using System.Collections.Generic;

namespace _1
{
    class Program
    {
        static void Main(string[] args)
        {
            var totalFrequency = 0;
            var totalFrequenciesSeen = new HashSet<int>();
            var listOfFrequencies = new List<int>();

            using (StreamReader sr = new StreamReader("input.txt"))
            {
                while (sr.Peek() != -1)
                {
                    String line = sr.ReadLine();
                    var parsedFrequency = Int32.Parse(line);
                    listOfFrequencies.Add(parsedFrequency);
                }
            }

            while(true)
            {
                foreach (var freq in listOfFrequencies)
                {
                    totalFrequenciesSeen.Add(totalFrequency);
                    totalFrequency += freq;
                    if(totalFrequenciesSeen.Contains(totalFrequency))
                    {
                        Console.WriteLine($"First frequency seen twice: {totalFrequency}");
                        return;
                    }
                }
            }
        }
    }
}
