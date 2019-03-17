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
            // Find the largest area that isn't infinite
            // Start at coordinates 0,0 and it can be negative
            // Every coordinate will have a list of positions that are the closest
            // string[] coordinates = File.ReadAllLines(@"D:\Skrivbord\AdventOfCode2018\Day 6\Day6Input.txt");
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

            // TODO - change to 500 för i and j i the for loop AND remove the printing for the area. 

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

            for (int i = 0; i < sizeOfArea; i++)
            {
                for (int j = 0; j < sizeOfArea; j++)
                {
                    var location = WhichIsClosest(i, j, locations);

                    if (i == 0)
                    {
                        ChangeInfinteAreaToTrue(location, locations);
                    }else if(i > 0 && j == 0)
                    {
                        ChangeInfinteAreaToTrue(location, locations);
                    }
                    else if(i == (sizeOfArea - 1))
                    {
                        ChangeInfinteAreaToTrue(location, locations);
                    }
                    else if(j == (sizeOfArea - 1))
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

            Console.WriteLine();
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
