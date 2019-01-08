using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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
                var timer = System.Diagnostics.Stopwatch.StartNew();
                timer.Start();
                
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

                // TODO - REFACTOR and fix!
                string shortestPolymer = polymer2;
                List<charTime> listOfCharTimes = new List<charTime>();

                string smallAlpha = "abcdefgehijklmnopqrstuvwxyz";
                string bigAlpha = "ABCDEFGEHIJKLMNOPQRSTUVWXYZ";

                Parallel.For(0, smallAlpha.Length, a =>
               {
                   string polymer = polymer2.Replace(smallAlpha[(int)a], ' ').Replace(bigAlpha[(int)a], ' ');
                   polymer = Regex.Replace(polymer, $" ", "");

                   string newPolymer = "";
                   char previousChar = ' ';

                   bool matchFound = true;
                   //Task<charTime> task = Task<charTime>.Factory.StartNew(() =>
                   //{
                       Console.WriteLine($"Task for {smallAlpha[(int)a]} started");
                       char usedChar = smallAlpha[(int)a];
                       while (matchFound)
                       {
                           matchFound = false;
                           for (int i = 0; i < polymer.Length; i++)
                           {
                               if (polymer[i] == previousChar)
                               {
                                   previousChar = polymer[i];
                                   continue;
                               }
                               else if (polymer[i].ToString().ToUpper() == previousChar.ToString() || polymer[i].ToString().ToLower() == previousChar.ToString())
                               {
                                   previousChar = polymer[i];
                                   newPolymer = polymer.Remove((i - 1), 2);
                                   matchFound = true;
                                   break;
                               }

                               previousChar = polymer[i];
                           }
                           previousChar = ' ';
                           polymer = newPolymer;
                            // Moved from here
                       }

                   //New code *******************
                   charTime charTime = new charTime() { Time = polymer.Length, ProcessedChar = smallAlpha[(int)a] };

                        // Console.WriteLine(polymer.Length);
                        //if (polymer.Length < shortestPolymer.Length)
                        //{
                        //    shortestPolymer = polymer;
                        //    Console.WriteLine($"{smallAlpha[a]} + {bigAlpha[a]}");
                        //}

                        //Console.WriteLine(shortestPolymer.Length);
                        //Console.WriteLine(timer.Elapsed);

                        //return new charTime { Time = polymer.Length, ProcessedChar = usedChar };
                   //});
                   Console.WriteLine(timer.Elapsed);
                   listOfCharTimes.Add(charTime);
                   //listOfCharTimes.Add(task.Result);

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
                    //            previousChar = polymer[i];
                    //            continue;
                    //        }
                    //        else if (polymer[i].ToString().ToUpper() == previousChar.ToString() || polymer[i].ToString().ToLower() == previousChar.ToString())
                    //        {
                    //            previousChar = polymer[i];
                    //            newPolymer = polymer.Remove((i - 1), 2);
                    //            matchFound = true;
                    //            break;
                    //        }

                    //        previousChar = polymer[i];

                    //if (polymer[i] == previousChar)
                    //{
                    //    // Do Nothing
                    //    previousChar = polymer[i];
                    //}
                    //else if (polymer[i].ToString().ToUpper() == previousChar.ToString() || polymer[i].ToString().ToLower() == previousChar.ToString())
                    //{
                    //    previousChar = polymer[i];
                    //    newPolymer = polymer.Remove((i - 1), 2);
                    //    matchFound = true;
                    //    break;
                    //}
                    //else
                    //{
                    //    previousChar = polymer[i];
                    //}
                    //        }
                    //        previousChar = ' ';
                    //        polymer = newPolymer;
                    //        // Moved from here
                    //    }

                    //    // Console.WriteLine(polymer.Length);
                    //    if (polymer.Length < shortestPolymer.Length)
                    //    {
                    //        shortestPolymer = polymer;
                    //        Console.WriteLine($"{smallAlpha[a]} + {bigAlpha[a]}");
                    //    }

                    //    Console.WriteLine(shortestPolymer.Length);
                    //    Console.WriteLine(timer.Elapsed);
                    //}
                });
                Console.WriteLine($"Done + {timer.Elapsed}");
                foreach (charTime CT in listOfCharTimes)
                {
                    Console.WriteLine($"Number of chars: {CT.Time} \n Character: {CT.ProcessedChar}");
                }
                Console.ReadLine();
            }
        }

        class charTime
        {
            public int Time { get; set; }
            public char ProcessedChar { get; set; } 
        }
    }
}

