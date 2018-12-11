using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace DayThree
{
    class Program
    {
        // Day 3 - Part 1 and 2 - find all inches that overlaps more than once and find the claim that in not overlapping with any other claim.

        // All claims have an ID. Looks like #123 @ 3,2: 5x4
        public static int verticalSize = 1500;
        public static int horizontalSize = 1500;
        public static int inchCounter = 0;
        public static int correctId;

        static void Main(string[] args)
        {
            // Get all the claims
            string[] claims = File.ReadAllLines(@"D:\Skrivbord\AdventOfCode2018\Day 3\Day3Input.txt");

            // Create the fabric 
            string[,] fabric = new string[verticalSize, horizontalSize];

            // Method for converting the string[] to list with claim objects
            var listOfClaims = TransformClaim(claims);

            CreateFabric(fabric, listOfClaims);

            Console.WriteLine(inchCounter);
            Console.WriteLine(correctId);

            Console.ReadLine();
        }

        // Method that turnes #123 @ 3,2: 5x4 into Claim - Id: 123, fromLeft: 3, fromTop: 2, inchesWide: 5, inchesTall: 4
        static List<Claim> TransformClaim(string[] unformattedClaims)
        {
            List<Claim> claims = new List<Claim>();

            foreach (string claim in unformattedClaims)
            {
                string newClaim = Regex.Replace(claim, "[^0-9]", ",");
                newClaim = Regex.Replace(newClaim, @",+", ",");
                newClaim = newClaim.Substring(1);

                Claim claimToAdd = new Claim(newClaim);
                claims.Add(claimToAdd);
            }

            return claims;
        }

        // Method that creats the fabric with "0" and then uses each claim to put in "1" or "X" 
        static void CreateFabric(string[,] fabric, List<Claim> claims)
        {
            for(int vertical = 0; vertical < verticalSize; vertical++)
            {
                for (int horizontal = 0; horizontal < horizontalSize; horizontal++)
                {
                    fabric[vertical, horizontal] = "0";
                }
            }

            foreach (Claim claim in claims)
            {
                for (int vertical = claim.fromTop; vertical < claim.fromTop + claim.inchesTall; vertical++)
                {
                    for (int horizontal = claim.fromLeft; horizontal < claim.fromLeft + claim.inchesWide; horizontal++)
                    {
                        if (fabric[vertical, horizontal] == "0")
                        {
                            fabric[vertical, horizontal] = "1";
                        }
                        else if(fabric[vertical, horizontal] == "1")
                        {
                            fabric[vertical, horizontal] = "X";
                        }
                    }
                }
            }

            foreach (Claim claim in claims)
            {
                bool theRightId = true;

                for (int vertical = claim.fromTop; vertical < claim.fromTop + claim.inchesTall; vertical++)
                {
                    for (int horizontal = claim.fromLeft; horizontal < claim.fromLeft + claim.inchesWide; horizontal++)
                    {
                        if (fabric[vertical, horizontal] == "X")
                        {
                            theRightId = false;
                        }
                    }
                }

                if (theRightId == true)
                {
                    correctId = claim.Id;
                }
            }

            foreach (string inch in fabric)
            {
                if (inch == "X")
                {
                    inchCounter++;
                }
            }
        }
    }

    // Class for holding the claims in the solution
    class Claim
    {
        public int Id { get; set; }
        public int fromLeft { get; set; }
        public int fromTop { get; set; }
        public int inchesWide { get; set; }
        public int inchesTall { get; set; }

        public Claim(string claimCommaSeparated)
        {
            string[] claims = claimCommaSeparated.Split(",");

            Id = Convert.ToInt32(claims[0]);
            fromLeft = Convert.ToInt32(claims[1]);
            fromTop = Convert.ToInt32(claims[2]);
            inchesWide = Convert.ToInt32(claims[3]);
            inchesTall = Convert.ToInt32(claims[4]);
        }
    }
}
