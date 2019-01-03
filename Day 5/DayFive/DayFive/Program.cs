using System;
using System.IO;
using System.Text.RegularExpressions;

namespace DayFive
{
    class Program
    {
        static void Main(string[] args)
        {
            // TODO - Refactor solution

            /* Dejv 2018 - Advent of code Day 5 - Alchemical Reduction */

            // Part 1

            // Read file
            // "D:\Skrivbord\AdventOfCode2018\Day 5\Day5Input.txt"
            // "D:\Skrivbord\AdventOfCode2018\Day 5\Test.txt"
            using (var myReader = File.OpenText(@"D:\Skrivbord\AdventOfCode2018\Day 5\Day5Input.txt"))
            {
                System.Diagnostics.Stopwatch.StartNew();
                
                // Get the polymer
                var hej = myReader.ReadToEnd();
                var polymer2 = new string(hej);
                
                // dabAcCaCBAcCcaDA

                //string newPolymer = "";
                //char previousChar = ' ';

                //bool matchFound = true;

                //while (matchFound)
                //{
                //    matchFound = false;
                //    for (int i = 0; i < polymer.Length; i++)
                //    {
                //        if (polymer[i] == previousChar)
                //        {
                //            // Do Nothing
                //            previousChar = polymer[i];
                //        }
                //        else if (polymer[i].ToString().ToUpper() == previousChar.ToString() || polymer[i].ToString().ToLower() == previousChar.ToString())
                //        {
                //            previousChar = polymer[i];
                //            newPolymer = polymer.Remove((i - 1), 2);
                //            matchFound = true;
                //            break;
                //        }
                //        else
                //        {
                //            previousChar = polymer[i];
                //        }
                //    }
                //    previousChar = ' ';
                //    polymer = newPolymer;
                //    Console.WriteLine(polymer.Length);
                //}
                
                //// Go through each character in the polymer
                //for(int i = 0; i < newPolymer.Length; i++)
                //{
                //    Console.Write(newPolymer[i]);
                //}


                // Part 2 - Wierd solution

                string shortestPolymer = polymer2;

                string smallAlpha = "abcdefgehijklmnopqrstuvwxyz";
                string bigAlpha = "ABCDEFGEHIJKLMNOPQRSTUVWXYZ";

                for (int a = 0; a < smallAlpha.Length; a++)
                {
                    string polymer = polymer2.Replace(smallAlpha[a], ' ').Replace(bigAlpha[a], ' ');
                    polymer = Regex.Replace(polymer, $" ", "");

                    string newPolymer = "";
                    char previousChar = ' ';

                    bool matchFound = true;

                    while (matchFound)
                    {
                        matchFound = false;
                        for (int i = 0; i < polymer.Length; i++)
                        {
                            if (polymer[i] == previousChar)
                            {
                                // Do Nothing
                                previousChar = polymer[i];
                            }
                            else if (polymer[i].ToString().ToUpper() == previousChar.ToString() || polymer[i].ToString().ToLower() == previousChar.ToString())
                            {
                                previousChar = polymer[i];
                                newPolymer = polymer.Remove((i - 1), 2);
                                matchFound = true;
                                break;
                            }
                            else
                            {
                                previousChar = polymer[i];
                            }
                        }
                        previousChar = ' ';
                        polymer = newPolymer;
                        // Console.WriteLine(polymer.Length);
                        if (polymer.Length < shortestPolymer.Length)
                        {
                            shortestPolymer = polymer;
                            Console.WriteLine($"{smallAlpha[a]} + {bigAlpha[a]}");
                        }
                    }

                    Console.WriteLine(shortestPolymer.Length);
                }

                Console.WriteLine($"Done + {System.Diagnostics.Stopwatch.GetTimestamp()}");
                Console.ReadLine();
            }
        }
    }
}
