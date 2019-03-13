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
            int number = 0;

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

            foreach (var location in locations)
            {
                thisValue = (Math.Abs(x - location.X)) + Math.Abs(y - location.Y);

                if (thisValue < smallestValue || smallestValue == 0)
                {
                    smallestValue = thisValue;
                    closestLocation = location;
                }
                // TODO - Not numbers -> Needs charcters 
            }

            Console.Write(closestLocation.NameNumber);

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
