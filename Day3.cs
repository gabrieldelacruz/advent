using System;

namespace Advent
{
    public class Day3
    {
        public class A
        {
            public static void Run()
            {
                string[] lines = System.IO.File.ReadAllLines(@"input3.txt");

                int result = 0;
                int position = 0;
                foreach (string line in lines)
                {
                    if (line[position % line.Length] == '#') result++;
                    position += 3;
                }
                Console.WriteLine("Day 3 A: " + result);
            }
        }

        public class B
        {
            public static void Run()
            {
                string[] lines = System.IO.File.ReadAllLines(@"input3.txt");
                Tuple<int, int>[] slopes = {
                new Tuple<int, int>(1, 1),
                new Tuple<int, int>(3, 1),
                new Tuple<int, int>(5, 1),
                new Tuple<int, int>(7, 1),
                new Tuple<int, int>(1, 2),
            };

                long result = 1;
                foreach (Tuple<int, int> slope in slopes)
                {
                    int count = 0;
                    for (int posY = 0, posX = 0; posY < lines.Length; posX += slope.Item1, posY += slope.Item2)
                    {
                        string line = lines[posY];
                        if (line[posX % line.Length] == '#') count++;
                    }
                    result *= count;
                }
                Console.WriteLine("Day 3 B: " + result);
            }
        }
    }
}
