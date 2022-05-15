
using Visma_s.Internal.Meetings.Services;

Console.WriteLine("Enter your Name and Surname:");
string userName = Console.ReadLine();
ApplicationService applicationService = new ApplicationService(userName);

while (true)
{
    Console.WriteLine("Enter your command or Help:");
    string command = Console.ReadLine().ToUpper();
    applicationService.Process(command);
}
