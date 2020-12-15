using System;
using System.Collections.Generic;

namespace Advent
{
    public class Day15
    {
        public class A
        {
            public static void Run()
            {
                string[] lines = System.IO.File.ReadAllLines(@"input15.txt");

                Dictionary<int, int> numbers = new Dictionary<int, int>();

                string line = lines[0];
                int start = line.IndexOf(',');
                int end = 0;
                int lastNumber = int.Parse(line.Substring(0, start++));
                for (int index = 1; index < 2020; ++index)
                {
                    int number = 0;
                    if (start < line.Length)
                    {
                        end = start + 1;
                        while (end < line.Length && line[end] != ',') end++;
                        number = int.Parse(line.Substring(start, end - start));
                        start = end + 1;
                    }
                    else
                    {
                        if (numbers.TryGetValue(lastNumber, out number))
                        {
                            number = index - number;
                        }
                    }
                    numbers[lastNumber] = index;
                    lastNumber = number;
                }

                Console.WriteLine("Day 15 A: " + lastNumber);
            }
        }

        public class B
        {
            public static void Run()
            {
                string[] lines = System.IO.File.ReadAllLines(@"input15.txt");

                Dictionary<int, int> numbers = new Dictionary<int, int>();

                string line = lines[0];
                int start = line.IndexOf(',');
                int end = 0;
                int lastNumber = int.Parse(line.Substring(0, start++));
                for (int index = 1; index < 30000000; ++index)
                {
                    int number = 0;
                    if (start < line.Length)
                    {
                        end = start + 1;
                        while (end < line.Length && line[end] != ',') end++;
                        number = int.Parse(line.Substring(start, end - start));
                        start = end + 1;
                    }
                    else
                    {
                        if (numbers.TryGetValue(lastNumber, out number))
                        {
                            number = index - number;
                        }
                    }
                    numbers[lastNumber] = index;
                    lastNumber = number;
                }

                Console.WriteLine("Day 15 B: " + lastNumber);
            }
        }
    }
}
