using System;

namespace Advent
{
    public class Day12
    {
        public class A
        {
            public static void Run()
            {
                string[] lines = System.IO.File.ReadAllLines(@"input12.txt");

                int x = 0, y = 0;
                int dirX = 1, dirY = 0;
                foreach (string line in lines)
                {
                    int value = int.Parse(line.Substring(1));
                    switch (line[0])
                    {
                        case 'W':
                            x -= value;
                            break;
                        case 'N':
                            y -= value;
                            break;
                        case 'E':
                            x += value;
                            break;
                        case 'S':
                            y += value;
                            break;
                        case 'L':
                            value = 360 - value;
                            goto case 'R'; // fall through
                        case 'R':
                            int prevDirX = dirX;
                            int prevDirY = dirY;
                            switch (value)
                            {
                                case 90:
                                    dirX = -prevDirY;
                                    dirY = prevDirX;
                                    break;
                                case 180:
                                    dirX = -prevDirX;
                                    dirY = -prevDirY;
                                    break;
                                case 270:
                                    dirX = prevDirY;
                                    dirY = -prevDirX;
                                    break;
                            }
                            break;
                        case 'F':
                            x += value * dirX;
                            y += value * dirY;
                            break;
                    }
                }
                int result = Math.Abs(x) + Math.Abs(y);
                Console.WriteLine("Day 12 A: " + result);
            }
        }

        public class B
        {
            public static void Run()
            {
                string[] lines = System.IO.File.ReadAllLines(@"input12.txt");

                int x = 0, y = 0;
                int dirX = 10, dirY = -1;
                foreach (string line in lines)
                {
                    int value = int.Parse(line.Substring(1));
                    switch (line[0])
                    {
                        case 'W':
                            dirX -= value;
                            break;
                        case 'N':
                            dirY -= value;
                            break;
                        case 'E':
                            dirX += value;
                            break;
                        case 'S':
                            dirY += value;
                            break;
                        case 'L':
                            value = 360 - value;
                            goto case 'R'; // fall through
                        case 'R':
                            int prevDirX = dirX;
                            int prevDirY = dirY;
                            switch (value)
                            {
                                case 90:
                                    dirX = -prevDirY;
                                    dirY = prevDirX;
                                    break;
                                case 180:
                                    dirX = -prevDirX;
                                    dirY = -prevDirY;
                                    break;
                                case 270:
                                    dirX = prevDirY;
                                    dirY = -prevDirX;
                                    break;
                            }
                            break;
                        case 'F':
                            x += value * dirX;
                            y += value * dirY;
                            break;
                    }
                }
                int result = Math.Abs(x) + Math.Abs(y);
                Console.WriteLine("Day 12 B: " + result);
            }
        }
    }
}
