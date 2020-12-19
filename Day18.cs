using System;
using System.Collections.Generic;

namespace Advent
{
    public class Day18
    {
        public class A
        {
            public static void Run()
            {
                string[] lines = System.IO.File.ReadAllLines(@"input18.txt");
                long result = 0;

                Stack<Tuple<long, char>> stack = new Stack<Tuple<long, char>>();
                foreach (string line in lines)
                {
                    long value = 0;
                    char operation = '+';
                    int index = 0;
                    while (index < line.Length)
                    {
                        if (line[index] == '(')
                        {
                            stack.Push(new Tuple<long, char>(value, operation));
                            value = 0;
                            operation = '+';
                            index++;
                        }
                        else
                        {
                            long operand = 0;
                            while (index < line.Length && line[index] >= '0' && line[index] <= '9')
                            {
                                operand = operand * 10 + (line[index++] - '0');
                            }

                            bool popped;
                            do
                            {
                                popped = false;
                                switch (operation)
                                {
                                    case '+':
                                        value += operand;
                                        break;
                                    case '*':
                                        value *= operand;
                                        break;
                                }
                                if (index < line.Length && line[index] == ')')
                                {
                                    var previous = stack.Pop();
                                    operand = value;
                                    value = previous.Item1;
                                    operation = previous.Item2;
                                    popped = true;
                                    index++;
                                }
                            } while (popped);

                            if (index < line.Length)
                            {
                                operation = line[++index];
                                index += 2;
                            }
                        }
                    }
                    System.Diagnostics.Debug.Assert(stack.Count == 0);
                    result += value;
                }

                Console.WriteLine("Day 18 A: " + result);
            }
        }

        public class B
        {
            public static void Run()
            {
                string[] lines = System.IO.File.ReadAllLines(@"input18.txt");
                long result = 0;

                Stack<Tuple<long, long, char>> stack = new Stack<Tuple<long, long, char>>();
                foreach (string line in lines)
                {
                    long value = 0;
                    long multiplier = 1;
                    char operation = '+';
                    int index = 0;
                    while (index < line.Length)
                    {
                        if (line[index] == '(')
                        {
                            stack.Push(new Tuple<long, long, char>(value, multiplier, operation));
                            value = 0;
                            multiplier = 1;
                            operation = '+';
                            index++;
                        }
                        else
                        {
                            long operand = 0;
                            while (index < line.Length && line[index] >= '0' && line[index] <= '9')
                            {
                                operand = operand * 10 + (line[index++] - '0');
                            }

                            bool popped;
                            do
                            {
                                popped = false;
                                switch (operation)
                                {
                                    case '+':
                                        value += operand;
                                        break;
                                    case '*':
                                        multiplier *= value;
                                        value = operand;
                                        break;
                                }
                                if (index < line.Length && line[index] == ')')
                                {
                                    var previous = stack.Pop();
                                    operand = value * multiplier;
                                    value = previous.Item1;
                                    multiplier = previous.Item2;
                                    operation = previous.Item3;
                                    popped = true;
                                    index++;
                                }
                            } while (popped);

                            if (index < line.Length)
                            {
                                operation = line[++index];
                                index += 2;
                            }
                        }
                    }
                    System.Diagnostics.Debug.Assert(stack.Count == 0);
                    result += value * multiplier;
                }

                Console.WriteLine("Day 18 B: " + result);
            }
        }
    }
}
