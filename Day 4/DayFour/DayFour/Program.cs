using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace DayFour
{
    class Program
    {
        static void Main(string[] args)
        {
            // Get all the records
            string[] records = File.ReadAllLines(@"C:\Users\david\Desktop\AdventOfCode2018\Day 4\Day4Input.txt");

            // Sorts the records according to the date in the beginning
            Array.Sort(records);

            // Prints all records unformatted, but sorted
            // PrintAllRecords(records);

            // Prints alla shifts and the awake and asleep time for each guard
            // PrintAllSleepWakeUp(records);

            // Part 1 - correct answer
            SolutionOne(CreateGuardList(records));

            // Part 2 - correct answer
            SolutionTwo(CreateGuardList(records));
        }

        // Solution to part 1 - find the Guard that sleeps the most and then multiply the Guard ID with the minute which he slept the most.
        private static void SolutionOne(List<Guard> newGuardList)
        {
            // Vairables to keep track of sleepisest guard.
            string bigSleeper = "";
            int mostSleep = 0;
            foreach (Guard newGuard in newGuardList)
            {
                if (newGuard.totalTime > mostSleep)
                {
                    bigSleeper = newGuard.GuardID;
                    mostSleep = newGuard.totalTime;
                }
            }

            Console.WriteLine($"Guard {bigSleeper} sleeps {mostSleep}");

            // Get the sleepiest guard
            var guard = newGuardList.Find(x => x.GuardID == bigSleeper);

            // TODO - The part below is duplicated in part 2 - refactor to method?
            // int array for figuring out the sleepiest minute
            int[] times = new int[60];

            // Variables for iterating through all the sleep cycles and keeping track of how many times the guard has been sleeping that minute.
            int sleep = 0;
            int wakeup = 0;
            foreach (string action in guard.Actions)
            {
                if (action.Contains("falls asleep"))
                {
                    sleep = Convert.ToInt32(Regex.Replace(action, @"[^\d]", "").Substring(8, 4));
                }
                if (action.Contains("wakes up"))
                {
                    wakeup = Convert.ToInt32(Regex.Replace(action, @"[^\d]", "").Substring(8, 4));
                    // Counting and adding upp the minutes which the guard are asleep
                    for (int i = sleep; i < wakeup; i++)
                    {
                        times[i]++;
                    }
                    sleep = 0;
                    wakeup = 0;
                }
            }

            // Variables to find the sleepiest minute and how many times the guard slept that minute
            int sleepiestMinute = 0;
            int minute = 0;
            for (int i = 0; i < times.Length; i++)
            {
                if (times[i] > sleepiestMinute)
                {
                    sleepiestMinute = times[i];
                    minute = i;
                }
            }

            Match match = Regex.Match(guard.GuardID, @"#(\d+)");
            int answer = minute * Convert.ToInt32(match.Groups[1].Value);

            Console.WriteLine($"{guard.GuardID} * {minute} = {answer}");
        }

        // Solution to part 2 -- find the guard which slept the most times on the same minute. Then multiply the Guard ID with the minute.
        private static void SolutionTwo(List<Guard> newGuardList)
        {
            // Variables for keeping track of the Guard, minute and how many consecutive minutes.
            string theOneGuard = "";
            int theMinute = 0;
            int howManyTimes = 0;

            // Go through the guard list and find the rigth guard.
            foreach (Guard newGuard in newGuardList)
            {
                // array for counting the how many times the guard is sleeping on each minute.
                int[] times = new int[60];

                int sleep = 0;
                int wakeup = 0;
                foreach (string action in newGuard.Actions)
                {
                    if (action.Contains("falls asleep"))
                    {
                        sleep = Convert.ToInt32(Regex.Replace(action, @"[^\d]", "").Substring(8, 4));
                    }
                    if (action.Contains("wakes up"))
                    {
                        wakeup = Convert.ToInt32(Regex.Replace(action, @"[^\d]", "").Substring(8, 4));
                        for (int i = sleep; i < wakeup; i++)
                        {
                            times[i]++;
                        }
                        sleep = 0;
                        wakeup = 0;
                    }
                }

                // check for the sleepiest minute
                int sleepiestMinute = 0;
                int minute = 0;
                for (int i = 0; i < times.Length; i++)
                {
                    if (times[i] > sleepiestMinute)
                    {
                        sleepiestMinute = times[i];
                        minute = i;
                    }
                }

                // save info if the guard has the sleepiest minute
                if (sleepiestMinute > howManyTimes)
                {
                    howManyTimes = sleepiestMinute;
                    theMinute = minute;
                    theOneGuard = newGuard.GuardID;
                }
            }

            // print the answer
            Console.WriteLine($"{theOneGuard} minute: {theMinute} times: {howManyTimes}");
            Match match = Regex.Match(theOneGuard, @"#(\d+)");
            int answer = theMinute * Convert.ToInt32(match.Groups[1].Value);
            Console.WriteLine($"{theOneGuard} * {theMinute} = {answer}");
        }

        // Extracts info from the sorted array and creats a List of Guard-objects. 
        private static List<Guard> CreateGuardList(string[] records)
        {
            List<Guard> ListOfGuards = new List<Guard>();
            
            // Going through the array of records and creating a list of guards(unique).
            for (int i = 0; i < records.Length; i++)
            {
                if (records[i].Contains("Guard"))
                {
                    Guard newGuard = new Guard();
                    newGuard.GuardID = Regex.Match(records[i], @"#(\d+)").ToString();
                    if (!ListOfGuards.Exists(x => x.GuardID == newGuard.GuardID))
                    {
                        ListOfGuards.Add(newGuard);
                    }
                }
            }

            // Going through all the records again to add all the actions(fall asleep and wake up) to the guard object
            string guardID = "";
            for (int i = 0; i < records.Length; i++)
            {
                if (records[i].Contains("Guard"))
                {
                    guardID = Regex.Match(records[i], @"#(\d+)").ToString();
                    continue;
                }
                var guardToChange = ListOfGuards.First(x => x.GuardID == guardID);
                guardToChange.Actions.Add(records[i]);
            }

            // Going through the list of Guards to figure out how much sleep time each guard has.
            foreach (Guard guard in ListOfGuards)
            {
                // Variable to hold total sleep time until adding it to the guard.
                int totalSleepTime = 0;
                // Assuming that actions fall asleep and wake up comes in pairs. Each pair gives a sleep time.
                int sleep = 0;
                int wakeup = 0;
                foreach (string action in guard.Actions)
                {
                    if(action.Contains("falls asleep"))
                    {
                        sleep = Convert.ToInt32(Regex.Replace(action, @"[^\d]", "").Substring(8, 4));
                    }
                    if(action.Contains("wakes up"))
                    {
                        wakeup = Convert.ToInt32(Regex.Replace(action, @"[^\d]", "").Substring(8, 4));
                        totalSleepTime += wakeup - sleep;
                        sleep = 0;
                        wakeup = 0;
                    }
                }
                guard.totalTime = totalSleepTime;
            }

            // Returns a complete List of Guard objects.
            return ListOfGuards;
        }

        // Just prints the unformatted array of strings
        private static void PrintAllRecords(string[] records)
        {
            foreach (var record in records)
            {
                Console.WriteLine(record);
            }
        }

        // Prints the unformatted array of strings according to the example. Just for visualisation.
        private static void PrintAllSleepWakeUp(string[] records)
        {
            // TODO - the date is always from the Guard starts shift and that can be wrong.
            PrintHeader();

            // Bool for keeping track if a guard didn't sleep at all
            bool noSleep = false;
            // A list for keeping track of falling asleep and and waking up. I assume they allways comes in pair. Odd index = falls asleep and Even index = wakes up
            List<int> sleepWakesUp = new List<int>();
            // Goes through all records and do some work 
            foreach (var record in records)
            {
                // When a new guard shows up in the array and and if the previous guard had any actions(sleep or wakeup) it prints them
                if (record.Contains("Guard") && sleepWakesUp.Count != 0)
                {
                    PrintSleepWakeUpTimes(sleepWakesUp);
                }

                // Checks the record for the type and prepare it for the next step. 
                if (record.Contains("Guard"))
                {
                    // If a new guard record shows upp and the one before didn't have any actions. It prints the whole shift as wake up.
                    if (noSleep)
                    {
                        Console.Write("\t");
                        for (int i = 0; i < 60; i++)
                        {
                            Console.Write(".");
                        }
                    }
                    Console.WriteLine();
                    string date = Regex.Replace(record, @"[^\d]", "").Substring(4, 4);
                    Console.Write(date);
                    Match match = Regex.Match(record, @"#(\d+)");
                    string guardID = match.Groups[1].Value;
                    Console.Write("\t" + guardID);
                    noSleep = true;
                }
                else if (record.Contains("falls asleep"))
                {
                    sleepWakesUp.Add(Convert.ToInt32(Regex.Replace(record, @"[^\d]", "").Substring(8, 4)));
                    noSleep = false;
                }
                else if (record.Contains("wakes up"))
                {
                    sleepWakesUp.Add(Convert.ToInt32(Regex.Replace(record, @"[^\d]", "").Substring(8, 4)));
                    noSleep = false;
                }
            }

            // One more call to this method to print the last shift.
            PrintSleepWakeUpTimes(sleepWakesUp);

            Console.WriteLine();
        }

        /// <summary>
        /// Prints the startheader with labels: Date, ID and minutes and all the minutes
        /// </summary>
        private static void PrintHeader()
        {
            //00000000001111111111...
            //01234567890123456789...
            // Variable for printing the two rows of minutes.
            int number = 0;
            // Keeps track of the logic in the two loops
            int counter = 0;
            Console.WriteLine("Date" + "\t" + "ID" + "\t" + "Minute");
            Console.Write("\t\t");
            // Prints the first row
            for (int i = 0; i < 60; i++)
            {
                Console.Write(number);
                counter++;
                if (counter == 10)
                {
                    number++;
                    counter = 0;
                }
            }
            number = 0;
            Console.WriteLine();
            Console.Write("\t\t");
            // Prints the second row
            for (int i = 0; i < 60; i++)
            {
                Console.Write(number);
                number++;

                if (number == 10)
                {
                    number = 0;
                }
            }
        }

        /// <summary>
        /// Prints the sleep and awake minutes of a Guard during a shift
        /// </summary>
        /// <param name="sleepWakesUp"></param>
        private static void PrintSleepWakeUpTimes(List<int> sleepWakesUp)
        {
            int latestAction = 0;
            Console.Write("\t");
            foreach (var action in sleepWakesUp)
            {
                if (sleepWakesUp.IndexOf(action) == 0)
                {
                    latestAction = 0;
                }
                if (sleepWakesUp.IndexOf(action) % 2 == 0)
                {
                    for (int a = latestAction; a < action; a++)
                    {
                        Console.Write(".");
                    }
                }
                if (sleepWakesUp.IndexOf(action) % 2 != 0)
                {
                    for (int a = latestAction; a < action; a++)
                    {
                        Console.Write("#");
                    }
                }

                latestAction = action;
            }
            if (latestAction != 60)
            {
                for (int i = latestAction; i < 60; i++)
                {
                    Console.Write(".");
                }
            }
            sleepWakesUp.Clear();
        }
    }
}

/// <summary>
/// Holds information about a guards ID, Total sleep time and all the actions(fall asleep and wake up) as integers. Even = fall asleep and Odd = wakes up.
/// </summary>
public class Guard
{
    // ID eg. #123
    public string GuardID { get; set; }
    // All the strings that comes after Guard records 
    public List<string> Actions = new List<string>();
    // Total time asleep  - all shifts
    public int totalTime { get; set; }
}

