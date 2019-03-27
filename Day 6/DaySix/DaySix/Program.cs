using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;

namespace DaySix
{
    class Program
    {
        static void Main(string[] args)
        {
            // TODO - Comment and fix everything -> REFACTOR

            // Find the largest area that isn't infinite
            // Start at coordinates 0,0 and it can be negative
            // Every coordinate will have a list of positions that are the closest
            //string[] coordinates = File.ReadAllLines(@"D:\Skrivbord\AdventOfCode2018\Day 6\Test.txt");
            string[] coordinates = File.ReadAllLines(@"D:\Skrivbord\AdventOfCode2018\Day 6\Day6Input.txt");

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

            int sizeOfArea = 500;

            //00 01 02 03 04 05 06 07 08 09
            //10                         19
            //20                         29
            //30                         39
            //40                         49
            //50                         59
            //60                         69
            //70                         79
            //80                         89
            //90 91 92 93 94 95 96 97 98 99

            for (int y = 0; y < sizeOfArea; y++)
            {
                for (int x = 0; x < sizeOfArea; x++)
                {
                    var location = WhichIsClosest(x, y, locations);

                    if (y == 0)
                    {
                        ChangeInfinteAreaToTrue(location, locations);
                    }
                    else if(y > 0 && x == 0)
                    {
                        ChangeInfinteAreaToTrue(location, locations);
                    }
                    else if(y == (sizeOfArea - 1))
                    {
                        ChangeInfinteAreaToTrue(location, locations);
                    }
                    else if(x == (sizeOfArea - 1))
                    {
                        ChangeInfinteAreaToTrue(location, locations);
                    }
                }
            }

            locations = locations.OrderByDescending(x => x.Area).ToList();

            foreach (var location in locations)
            {
                if (location.infinteArea == false)
                {
                    Console.WriteLine($"NameNumber: {location.NameNumber} Area: {location.Area}");
                }
            }

            /* Part 2 */

            int totalArea = 0;
            int lessThan = 10000;

            for (int y = 0; y < sizeOfArea; y++)
            {
                for (int x = 0; x < sizeOfArea; x++)
                {
                    totalArea = totalArea + lessThanInputDistance(x, y, lessThan, locations);
                }
            }

            Console.WriteLine($"Coordinates wiht less distance than {lessThan} make up an area of {totalArea}");
            

        }

        public static void ChangeInfinteAreaToTrue(Location loc, List<Location> locations)
        {
            var locationToChange = locations.Where(x => x.NameNumber == loc.NameNumber).First();
            locationToChange.infinteArea = true;
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
                thisValue = (Math.Abs(x - location.X)) + Math.Abs(y - location.Y);

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
            }

            // TODO - change this -> no printing -> just update the Location objects.
            if (equallyClose)
            {
                //Console.Write(".");
            }
            else if(smallestValue == 0)
            {
                //char[] letters = { 'A', 'B', 'C', 'D', 'E', 'F' };
                //Console.Write(letters[Convert.ToInt32(closestLocation.NameNumber) - 1]);
                addOneToArea(closestLocation, locations);
            }
            else
            {
                //char[] letters = { 'a', 'b', 'c', 'd', 'e', 'f' };
                //Console.Write(letters[Convert.ToInt32(closestLocation.NameNumber) - 1]);
                addOneToArea(closestLocation, locations);
            }

            return closestLocation;
        }

        public static void addOneToArea(Location loc, List<Location> locations)
        {
            var location = locations.Where(x => x.NameNumber == loc.NameNumber).First();
            location.Area++;
        }

        public static int lessThanInputDistance(int x, int y, int lessThan, List<Location> locations)
        {
            int totalDistance = 0;
            //Less than 32
            //Distance to coordinate A: abs(4-1) + abs(3-1) =  5
            //Distance to coordinate B: abs(4 - 1) + abs(3 - 6) = 6
            //Distance to coordinate C: abs(4 - 8) + abs(3 - 3) = 4
            //Distance to coordinate D: abs(4 - 3) + abs(3 - 4) = 2
            //Distance to coordinate E: abs(4 - 5) + abs(3 - 5) = 3
            //Distance to coordinate F: abs(4 - 8) + abs(3 - 9) = 10
            //Total distance: 5 + 6 + 4 + 2 + 3 + 10 = 30

            // int x = 4, int y = 3
            // lessThan = 32
            // List with all locations

            int thisValue = 0;

            foreach (Location location in locations)
            {
                thisValue = thisValue + (Math.Abs(x - location.X) + (Math.Abs(y - location.Y)));
            }

            if (thisValue < lessThan)
            {
                totalDistance = 1;
            }
            else
            {
                totalDistance = 0;
            }

            return totalDistance;
        }
    }

    public class Location
    {
        public string NameNumber { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public bool infinteArea { get; set; } = false;
        public int Area { get; set; }
    }
}
