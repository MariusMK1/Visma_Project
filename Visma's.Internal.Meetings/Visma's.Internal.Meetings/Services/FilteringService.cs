using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Visma_s.Internal.Meetings.Models;

namespace Visma_s.Internal.Meetings.Services
{
    public class FilteringService
    {
        //Method for connecting filtering and printing
        public static void Filter(MeetingsService _meetingsService)
        {
            Console.WriteLine("Select how would you like to filter:" +
                                        "\n By description - 1" +
                                        "\n By responsible person - 2" +
                                        "\n By category - 3" +
                                        "\n By type - 4" +
                                        "\n By date from - 5" +
                                        "\n By between dates - 6" +
                                        "\n By number of attendees - 7");
            string choice = Console.ReadLine();
            List<Meeting> meetings = new List<Meeting>();
            switch (choice)
            {
                case "1":
                    Console.WriteLine("Enter description:");
                    string description = Console.ReadLine();
                    meetings = _meetingsService.FilterByDescription(description);
                    InOutService.PrintToConsole(meetings);
                    break;

                case "2":
                    Console.WriteLine("Enter responsible person:");
                    string person = Console.ReadLine();
                    meetings = _meetingsService.FilterByResponsibleP(person);
                    InOutService.PrintToConsole(meetings);
                    break;

                case "3":
                    Console.WriteLine("Select the category of the meeting:" +
                                        "\n CodeMonkey - 1" +
                                        "\n Hub - 2" +
                                        "\n Short - 3" +
                                        "\n Team building - 4");
                    Category category = Category.CodeMonkey;
                    while (true)
                    {
                        string choice2 = Console.ReadLine();
                        bool loopBrak = true;
                        switch (choice2)
                        {
                            case "1":
                                category = Category.CodeMonkey;
                                break;
                            case "2":
                                category = Category.Hub;
                                break;
                            case "3":
                                category = Category.Short;
                                break;
                            case "4":
                                category = Category.TeamBuilding;
                                break;
                            default:
                                Console.WriteLine("Please enter a valid choice.");
                                loopBrak = false;
                                break;
                        }
                        if (loopBrak != false)
                        {
                            break;
                        }
                    }
                    meetings = _meetingsService.FilterByCategory(category);
                    InOutService.PrintToConsole(meetings);
                    break;

                case "4":
                    Console.WriteLine("Select the type of the meeting:" +
                                            "\n Live - 1" +
                                            "\n InPerson - 2");
                    MeetingType type = MeetingType.Live;
                    while (true)
                    {
                        string choice3 = Console.ReadLine();
                        bool loopBrak = true;
                        switch (choice3)
                        {
                            case "1":
                                type = MeetingType.Live;
                                break;
                            case "2":
                                type = MeetingType.InPerson;
                                break;
                            default:
                                Console.WriteLine("Please enter a valid choice.");
                                loopBrak = false;
                                break;
                        }
                        if (loopBrak != false)
                        {
                            break;
                        }
                    }
                    meetings = _meetingsService.FilterByType(type);
                    InOutService.PrintToConsole(meetings);
                    break;

                case "5":
                    Console.WriteLine("Enter the date (yyyy-mm-dd hh:mm) you want to filter from:");
                    DateTime date = DateTime.Parse(Console.ReadLine());
                    meetings = _meetingsService.FilterFromStartDate(date);
                    InOutService.PrintToConsole(meetings);
                    break;

                case "6":
                    Console.WriteLine("Enter the start date (yyyy-mm-dd hh:mm):");
                    DateTime startDate = DateTime.Parse(Console.ReadLine());
                    Console.WriteLine("Enter the end date (yyyy-mm-dd hh:mm):");
                    DateTime endDate = DateTime.Parse(Console.ReadLine());
                    meetings = _meetingsService.FilterbyDates(startDate, endDate);
                    InOutService.PrintToConsole(meetings);
                    break;

                case "7":
                    Console.WriteLine("Enter the minimum number of attednees:");
                    int attendees = int.Parse(Console.ReadLine());
                    meetings = _meetingsService.FilterByNumberOfAttendees(attendees);
                    InOutService.PrintToConsole(meetings);
                    break;

                default:
                    Console.WriteLine("This filtering option does not exist");
                    break;
            }
        }
    }
}
