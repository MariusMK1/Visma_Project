using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Visma_s.Internal.Meetings.Models;

namespace Visma_s.Internal.Meetings.Services
{
    public class ApplicationService
    {
        private MeetingsService _meetingsService;
        public ApplicationService(string userName)
        {
            _meetingsService = new MeetingsService(userName);
        }
        // this method processes my entire program 
        public void Process (string command)
        {
            try
            {
                if (command.Equals("ADD"))
                {
                    Console.WriteLine("Enter meeting name:");
                    string name = Console.ReadLine();

                    Console.WriteLine("Enter responsible person name:");
                    string responsiblePerson = Console.ReadLine();

                    Console.WriteLine("Enter description of the meeting:");
                    string description = Console.ReadLine();

                    Console.WriteLine("Select the category of the meeting:" +
                                        "\n CodeMonkey - 1" +
                                        "\n Hub - 2" +
                                        "\n Short - 3" +
                                        "\n Team building - 4");
                    Category category = Category.CodeMonkey;
                    while (true)
                    {
                        string choice = Console.ReadLine();
                        bool loopBrak = true;
                        switch (choice)
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

                    Console.WriteLine("Select the type of the meeting:" +
                                            "\n Live - 1" +
                                            "\n InPerson - 2");
                    MeetingType type = MeetingType.Live;
                    while (true)
                    {
                        string choice = Console.ReadLine();
                        bool loopBrak = true;
                        switch (choice)
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

                    Console.WriteLine("Enter the start of the meeting (yyyy-mm-dd hh:mm):");
                    DateTime startDate = DateTime.Parse(Console.ReadLine());

                    Console.WriteLine("Enter the end of the meeting (yyyy-mm-dd hh:mm):");
                    DateTime endDate;
                    while (true)
                    {
                        bool loopBrak = true;
                        endDate = DateTime.Parse(Console.ReadLine());
                        if (startDate > endDate)
                        {
                            Console.WriteLine("End date can not be before start date.");
                            loopBrak = false;
                        }
                        if (loopBrak != false)
                        {
                            break;
                        }
                    }
                    _meetingsService.Add(name, responsiblePerson, description, category, type, startDate, endDate);
                    _meetingsService.AddPerson(name, responsiblePerson);
                }

                else if (command.Equals("DELETE"))
                {
                    Console.WriteLine("Enter the name of the meeting to delete:");
                    string name = Console.ReadLine();
                    _meetingsService.CheckIFMeetingExists(name);
                    _meetingsService.Delete(name);
                }

                else if (command.Equals("SHOW ALL"))
                {
                    List<Meeting> meetings = _meetingsService.GetAll();
                    InOutService.PrintToConsole(meetings);
                }

                else if (command.Equals("ADD A PERSON"))
                {
                    Console.WriteLine("Enter the meeting name you want to add a person to:");
                    string name = Console.ReadLine();
                    _meetingsService.CheckIFMeetingExists(name);

                    Console.WriteLine("Enter the persons name and surname:");
                    string member = Console.ReadLine();
                    _meetingsService.AddPerson(name, member);
                }

                else if (command.Equals("REMOVE A PERSON"))
                {
                    Console.WriteLine("Enter the meeting name you want to remove a person from:");
                    string name = Console.ReadLine();
                    _meetingsService.CheckIFMeetingExists(name);

                    Console.WriteLine("Enter the persons name and surname:");
                    string member = Console.ReadLine();
                    _meetingsService.RemovePerson(name, member);
                }

                else if (command.Equals("SHOW MEMBERS"))
                {
                    Console.WriteLine("Enter the meeting name you want to see the members:");
                    string name = Console.ReadLine();
                    _meetingsService.CheckIFMeetingExists(name);
                    List <string> members = _meetingsService.FindMembers(name);
                    InOutService.PritnToConsoleMembers(members);
                }

                else if (command.Equals("CLEAR CONSOLE"))
                {
                    Console.Clear();
                }

                else if (command.Equals("FILTER"))
                {
                    FilteringService.Filter(_meetingsService);
                }

                else if (command.Equals("HELP"))
                {
                    InOutService.PritntCommands();
                }

                else if (command.Equals("EXIT"))
                {
                    _meetingsService.Write();
                    Environment.Exit(0);
                }

                else
                {
                    Console.WriteLine("Incorrect command");
                }
            }

            catch(ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }

            catch(FormatException ex)
            {
                Console.WriteLine("Something wrong with your parameters");
            }

            catch (Exception ex)
            {
                Console.WriteLine("Something went wrong");
            }
        }
    }
}
