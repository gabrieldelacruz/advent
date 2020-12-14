using System;
using System.Collections.Generic;

namespace Advent
{
    public class Day13
    {
        public class A
        {
            public static void Run()
            {
                string[] lines = System.IO.File.ReadAllLines(@"input13.txt");

                int timestamp = int.Parse(lines[0]);
                int minBusID = 0;
                int minWait = timestamp;
                int start = 0;
                int end = 0;
                string line = lines[1];
                while (start < line.Length)
                {
                    end = start + 1;
                    while (end < line.Length && line[end] != ',') end++;
                    if (line[start] != 'x')
                    {
                        int busID = int.Parse(line.Substring(start, end - start));
                        int wait = busID - timestamp % busID;
                        if (wait < minWait)
                        {
                            minBusID = busID;
                            minWait = wait;
                        }
                    }
                    start = end + 1;
                }

                int result = minBusID * minWait;
                Console.WriteLine("Day 13 A: " + result);
            }
        }

        public class B
        {
            public static void Run()
            {
                string[] lines = System.IO.File.ReadAllLines(@"input13.txt");

                int start = 0;
                int end = 0;
                string line = lines[1];

                List<int> busIDs = new List<int>(64);
                List<int> delays = new List<int>(64);

                // Using Chinese remainder theorem

                int delay = 0;
                long N = 1;
                while (start < line.Length)
                {
                    end = start + 1;
                    while (end < line.Length && line[end] != ',') end++;
                    if (line[start] != 'x')
                    {
                        int busID = int.Parse(line.Substring(start, end - start));
                        N *= busID;
                        busIDs.Add(busID);
                        delays.Add(delay + busID);
                    }
                    delay--;
                    start = end + 1;
                }

                long timestamp = 0;
                for (int i = 0; i < busIDs.Count; ++i)
                {
                    int busID = busIDs[i];

                    long bi = N / busID;

                    long s, t;
                    long mcd = ExtendedEuclidean(bi, busID, out s, out t);
                    System.Diagnostics.Debug.Assert(mcd == 1);
                    s = (s + busID) % busID;

                    timestamp += delays[i] * bi * s;

                }
                timestamp %= N;

                Console.WriteLine("Day 13 B: " + timestamp);
            }

            private static long ExtendedEuclidean(long a, long b, out long s, out long t)
            {
                s = 1;
                t = 0;
                long s0 = 0, t0 = 1;
                while (b != 0)
                {
                    long q = a / b;
                    long r = a % b;
                    a = b;
                    b = r;
                    long sTemp = s;
                    long tTemp = t;
                    s = s0;
                    t = t0;
                    s0 = sTemp - s0 * q;
                    t0 = tTemp - t0 * q;
                }
                return a;
            }
        }
    }
}
