using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Advent
{
    public class Day4
    {
        public class A
        {
            public static void Run()
            {
                string[] lines = System.IO.File.ReadAllLines(@"input4.txt");

                string[] fields = { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid", "cid" };

                Dictionary<string, int> flags = new Dictionary<string, int>();

                int fieldIndex = 0;
                foreach (string field in fields)
                {
                    flags.Add(field, 1 << fieldIndex++);
                }

                int result = 0;

                bool valid = false;
                int validMask = 0x7F;

                int mask = 0;
                foreach (string line in lines)
                {
                    if (!valid)
                    {
                        int index = line.IndexOf(':');
                        while (index >= 0)
                        {
                            string field = line.Substring(index - 3, 3);
                            int flag = 0;
                            if (flags.TryGetValue(field, out flag))
                            {
                                mask |= flag;
                                if ((mask & validMask) == validMask)
                                {
                                    result++;
                                    valid = true;
                                    break;
                                }
                            }
                            index = line.IndexOf(':', index + 1);
                        }
                    }
                    if (line.Length == 0)
                    {
                        mask = 0;
                        valid = false;
                    }
                }
                Console.WriteLine("Day 4 A: " + result);
            }
        }

        public class B
        {
            public static void Run()
            {
                string[] lines = System.IO.File.ReadAllLines(@"input4.txt");
                string lastLine = lines.Length > 0 ? lines[lines.Length - 1] : null;

                string[] fields = { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid", "cid" };
                string[] regex = {
                    @"^\d{4}$",
                    @"^\d{4}$",
                    @"^\d{4}$",
                    @"^\d+(cm|in)$",
                    @"^#(\d|[a-f]){6}$",
                    @"^(amb|blu|brn|gry|grn|hzl|oth)$",
                    @"^\d{9}$",
                    @".*"
                };

                Dictionary<string, int> indices = new Dictionary<string, int>();

                int fieldIndex = 0;
                foreach (string field in fields)
                {
                    indices.Add(field, fieldIndex++);
                }

                int result = 0;

                bool valid = true;
                int validMask = 0x7F;

                int mask = 0;
                foreach (string line in lines)
                {
                    int colon = line.IndexOf(':');
                    while (valid && colon >= 0)
                    {
                        string field = line.Substring(colon - 3, 3);
                        int index = 0;
                        if (indices.TryGetValue(field, out index))
                        {
                            int end = line.IndexOf(' ', colon);
                            string value = end >= 0 ? line.Substring(colon + 1, end - colon - 1) : line.Substring(colon + 1);
                            Match match = Regex.Match(value, regex[index]);
                            valid &= match.Success;
                            if (valid)
                            {
                                switch (index)
                                {
                                    case 0:
                                        int byr = Int32.Parse(value);
                                        valid &= byr >= 1920 && byr <= 2002;
                                        break;
                                    case 1:
                                        int iyr = Int32.Parse(value);
                                        valid &= iyr >= 2010 && iyr <= 2020;
                                        break;
                                    case 2:
                                        int eyr = Int32.Parse(value);
                                        valid &= eyr >= 2020 && eyr <= 2030;
                                        break;
                                    case 3:
                                        int hgt = Int32.Parse(value.Substring(0, value.Length - 2));
                                        if (value[value.Length - 1] == 'm')
                                            valid &= hgt >= 150 && hgt <= 193;
                                        else
                                            valid &= hgt >= 59 && hgt <= 76;
                                        break;
                                }
                            }

                            if (valid) mask |= 1 << index;
                        }

                        colon = line.IndexOf(':', colon + 1);
                    }
                    if (line.Length == 0 || object.ReferenceEquals(line, lastLine))
                    {
                        if (valid && (mask & validMask) == validMask)
                        {
                            result++;
                        }

                        mask = 0;
                        valid = true;
                    }
                }
                Console.WriteLine("Day 4 B: " + result);
            }
        }
    }
}
