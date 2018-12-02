using System;
using System.Collections.Generic;
using System.IO;

namespace DayOne
{
    class Program
    {
        static void Main(string[] args)
        {
            /* Dejv 2018 - Advent of code Day 1 - Finding the right frequency */

            // Part 1 - solution works
            
            // Read file 
            using (var myReader = File.OpenText(@"D:\Skrivbord\AdventOfCode2018\Day 1\Day1Input.txt"))
            {
                // Get rid of new lines
                var inputs = myReader.ReadToEnd().Split("\n");
                
                // Variable to hold the answer
                var result = 0;

                // Loops through the inputs and adds the togheter get the right frequency
                for(int i = 0; i < inputs.Length; i++)
                {
                    result += Convert.ToInt32(inputs[i]);
                }

                Console.WriteLine(result.ToString());
            }

            // Part 2 - solution works

            // Read file
            using (var myReader = File.OpenText(@"D:\Skrivbord\AdventOfCode2018\Day 1\Day1Input.txt"))
            {
                // Get rid of new lines
                var inputs = myReader.ReadToEnd().Split("\n");

                // Variable to hold the answer
                var result = 0;

                // Variable to hold the frequencies that we are going to look in for the first duplicated one
                List<int> frequencies = new List<int>();

                bool frequenciesFound = false;

                // Loops through the inputs as many times as needed and adds them togheter until the frequency occurs for a second time.
                while (!frequenciesFound)
                {
                    // Loops through the inputs and adds them togheter get the right frequency and checks if has occured before
                    for (int i = 0; i < inputs.Length; i++)
                    {
                        // Set the current frequency
                        result += Convert.ToInt32(inputs[i]);

                        // Check if the frequency has been set before
                        if (frequencies.Contains(result))
                        {
                            // Frequency duplicate => exit
                            frequenciesFound = true;
                            break;
                        }
                        else
                        {
                            // Add frequency to the list and then check the next one
                            frequencies.Add(result);
                        }
                        
                    }
                }
                Console.WriteLine(result.ToString());
            }
        }
    }
}
