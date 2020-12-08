using System;
using System.Collections.Generic;
using System.Text;

namespace Advent
{
    public class Day6
    {
        public class A
        {
            public static void Run()
            {
                string[] lines = System.IO.File.ReadAllLines(@"input6.txt");

                int result = 0;

                int mask = 0;
                int count = 0;
                for (int i = 0; i < lines.Length; ++i)
                {
                    string line = lines[i];
                    for (int j = 0; j < line.Length; ++j)
                    {
                        int flag = 1 << line[j] - 'a';
                        if ((mask & flag) == 0)
                        {
                            mask |= flag;
                            count++;
                        }
                    }
                    if (line.Length == 0 || i == lines.Length - 1)
                    {
                        result += count;
                        mask = 0;
                        count = 0;
                    }
                }
                Console.WriteLine("Day 6 A: " + result);
            }
        }

        public class B
        {
            public static void Run()
            {
                string[] lines = System.IO.File.ReadAllLines(@"input6.txt");

                int result = 0;

                int mask = 0x7FFFFFFF;
                for (int i = 0; i < lines.Length; ++i)
                {
                    string line = lines[i];
                    if (line.Length != 0)
                    {
                        int personMask = 0;
                        for (int j = 0; j < line.Length; ++j)
                        {
                            int flag = 1 << line[j] - 'a';
                            personMask |= flag;
                        }
                        mask &= personMask;
                    }
                    if (line.Length == 0 || i == lines.Length - 1)
                    {
                        int count = 0;
                        while (mask != 0)
                        {
                            count += mask & 1;
                            mask >>= 1;
                        }
                        result += count;
                        mask = 0x7FFFFFFF;
                    }
                }
                Console.WriteLine("Day 6 B: " + result);
            }
        }
    }
}
