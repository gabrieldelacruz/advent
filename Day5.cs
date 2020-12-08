using System;
using System.Collections.Generic;
using System.Text;

namespace Advent
{
    public class Day5
    {
        public class A
        {
            public static void Run()
            {
                string[] lines = System.IO.File.ReadAllLines(@"input5.txt");

                int result = 0;
                foreach (string line in lines)
                {
                    int id = 0;
                    for (int i = 0; i < line.Length; ++i)
                    {
                        id <<= 1;
                        id |= (line[i] == 'B' || line[i] == 'R') ? 1 : 0;
                    }
                    result = Math.Max(result, id);
                }
                Console.WriteLine("Day 5 A: " + result);
            }
        }

        public class B
        {
            public static void Run()
            {
                string[] lines = System.IO.File.ReadAllLines(@"input5.txt");

                int[] rows = new int[128];

                const int fullRow = 0xFF;

                int result = 0;
                foreach (string line in lines)
                {
                    int row = 0;
                    int seat = 0;
                    for (int i = 0; i < 7; ++i)
                    {
                        row <<= 1;
                        row |= (line[i] == 'B') ? 1 : 0;
                    }
                    for (int i = 7; i < 10; ++i)
                    {
                        seat <<= 1;
                        seat |= (line[i] == 'R') ? 1 : 0;
                    }
                    rows[row] |= 1 << seat;
                }
                bool firstRows = true;
                for (int row = 0; row < rows.Length; ++row)
                {
                    int seats = rows[row];
                    if (firstRows)
                    {
                        if (seats != 0) firstRows = false;
                    }
                    else
                    {
                        if (seats != fullRow)
                        {
                            int seat = 0;
                            while ((seats & (1 << seat)) != 0) seat++;

                            result = row << 3 | seat;
                            break;
                        }
                    }
                }
                Console.WriteLine("Day 5 B: " + result);
            }
        }
    }
}
