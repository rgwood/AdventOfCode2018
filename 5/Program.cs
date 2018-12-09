using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace _5
{
    class Program
    {
        static void Main(string[] args)
        {
            var originalInput = new List<char>();
            using (StreamReader sr = new StreamReader("input.txt"))
            {
                String file = sr.ReadToEnd();
                originalInput.AddRange(file.AsEnumerable());

                for (int i = 0; i < originalInput.Count; i++)
                {
                    if(!char.IsLetter(originalInput[i]))
                    {
                        Console.WriteLine($"{i}, {originalInput[i]}");
                    }
                }

                originalInput.RemoveAt(originalInput.Count - 1); //discard EOF
            }

            //originalInput = "dabAcCaCBAcCcaDA".ToList();

            var alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            (char letter, int count) best = (letter: ' ', count: int.MaxValue);
            foreach (var c in alphabet)
            {
                Console.WriteLine($"Testing without '{c}'");
                char[] charsToRemove = {char.ToUpper(c), char.ToLower(c)};
                var modifiedInput = originalInput.Where(ch => !charsToRemove.Contains(ch)).ToList();

                // Console.WriteLine($"Initial count: {modifiedInput.Count}");

                var noMoreReactions = false;
                while (!noMoreReactions)
                {
                    noMoreReactions = !React(modifiedInput);
                    //Console.WriteLine(new string(input.ToArray()));
                }
                var finalCount = modifiedInput.Count;

                if(best.count > finalCount)
                {
                    best = (letter: c, count: finalCount);
                }

                // Console.WriteLine($"Final count: {modifiedInput.Count}");
            }

            Console.WriteLine($"Winner: {best.letter}, {best.count}");
        }

        static bool React(List<char> input)
        {
            char prevChar = input[0];
            for (int i = 1; i < input.Count; i++)
            {
                if (SameCharWithDifferentCase(prevChar, input[i]))
                {
                    input.RemoveAt(i);
                    input.RemoveAt(i - 1);
                    return true;
                }

                prevChar = input[i];
            }

            return false;
        }

        static bool SameCharWithDifferentCase(char c1, char c2)
        {
            var sum = (char.IsUpper(c1) ? 1 : -1) + (char.IsUpper(c2) ? 1 : -1);
            var differentCase = (sum == 0);
            var sameChar = char.ToUpper(c1) == char.ToUpper(c2);
            return differentCase && sameChar;
        }
    }
}
