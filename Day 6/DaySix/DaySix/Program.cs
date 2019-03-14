using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace DaySix
{
    class Program
    {
        static void Main(string[] args)
        {
            // Find the largest area that isn't infinite
            // Start at coordinates 0,0 and it can be negative
            // Every coordinate will have a list of positions that are the closest
            // string[] coordinates = File.ReadAllLines(@"D:\Skrivbord\AdventOfCode2018\Day 6\Day6Input.txt");
            string[] coordinates = File.ReadAllLines(@"D:\Skrivbord\AdventOfCode2018\Day 6\Test.txt");

            List<Location> locations = new List<Location>();
            int number = 1;

            foreach (var cor in coordinates)
            {
                Location loc = new Location();

                string cor2 = cor.Replace(" ", String.Empty);
                string[] newCor = cor2.Split(",");

                loc.NameNumber = number.ToString();
                loc.X = Convert.ToInt32(newCor[0]);
                loc.Y = Convert.ToInt32(newCor[1]);

                locations.Add(loc);
                number++;
            }

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    WhichIsClosest(i, j, locations);
                }
                Console.WriteLine();
            }
            WhichIsClosest(0, 0, locations);
        }

        public static Location WhichIsClosest(int x, int y, List<Location> locations)
        {
            Location closestLocation = new Location();

            int thisValue = 0;
            int smallestValue = 0;
            bool firstValue = true;
            bool equallyClose = false;

            foreach (var location in locations)
            {
                thisValue = (Math.Abs(x - location.Y)) + Math.Abs(y - location.X);

                if (firstValue)
                {
                    smallestValue = thisValue;
                    firstValue = false;
                    closestLocation = location;
                }
                else
                {
                    if (thisValue < smallestValue)
                    {
                        smallestValue = thisValue;
                        equallyClose = false;
                        closestLocation = location;
                    }
                    else if(thisValue == smallestValue)
                    { 
                        equallyClose = true;
                    }
                    else
                    {
                        // Do nothing
                    }
                }

                // TODO - Not numbers -> Needs charcters 
            }

            if (equallyClose)
            {
                Console.Write(".");
            }
            else if(smallestValue == 0)
            {
                char[] letters = { 'A', 'B', 'C', 'D', 'E', 'F' };
                Console.Write(letters[Convert.ToInt32(closestLocation.NameNumber) - 1]);
            }
            else
            {
                char[] letters = { 'a', 'b', 'c', 'd', 'e', 'f' };
                Console.Write(letters[Convert.ToInt32(closestLocation.NameNumber) - 1]);
            }

            return closestLocation;
        }
    }

    public class Location
    {
        public string NameNumber { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Area { get; set; }
    }
}
