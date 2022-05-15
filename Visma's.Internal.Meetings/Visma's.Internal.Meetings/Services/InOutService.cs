using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Visma_s.Internal.Meetings.Models;

namespace Visma_s.Internal.Meetings.Services
{
    public class InOutService
    {
        //Writes all info to JSON
        public static void WriteToJSON(string fileName, List<Meeting> _meetings)
        {
            var jsonData = JsonConvert.SerializeObject(_meetings);
            File.WriteAllText(fileName, jsonData);
        }
        //Reads all info to JSON
        public static void ReadFromJSON(string fileName, out List<Meeting> _meetings)
        {
            var Data = File.ReadAllText(fileName);
            _meetings = JsonConvert.DeserializeObject<List<Meeting>>(Data);
        }
        //Prints meetings to console in a table
        public static void PrintToConsole(List<Meeting> meetings)
        {
            Console.WriteLine(new string('-', 123));
            Console.WriteLine("|    Meeting    | Responsible person |     Description    |   Category   |   Type   |    Start Date    |     End Date     |");
            Console.WriteLine(new string('-', 123));
            meetings.ForEach(meeting => Console.WriteLine($"| {meeting.Name ,-13} | {meeting.ResponsiblePerson,-18}" + 
                                                $" | {meeting.Description, -18} | {meeting.Category, 12} | {meeting.Type, 8}" +
                                                $" | {meeting.StartDate, 16:yyyy-MM-dd HH:mm} | {meeting.EndDate, 16:yyyy-MM-dd HH:mm} |"));
            Console.WriteLine(new string('-', 123));
        }
        //Prints members to a console of one meeting
        public static void PritnToConsoleMembers(List<string> members)
        {
            Console.WriteLine("All registered members of the meeting:");
            members.ForEach(member => Console.WriteLine($"{member}"));
        }
        //Prints to console all commands
        public static void PritntCommands()
        {
            Console.WriteLine("All commands for the console:" +
                                "\nAdd - Adds a meeting" +
                                "\nDelete - Deletes a meeting" +
                                "\nShow all - Shows all existing meetings" +
                                "\nAdd a person - Adds a person to a meeting" +
                                "\nRemove a person - Removes a person from a meeting" +
                                "\nShow members - Shows all members of the meeting" +
                                "\nFilter - Filters meetings" +
                                "\nClear console - Clears console" +
                                "\nHelp - Shows all the commands" +
                                "\nExit - Saves and exits console");
        }
    }
}
