using System;
using System.Collections.Generic;

namespace Advent
{
    public class Day11
    {
        public class A
        {
            private static int read = 0;
            private static int write = 1;
            private static sbyte[,,] grid = null;

            private const sbyte floor = -1;
            private const sbyte occupiedFlag = 0x40;

            public static void Run()
            {
                string[] lines = System.IO.File.ReadAllLines(@"input11.txt");
                grid = new sbyte[lines[0].Length, lines.Length, 2];

                for (int j = 0; j < grid.GetLength(1); ++j)
                {
                    for (int i = 0; i < grid.GetLength(0); ++i)
                    {
                        grid[i, j, 0] = lines[j][i] == '.' ? floor : (sbyte)0;
                    }
                }

                int occupieds = 0;
                bool changed;
                do
                {
                    changed = false;
                    for (int j = 0; j < grid.GetLength(1); ++j)
                    {
                        for (int i = 0; i < grid.GetLength(0); ++i)
                        {
                            sbyte value = grid[i, j, read];
                            if (value != floor)
                            {
                                sbyte change = (value == 0) ? (sbyte)1 : (value >= occupiedFlag + 4) ? (sbyte)-1 : (sbyte)0;
                                if (change != 0)
                                {
                                    value ^= occupiedFlag;
                                    occupieds += change;
                                    changed = true;
                                }
                                value += CheckNeighbour(i - 1, j - 1, change);
                                value += CheckNeighbour(i, j - 1, change);
                                value += CheckNeighbour(i + 1, j - 1, change);
                                value += CheckNeighbour(i - 1, j, change);
                            }
                            grid[i, j, write] = value;
                        }
                    }
                    int temp = read;
                    read = write;
                    write = temp;
                } while (changed);

                Console.WriteLine("Day 11 A: " + occupieds);
            }

            private static sbyte CheckNeighbour(int i, int j, sbyte neighbourChange)
            {
                sbyte change = 0;
                if (i >= 0 && i < grid.GetLength(0) && j >= 0 && j < grid.GetLength(1) && grid[i, j, read] != floor)
                {
                    grid[i, j, write] += neighbourChange;
                    change += (grid[i, j, read] & occupiedFlag) != 0 ? (sbyte)-1 : (sbyte)0;
                    change += (grid[i, j, write] & occupiedFlag) != 0 ? (sbyte)1 : (sbyte)0;
                }
                return change;
            }
        }

        public class B
        {
            public static void Run()
            {
                string[] lines = System.IO.File.ReadAllLines(@"input11.txt");
                Dictionary<uint, bool[]> seats = new Dictionary<uint, bool[]>();

                int width = lines[0].Length;
                int height = lines.Length;
                for (int y = 0; y < height; ++y)
                {
                    for (int x = 0; x < width; ++x)
                    {
                        if (lines[y][x] != '.')
                        {
                            seats.Add(GetKey(x, y), new bool[] { false, false });
                        }
                    }
                }

                int read = 0;
                int write = 1;
                int occupieds = 0;
                bool changed;
                do
                {
                    changed = false;
                    for (int y = 0; y < height; ++y)
                    {
                        for (int x = 0; x < width; ++x)
                        {
                            bool[] occupied;
                            if (seats.TryGetValue(GetKey(x, y), out occupied))
                            {
                                int neighbours = 0;
                                for (int j = -1; j <= 1; ++j)
                                {
                                    for (int i = -1; i <= 1; ++i)
                                    {
                                        if (i != 0 || j != 0)
                                        {
                                            int x0 = x;
                                            int y0 = y;

                                            bool[] occupiedNeighbour = null;
                                            do
                                            {
                                                x0 += i;
                                                y0 += j;
                                            } while (x0 >= 0 && x0 < width && y0 >= 0 && y0 < height && !seats.TryGetValue(GetKey(x0, y0), out occupiedNeighbour));
                                            if (occupiedNeighbour != null && occupiedNeighbour[read]) neighbours++;
                                        }
                                    }
                                }
                                if (!occupied[read] && neighbours == 0)
                                {
                                    occupied[write] = true;
                                    occupieds++;
                                }
                                else if (occupied[read] && neighbours >= 5)
                                {
                                    occupied[write] = false;
                                    occupieds--;
                                }
                                else
                                {
                                    occupied[write] = occupied[read];
                                }
                                changed |= occupied[write] != occupied[read];
                            }
                        }
                    }
                    int temp = read;
                    read = write;
                    write = temp;
                } while (changed);

                Console.WriteLine("Day 11 B: " + occupieds);
            }

            private static uint GetKey(int x, int y)
            {
                return x >= 0 && y >= 0 ? ((uint)x << 16 | (uint)y): 0xFFFFFFFFU; 
            }
        }
    }
}
