using System;
using System.Collections.Generic;

namespace Advent
{
    public class Day17
    {
        public class A
        {
            public static void Run()
            {
                string[] lines = System.IO.File.ReadAllLines(@"input17.txt");

                const uint activeFlag = 0x80000000;
                const uint cycleMask = 0x0000FF00;
                const uint countMask = 0x000000FF;
                Dictionary<Tuple<int, int, int>, uint>[] cubes = new Dictionary<Tuple<int, int, int>, uint>[2];
                cubes[0] = new Dictionary<Tuple<int, int, int>, uint>(100);
                cubes[1] = new Dictionary<Tuple<int, int, int>, uint>(100);

                for (int j = 0; j < lines.Length; ++j)
                {
                    string line = lines[j];
                    for (int i = 0; i < line.Length; ++i)
                    {
                        bool active = line[i] == '#';
                        if (active)
                        {
                            cubes[0].Add(new Tuple<int, int, int>(i, j, 0), activeFlag | cycleMask | 3);
                        }
                    }
                }

                int read = 0;
                int write = 1;
                int result = 0;
                uint cycleMaskRead = 0;
                uint cycleMaskWrite = cycleMask;
                for (uint cycle = 0; cycle <= 6; ++cycle)
                {
                    result = 0;
                    cycleMaskRead = cycleMaskWrite;
                    cycleMaskWrite = cycle << 8;
                    foreach (var cube in cubes[read])
                    {
                        uint neighbors = cube.Value & countMask;
                        if ((cube.Value & cycleMask) != cycleMaskRead) neighbors = 0;
                        bool active = (cube.Value & activeFlag) != 0;
                        active = (!active && neighbors == 3) || (active && neighbors >= 2 && neighbors <=3);
                        if (active)
                        {
                            for (int x = -1; x <= 1; ++x)
                            {
                                for (int y = -1; y <= 1; ++y)
                                {
                                    for (int z = -1; z <= 1; ++z)
                                    {
                                        if (x != 0 || y != 0 || z != 0)
                                        {
                                            var key = new Tuple<int, int, int>(cube.Key.Item1 + x, cube.Key.Item2 + y, cube.Key.Item3 + z);
                                            uint cubeNeighbor = 0;
                                            cubes[write].TryGetValue(key, out cubeNeighbor);
                                            if ((cubeNeighbor & cycleMask) != cycleMaskWrite)
                                            {
                                                cubeNeighbor = (cubeNeighbor & activeFlag) | cycleMaskWrite;
                                            }
                                            cubes[write][key] = cubeNeighbor + 1;
                                        }
                                    }
                                }
                            }
                            result++;
                        }
                        uint cubeValue = 0;
                        cubes[write].TryGetValue(cube.Key, out cubeValue);
                        cubes[write][cube.Key] = (cubeValue & ~activeFlag) | (active ? activeFlag : 0);
                    }
                    int temp = read;
                    read = write;
                    write = temp;
                }

                Console.WriteLine("Day 17 A: " + result);
            }
        }

        public class B
        {
            public static void Run()
            {
                string[] lines = System.IO.File.ReadAllLines(@"input17.txt");

                const uint activeFlag = 0x80000000;
                const uint cycleMask = 0x0000FF00;
                const uint countMask = 0x000000FF;
                Dictionary<Tuple<int, int, int, int>, uint>[] cubes = new Dictionary<Tuple<int, int, int, int>, uint>[2];
                cubes[0] = new Dictionary<Tuple<int, int, int, int>, uint>(100);
                cubes[1] = new Dictionary<Tuple<int, int, int, int>, uint>(100);

                for (int j = 0; j < lines.Length; ++j)
                {
                    string line = lines[j];
                    for (int i = 0; i < line.Length; ++i)
                    {
                        bool active = line[i] == '#';
                        if (active)
                        {
                            cubes[0].Add(new Tuple<int, int, int, int>(i, j, 0, 0), activeFlag | cycleMask | 3);
                        }
                    }
                }

                int read = 0;
                int write = 1;
                int result = 0;
                uint cycleMaskRead = 0;
                uint cycleMaskWrite = cycleMask;
                for (uint cycle = 0; cycle <= 6; ++cycle)
                {
                    result = 0;
                    cycleMaskRead = cycleMaskWrite;
                    cycleMaskWrite = cycle << 8;
                    foreach (var cube in cubes[read])
                    {
                        uint neighbors = cube.Value & countMask;
                        if ((cube.Value & cycleMask) != cycleMaskRead) neighbors = 0;
                        bool active = (cube.Value & activeFlag) != 0;
                        active = (!active && neighbors == 3) || (active && neighbors >= 2 && neighbors <= 3);
                        if (active)
                        {
                            for (int x = -1; x <= 1; ++x)
                            {
                                for (int y = -1; y <= 1; ++y)
                                {
                                    for (int z = -1; z <= 1; ++z)
                                    {
                                        for (int w = -1; w <= 1; ++w)
                                        {
                                            if (x != 0 || y != 0 || z != 0 || w != 0)
                                            {
                                                var key = new Tuple<int, int, int, int>(cube.Key.Item1 + x, cube.Key.Item2 + y, cube.Key.Item3 + z, cube.Key.Item4 + w);
                                                uint cubeNeighbor = 0;
                                                cubes[write].TryGetValue(key, out cubeNeighbor);
                                                if ((cubeNeighbor & cycleMask) != cycleMaskWrite)
                                                {
                                                    cubeNeighbor = (cubeNeighbor & activeFlag) | cycleMaskWrite;
                                                }
                                                cubes[write][key] = cubeNeighbor + 1;
                                            }
                                        }
                                    }
                                }
                            }
                            result++;
                        }
                        uint cubeValue = 0;
                        cubes[write].TryGetValue(cube.Key, out cubeValue);
                        cubes[write][cube.Key] = (cubeValue & ~activeFlag) | (active ? activeFlag : 0);
                    }
                    int temp = read;
                    read = write;
                    write = temp;
                }

                Console.WriteLine("Day 17 B: " + result);
            }
        }
    }
}
