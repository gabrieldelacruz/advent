using System;
using System.Collections.Generic;
using System.Text;

namespace Advent
{
    public class Day1
    {
        public class A
        {
            public static void Run()
            {
                string[] lines = System.IO.File.ReadAllLines(@"input1.txt");

                const int total = 2020;
                bool[] values = new bool[total + 1];

                int result = -1;
                foreach (string line in lines)
                {
                    int value = Int32.Parse(line);
                    if (value >= 0 && value <= total)
                    {
                        int complementary = total - value;
                        if (values[complementary])
                        {
                            result = value * complementary;
                            break;
                        }
                        values[value] = true;
                    }
                }
                Console.WriteLine("Day 1 A: " + (result >= 0 ? result.ToString() : "Not found"));
            }
        }

        public class B
        {
            public static void Run()
            {
                string[] lines = System.IO.File.ReadAllLines(@"input1.txt");

                const int total = 2020;
                HashSet<int> values = new HashSet<int>(lines.Length);

                int result = -1;
                foreach (string line in lines)
                {
                    int value = Int32.Parse(line);
                    if (value >= 0 && value <= total)
                    {
                        values.Add(value);
                    }
                }

                foreach (int valueA in values)
                {
                    foreach (int valueB in values)
                    {
                        int value = valueA + valueB;
                        if (value <= total)
                        {
                            int complementary = total - value;
                            if (values.Contains(complementary))
                            {
                                result = valueA * valueB * complementary;
                                break;
                            }
                        }
                    }
                }

                Console.WriteLine("Day 1 B: " + (result >= 0 ? result.ToString() : "Not found"));
            }
        }
    }
}
