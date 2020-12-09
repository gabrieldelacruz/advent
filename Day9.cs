using System;

namespace Advent
{
    public class Day9
    {
        public class A
        {
            public static void Run()
            {
                string[] lines = System.IO.File.ReadAllLines(@"input9.txt");

                long[] window = new long[25];

                for (int i = 0; i < window.Length; ++i)
                {
                    window[i] = long.Parse(lines[i]);
                }

                long result = 0;
                int index = 0;
                for (int i = window.Length; i < lines.Length; ++i)
                {
                    long value = long.Parse(lines[i]);

                    bool found = false;
                    long half = (long)Math.Ceiling(value / 2.0f);
                    for (int j = 0; j < window.Length; ++j)
                    {
                        if (window[j] < half)
                        {
                            if (Array.IndexOf(window, value - window[j]) >= 0)
                            {
                                found = true;
                                break;
                            }
                        }
                    }

                    if (!found)
                    {
                        result = value;
                        break;
                    }

                    window[index] = value;
                    index = (index + 1) % window.Length;
                }

                Console.WriteLine("Day 9 A: " + result);
            }
        }

        public class B
        {
            public static void Run()
            {
                string[] lines = System.IO.File.ReadAllLines(@"input9.txt");
                long[] values = new long[lines.Length];

                for (int i = 0; i < lines.Length; ++i)
                {
                    values[i] = long.Parse(lines[i]);
                }

                int start = 0;
                int end = 1;
                long target = 1639024365; // Hardcoded result from 9A
                long accum = values[0] + values[1];
                while (accum != target)
                {
                    if (accum > target)
                        accum -= values[start++];
                    else
                        accum += values[++end];
                }

                long minValue = long.MaxValue;
                long maxValue = long.MinValue;
                for (int i = start; i <= end; ++i)
                {
                    minValue = Math.Min(minValue, values[i]);
                    maxValue = Math.Max(maxValue, values[i]);
                }

                long result = minValue + maxValue;
                Console.WriteLine("Day 9 B: " + result);
            }
        }
    }
}
