using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace DayTwo
{

    // REFACTOR!!!!!
    class Program
    {
        static void Main(string[] args)
        {
            /* Dejv 2018 - Advent of code Day 2 - Inventory Management System */

            // Part 1 

            // Read file
            using (var myReader = File.OpenText(@"D:\Skrivbord\AdventOfCode2018\Day 2\Day2Input.txt"))
            {
                // Get lines with letters
                string[] ids = myReader.ReadToEnd().Split("\n");

                
                int twos = 0;
                int threes = 0;

                string alpha = "abcdefgehijklmnopqrstuvwxyz";
                foreach (var id in ids)
                {
                    int notyettwos = 0;
                    int notyetthrees = 0;
                    for (int i = 0; i < alpha.Length; i++)
                    {
                        char letterToCheck = alpha[i];
                        int counter = 0;
                        for (int j = 0; j < id.Length; j++)
                        {
                            if (letterToCheck == id[j])
                            {
                                counter += 1;
                            }
                        }
                        if(counter == 2)
                        {
                            notyettwos += 1;
                        }
                        else if(counter == 3)
                        {
                            notyetthrees += 1;
                        }
                        else
                        {
                            // nada
                        }
                    }
                    if (notyettwos >= 1)
                    {
                        twos += 1;
                    }
                    if (notyetthrees >= 1)
                    {
                        threes += 1;
                    }
                }

                Console.WriteLine($"{twos} * {threes} = {twos * threes}");
            }

            // Part 2
            using (var myReader = File.OpenText(@"D:\Skrivbord\AdventOfCode2018\Day 2\Day2Input.txt"))
            {
                List<string> ids = new List<string>();

                
                string[] test = myReader.ReadToEnd().Split("\r\n");

                for(int j = 0;j < test.Length; j++)
                {
                    
                    for (int i = 0; i < test[j].Length; i++)
                    {
                        string first = test[j];
                        
                        foreach (var item in test)
                        {
                            if (first == item)
                            {
                                break;
                            }
                            int count = 0;
                            string matches = "";
                            for (int d = 0; d < first.Length; d++)
                            {
                                if (first[d] == item[d])
                                {
                                    count++;
                                    matches += first[d];
                                    if (count == 25)
                                    {
                                        Console.WriteLine(first + "\n" + item);
                                        Console.WriteLine(matches);
                                    }
                                }
                            }
                        }
                    }
                }

                // pbykrmjmizwhxlqnwasfgtycdv
                //Console.WriteLine(correctAnswer);
                //Console.WriteLine(firstValue);
            }
        }
    }
}
