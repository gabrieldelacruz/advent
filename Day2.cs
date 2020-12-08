using System;

namespace Advent
{
    public class Day2
    {
        public class A
        {
            public static void Run()
            {
                string[] lines = System.IO.File.ReadAllLines(@"input2.txt");

                int result = 0;
                foreach (string line in lines)
                {
                    int separatorMin = line.IndexOf('-');
                    int minCount = Int32.Parse(line.Substring(0, separatorMin));
                    int separatorMax = line.IndexOf(' ');
                    int maxCount = Int32.Parse(line.Substring(separatorMin + 1, separatorMax - separatorMin - 1));
                    char letter = line[separatorMax + 1];
                    int count = 0;
                    for (int i = line.IndexOf(':') + 2; i < line.Length; ++i)
                    {
                        if (line[i] == letter) count++;
                    }
                    if (count >= minCount && count <= maxCount) result++;
                }
                Console.WriteLine("Day 2 A: " + result);
            }
        }

        public class B
        {
            public static void Run()
            {
                string[] lines = System.IO.File.ReadAllLines(@"input2.txt");

                int result = 0;
                foreach (string line in lines)
                {
                    int separatorMin = line.IndexOf('-');
                    int posA = Int32.Parse(line.Substring(0, separatorMin));
                    int separatorMax = line.IndexOf(' ');
                    int posB = Int32.Parse(line.Substring(separatorMin + 1, separatorMax - separatorMin - 1));
                    char letter = line[separatorMax + 1];
                    int start = line.IndexOf(':') + 1;
                    if ((line[start + posA] == letter) ^ (line[start + posB] == letter)) result++;
                }
                Console.WriteLine("Day 2 B: " + result);
            }
        }
    }
}
