using System;

namespace Advent
{
    public class Day8
    {
        public class A
        {
            public static void Run()
            {
                string[] lines = System.IO.File.ReadAllLines(@"input8.txt");

                string[] instructions = new string[] { "nop", "jmp", "acc" };

                bool[] visited = new bool[lines.Length];

                int acc = 0;
                int pc = 0;
                while(!visited[pc])
                {
                    visited[pc] = true;
                    string line = lines[pc];
                    int opcode = Array.IndexOf(instructions, line.Substring(0, 3));
                    switch (opcode)
                    {
                        case 0: // nop
                            pc++;
                            break;
                        case 1: // jmp
                            pc += Int32.Parse(line.Substring(4));
                            break;
                        case 2: // acc
                            acc += Int32.Parse(line.Substring(4));
                            pc++;
                            break;
                    }
                }
                Console.WriteLine("Day 8 A: " + acc);
            }
        }

        public class B
        {
            public static void Run()
            {
                string[] lines = System.IO.File.ReadAllLines(@"input8.txt");

                string[] instructions = new string[] { "nop", "jmp", "acc" };

                int[] program = new int[lines.Length];
                int[] visited = new int[lines.Length];

                for (int i = 0; i < lines.Length; ++i)
                {
                    string line = lines[i];
                    int opcode = Array.IndexOf(instructions, line.Substring(0, 3));
                    int param = Int32.Parse(line.Substring(4));
                    program[i] = param << 4 | opcode;
                    visited[i] = -2;
                }

                int acc = 0;
                int pc = 0;
                int pcHacked = -1;
                int accHacked = 0;
                while (pc != program.Length)
                {
                    if (visited[pc] == pcHacked)
                    {
                        pc = pcHacked;
                    }

                    int instruction = program[pc];
                    int opcode = instruction & 0xF;
                    int param = instruction >> 4;

                    if (pcHacked == -1)
                    {
                        if (opcode == 0 || opcode == 1)
                        {
                            pcHacked = pc;
                            accHacked = acc;
                            opcode = (opcode == 0) ? 1 : 0;
                        }
                    }
                    else if (pc == pcHacked)
                    {
                        pcHacked = -1;
                        acc = accHacked;
                    }
                    else
                    {
                        visited[pc] = pcHacked;
                    }

                    switch (opcode)
                    {
                        case 0: // nop
                            pc++;
                            break;
                        case 1: // jmp
                            pc += param;
                            break;
                        case 2: // acc
                            acc += param;
                            pc++;
                            break;
                    }
                }
                Console.WriteLine("Day 8 B: " + acc);
            }
        }

    }
}
