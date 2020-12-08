using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Advent
{
    public class Day7
    {
        public class A
        {
            private static Dictionary<string, int> ids = new Dictionary<string, int>();
            private static List<HashSet<int>> canBeContainedList = new List<HashSet<int>>();

            public static void Run()
            {
                string[] lines = System.IO.File.ReadAllLines(@"input7.txt");

                foreach (string line in lines)
                {
                    Match match = Regex.Match(line, @"^(\w+ \w+) bags contain (?:no other bags|(?<count>\d+) (?<bag>\w+ \w+) bags?(?:, (?<count>\d+) (?<bag>\w+ \w+) bags?)*).$");
                    if (match.Success)
                    {
                        string container = match.Groups[1].Value;
                        int containerId = GetId(container);
                        for (int i = 0; i < match.Groups[3].Captures.Count; ++i)
                        {
                            string bag = match.Groups[3].Captures[i].Value;
                            int bagId = GetId(bag);
                            canBeContainedList[bagId].Add(containerId);
                        }
                    }
                }

                int goldId = GetId("shiny gold");
                HashSet<int> goldSet = new HashSet<int>();
                GetCanBeCointained(goldId, goldSet);

                int result = goldSet.Count;
                Console.WriteLine("Day 7 A: " + result);
            }

            private static int GetId(string name)
            {
                int id;
                if (!ids.TryGetValue(name, out id))
                {
                    id = canBeContainedList.Count;
                    ids.Add(name, id);
                    canBeContainedList.Add(new HashSet<int>());
                }
                return id;
            }

            private static void GetCanBeCointained(int id, HashSet<int> set)
            {
                HashSet<int> otherSet = canBeContainedList[id];
                foreach (int otherId in otherSet)
                {
                    if (!set.Contains(otherId))
                    {
                        set.Add(otherId);
                        GetCanBeCointained(otherId, set);
                    }
                }
            }
        }


        public class B
        {
            private static Dictionary<string, int> ids = new Dictionary<string, int>();
            private static List<Dictionary<int, int>> containsList = new List<Dictionary<int, int>>();

            public static void Run()
            {
                string[] lines = System.IO.File.ReadAllLines(@"input7.txt");

                foreach (string line in lines)
                {
                    Match match = Regex.Match(line, @"^(\w+ \w+) bags contain (?:no other bags|(?<count>\d+) (?<bag>\w+ \w+) bags?(?:, (?<count>\d+) (?<bag>\w+ \w+) bags?)*).$");
                    if (match.Success)
                    {
                        string container = match.Groups[1].Value;
                        int containerId = GetId(container);
                        for (int i = 0; i < match.Groups[3].Captures.Count; ++i)
                        {
                            string bag = match.Groups[3].Captures[i].Value;
                            int bagId = GetId(bag);
                            int count = Int32.Parse(match.Groups[2].Captures[i].Value);
                            containsList[containerId].Add(bagId, count);
                        }
                    }
                }

                int goldId = GetId("shiny gold");
                int result = Contains(goldId) - 1; //not counting itself
                Console.WriteLine("Day 7 B: " + result);
            }

            private static int GetId(string name)
            {
                int id;
                if (!ids.TryGetValue(name, out id))
                {
                    id = containsList.Count;
                    ids.Add(name, id);
                    containsList.Add(new Dictionary<int, int>());
                }
                return id;
            }

            private static int Contains(int id)
            {
                int count = 1;
                Dictionary<int, int> contains = containsList[id];
                foreach (var kvp in contains)
                {
                    count += Contains(kvp.Key) * kvp.Value;
                }
                return count;
            }
        }
    }
}
