using System;

namespace Advent
{
    public class Day10
    {
        public class A
        {
            public static void Run()
            {
                string[] lines = System.IO.File.ReadAllLines(@"input10.txt");
                bool[] adapters = new bool[lines.Length * 3];

                foreach (string line in lines)
                {
                    int value = int.Parse(line);
                    adapters[value] = true;
                }

                int count1s = 0;
                int count3s = 1; // count the difference with the device
                int prevIndex = 0;
                for (int index = 0; index < adapters.Length; ++index)
                {
                    if (adapters[index])
                    {
                        int diff = index - prevIndex;
                        if (diff == 1) count1s++;
                        if (diff == 3) count3s++;
                        prevIndex = index;
                    }
                }
                int result = count1s * count3s;
                Console.WriteLine("Day 10 A: " + result);
            }
        }

        public class B
        {
            public static void Run()
            {
                string[] lines = System.IO.File.ReadAllLines(@"input10.txt");
                long[] adapters = new long[lines.Length * 3];

                long lastAdapter = 0;
                foreach (string line in lines)
                {
                    long value = long.Parse(line);
                    adapters[value] = 1;
                    lastAdapter = Math.Max(lastAdapter, value);
                }

                adapters[0] = 1;
                for (int index = 1; index < adapters.Length; ++index)
                {
                    if (adapters[index] != 0)
                    {
                        adapters[index] = 0;
                        for (int i = 1; i <= Math.Min(index, 3); ++i)
                        {
                            adapters[index] += adapters[index - i];
                        }
                    }
                }
                long result = adapters[lastAdapter];
                Console.WriteLine("Day 10 B: " + result);
            }
        }
    }
}
