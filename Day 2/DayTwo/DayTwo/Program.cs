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

            using (var myReader = File.OpenText(@"D:\Skrivbord\AdventOfCode2018\Day 2\Day2Input.txt"))
            {
                // Get lines with IDs
                string[] IDs = myReader.ReadToEnd().Split("\n");

                // Variables to hold the checksum and letters to match with
                int twos = 0;
                int threes = 0;
                string alphabet = "abcdefgehijklmnopqrstuvwxyz";

                // Go through all the ids one by one
                foreach (var ID in IDs)
                {
                    // Variables for checking if it should be added to the checksum 
                    int notYetTwos = 0;
                    int notYetThrees = 0;

                    // Uses the alphabet to check each letter against the values in the selected id
                    for (int i = 0; i < alphabet.Length; i++)
                    {
                        // Selects a letter from the alphabet to check and initialize a counter for the occurences
                        char letterToCheck = alphabet[i];
                        int counter = 0;

                        // Check how many times letter from alphabet matches in the selected id 
                        for (int j = 0; j < ID.Length; j++)
                        {
                            if (letterToCheck == ID[j])
                            {
                                counter += 1;
                            }
                        }

                        // When the check is done it checks if the letter from the alphabet matched exactly 2 or 3 times. Only then it should increment.
                        if(counter == 2)
                        {
                            notYetTwos += 1;
                        }
                        else if(counter == 3)
                        {
                            notYetThrees += 1;
                        }
                        else
                        {
                            // nada
                        }
                    }

                    // If there were more than 1 occurrence of twos or threes it should always count as one.
                    if (notYetTwos >= 1)
                    {
                        twos += 1;
                    }
                    if (notYetThrees >= 1)
                    {
                        threes += 1;
                    }
                }

                Console.WriteLine($"{twos} * {threes} = {twos * threes}");
            }

            // Part 2

            using (var myReader = File.OpenText(@"D:\Skrivbord\AdventOfCode2018\Day 2\Day2Input.txt"))
            {
                // Get lines with IDs
                string[] IDs = myReader.ReadToEnd().Split("\r\n");

                // Variable to keep track of most matches and the letters matched
                int mostLettersMatches = 0;
                string mostLettersMatchedID = "";

                // First loop to check with
                foreach (var originalID in IDs)
                {
                    // Second loop to get the id to check against
                    foreach (var checkID in IDs)
                    {
                        // To skip the one that is exactly the same
                        if (originalID == checkID)
                        {
                            break;
                        }

                        // Variables to keep track of number of matches and the a string with the matches
                        int numberOfLetterMatches = 0;
                        string letterMatchID = "";

                        // Check letter by letter and count the matches
                        for (int d = 0; d < originalID.Length; d++)
                        {
                            if (originalID[d] == checkID[d])
                            {
                                numberOfLetterMatches++;
                                letterMatchID += originalID[d];
                            }
                        }

                        // If it was more matches save how many and the letters that were matched
                        if (mostLettersMatches < numberOfLetterMatches)
                        {
                            mostLettersMatches = numberOfLetterMatches;
                            mostLettersMatchedID = letterMatchID;
                        }
                    }
                }

                Console.WriteLine(mostLettersMatches.ToString() + "\n" + mostLettersMatchedID);
            }
        }
    }
}
