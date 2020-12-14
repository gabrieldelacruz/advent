using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Advent
{
    public class Day14
    {
        public class A
        {
            public static void Run()
            {
                string[] lines = System.IO.File.ReadAllLines(@"input14.txt");

                Dictionary<ulong, ulong> memory = new Dictionary<ulong, ulong>();

                ulong mask0s = ulong.MaxValue;
                ulong mask1s = 0;

                foreach (string line in lines)
                {
                    if (line[1] == 'a')
                    {
                        // mask
                        mask0s = ulong.MaxValue;
                        mask1s = 0;
                        int charIndex = 7;
                        for (int i = 35; i >= 0; i--)
                        {
                            switch (line[charIndex++])
                            {
                                case '0':
                                    mask0s ^= 1UL << i;
                                    break;
                                case '1':
                                    mask1s |= 1UL << i;
                                    break;
                            }
                        }
                    }
                    else
                    {
                        // mem
                        int bracket = line.IndexOf(']', 5);
                        ulong address = ulong.Parse(line.Substring(4, bracket - 4));
                        ulong value = ulong.Parse(line.Substring(bracket + 4));
                        value &= mask0s;
                        value |= mask1s;
                        memory[address] = value;
                    }
                }

                ulong result = 0;
                foreach (ulong value in memory.Values)
                {
                    result += value;
                }

                Console.WriteLine("Day 14 A: " + result);
            }
        }

        public class B
        {
            private class Node
            {
                public int entryIndex;
                public Node index0;
                public Node index1;
            }
            private struct Entry
            {
                public ulong address;
                public ulong value;
                public ulong maskXs;
                public int bitCount;
            }

            static List<Entry> entries = null;

            private static void AddEntry(ref Node node, int entryIndex, ulong bitMask)
            {
                Entry entry = entries[entryIndex];
                if (node == null)
                {
                    node = new Node();
                    node.entryIndex = entryIndex;
                }
                else if (node.index0 == null && node.index1 == null)
                {
                    Entry nodeEntry = entries[node.entryIndex];
                    if (((entry.maskXs | nodeEntry.maskXs) == entry.maskXs) && ((nodeEntry.address & ~entry.maskXs) == entry.address))
                    {
                        // overwrite previous entry
                        entries[node.entryIndex] = new Entry();
                        node.entryIndex = entryIndex;
                    }
                    else
                    {
                        int prevEntryIndex = node.entryIndex;
                        node.entryIndex = -1;
                        AdvanceTreeLevel(ref node, prevEntryIndex, bitMask);
                        AdvanceTreeLevel(ref node, entryIndex, bitMask);
                    }
                }
                else
                {
                    AdvanceTreeLevel(ref node, entryIndex, bitMask);
                }
            }

            private static void AdvanceTreeLevel(ref Node node, int entryIndex, ulong bitMask)
            {
                Debug.Assert(node.entryIndex == -1);
                Debug.Assert(bitMask > 0);

                Entry entry = entries[entryIndex];
                if ((entry.maskXs & bitMask) != 0)
                {
                    // split
                    entry.address &= ~bitMask;
                    entry.maskXs &= ~bitMask;
                    entry.bitCount--;

                    entries[entryIndex] = entry;
                    AddEntry(ref node.index0, entryIndex, bitMask >> 1);

                    int newEntryIndex = entries.Count;
                    entry.address |= bitMask;
                    entries.Add(entry);
                    AddEntry(ref node.index1, newEntryIndex, bitMask >> 1);
                }
                else
                {
                    if ((entry.address & bitMask) == 0)
                    {
                        AddEntry(ref node.index0, entryIndex, bitMask >> 1);
                    }
                    else
                    {
                        AddEntry(ref node.index1, entryIndex, bitMask >> 1);
                    }
                }
            }

            public static void Run()
            {
                string[] lines = System.IO.File.ReadAllLines(@"input14.txt");

                Node memoryTree = null;
                entries = new List<Entry>(1024);

                ulong mask1s = 0;
                ulong maskXs = 0;
                int bitCount = 0;

                foreach (string line in lines)
                {
                    if (line[1] == 'a')
                    {
                        // mask
                        mask1s = 0;
                        maskXs = 0;
                        bitCount = 0;
                        int charIndex = 7;
                        for (int i = 35; i >= 0; i--)
                        {
                            switch (line[charIndex++])
                            {
                                case '1':
                                    mask1s |= 1UL << i;
                                    break;
                                case 'X':
                                    maskXs |= 1UL << i;
                                    bitCount++;
                                    break;
                            }
                        }
                    }
                    else
                    {
                        // mem
                        int bracket = line.IndexOf(']', 5);
                        ulong address = ulong.Parse(line.Substring(4, bracket - 4));
                        address |= mask1s;
                        address &= ~maskXs;
                        ulong value = ulong.Parse(line.Substring(bracket + 4));

                        Entry entry = new Entry();
                        entry.address = address;
                        entry.value = value;
                        entry.maskXs = maskXs;
                        entry.bitCount = bitCount;

                        int entryIndex = entries.Count;
                        entries.Add(entry);

                        ulong bitMask = 1UL << 35;
                        AddEntry(ref memoryTree, entryIndex, bitMask);
                    }
                }

                ulong result = 0;
                foreach (Entry entry in entries)
                {
                    result += entry.value * (1UL << entry.bitCount);
                }

                Console.WriteLine("Day 14 B: " + result);
            }
        }
    }
}
