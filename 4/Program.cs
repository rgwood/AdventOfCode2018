using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace _4
{
    public class Record
    {
        public Record(int id)
        {
            ID = id;
            Asleep = new bool[60];
        }
        public int ID { get; }

        public bool[] Asleep { get; set; }

        public int MinAsleep => Asleep.Where(m => m == true).Count();

        public void MarkAsleepFrom(int min)
        {
            for (int i = min; i < 60; i++)
            {
                Asleep[i] = true;
            }
        }

        public void MarkAwakeFrom(int min)
        {
            for (int i = min; i < 60; i++)
            {
                Asleep[i] = false;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var guardPattern = @"Guard #(\d+)";
            var lines = new List<string>();
            using (StreamReader sr = new StreamReader("input.txt"))
            {
                while (sr.Peek() != -1)
                {
                    String line = sr.ReadLine();
                    lines.Add(line);
                }
            }

            var sortedLines = lines.OrderBy(l => l).ToList();

            Record currentRecord = null;
            var allRecords = new List<Record>();

            foreach (var line in sortedLines)
            {
                Console.WriteLine(line);
                var match = Regex.Match(line, guardPattern);
                if (match.Success)
                {

                    currentRecord = new Record(int.Parse(match.Groups[1].Value));
                    allRecords.Add(currentRecord);
                    continue;
                }

                var minGroup = Regex.Match(line, @"00:(\d{2})").Groups[1].Value;
                var min = int.Parse(minGroup);

                if (line.Contains("wakes up"))
                {
                    //Console.WriteLine($"wake {min}");
                    currentRecord.MarkAwakeFrom(min);
                }
                else if (line.Contains("falls asleep"))
                {
                    //Console.WriteLine($"asleep {min}");
                    currentRecord.MarkAsleepFrom(min);
                }
            }

            var groupedById = allRecords.GroupBy(r => r.ID);

            // 1
            // var mostSleep = groupedById.OrderByDescending(g => g.Sum(r => r.MinAsleep)).First();
            // var ret = GetMinMostAsleep(mostSleep);

            // var minAsleepMost = ret.min;
            
            // Console.WriteLine($"Minute spent most asleep: {minAsleepMost}");
            // Console.WriteLine($"Most sleep: id {mostSleep.Key}, {mostSleep.Sum(r => r.MinAsleep)} min");
            // Console.WriteLine($"Answer: {minAsleepMost * mostSleep.Key}");

            // 2
            var winner = groupedById.Select(g => GetMinMostAsleep(g)).OrderByDescending(r => r.timesSpentAsleep).First();

            Console.WriteLine($"Id: {winner.id}, Min: {winner.min}, Result: {winner.id * winner.min}");

        }

        static (int id, int min, int timesSpentAsleep) GetMinMostAsleep(IEnumerable<Record> records)
        {
            var minutesAsleep = new Dictionary<int, int>();
            for (int i = 0; i < 60; i++)
            {
                minutesAsleep.Add(i, 0);
            }

            foreach (var record in records)
            {
                for (int i = 0; i < 60; i++)
                {
                    if (record.Asleep[i])
                        minutesAsleep[i]++;
                }
            }

            var winner = minutesAsleep.OrderByDescending(m => m.Value).First();
            return (id: records.First().ID, min: winner.Key, timesSpentAsleep: winner.Value);  
        }
    }
}
