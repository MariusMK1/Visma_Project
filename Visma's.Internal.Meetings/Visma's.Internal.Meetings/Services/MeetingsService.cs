using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Visma_s.Internal.Meetings.Models;

namespace Visma_s.Internal.Meetings.Services
{
    public class MeetingsService
    {
        private List<Meeting> _meetings;
        private string _user;
        public MeetingsService(string userName)
        {
            _meetings = new List<Meeting>();
            if(File.Exists(@"Meetings.json"))
            {
                InOutService.ReadFromJSON(@"Meetings.json", out _meetings);
            }
            _user = userName;
        }
        //Adds a meeting
        public void Add(string name, string responsiblePerson, string description, Category category, MeetingType type, DateTime startDate, DateTime endDate)
        {
            if(_meetings.Any(m => m.Name == name))
            {
                throw new ArgumentException("Meeting with this name already exists");
            }

            Meeting meeting = new Meeting(name, responsiblePerson, description, category, type, startDate, endDate);
            _meetings.Add(meeting);
        }
        /// <summary>
        /// Deletes a meeting
        /// </summary>
        /// <param name="name">Name of a meeting</param>
        /// <exception cref="ArgumentException"></exception>
        public void Delete(string name)
        {
            Meeting meeting = _meetings.Where(m => m.Name == name).First();
            if (meeting.ResponsiblePerson != _user)
            {
                throw new ArgumentException("You can not delete this meeting becouse you are not the responsible person");
            }
            _meetings = _meetings.Where(m => m.Name != name).ToList();
        }
        /// <summary>
        /// Returns all meetings
        /// </summary>
        /// <returns></returns>
        public List<Meeting> GetAll()
        {
            return _meetings;
        }
        /// <summary>
        /// Adds a person to a meeting
        /// </summary>
        /// <param name="name">Name of a meeting</param>
        /// <param name="person">Name and surname of a person to add</param>
        /// <exception cref="ArgumentException"></exception>
        public void AddPerson(string name, string person)
        {
            Meeting meeting = _meetings.Where(m => m.Name == name).First();
            List<string> members = meeting.Members;
            if (members.Contains(person))
            {
                throw new ArgumentException("This person is already in a meeting");
            }

            List<Meeting> meetings = _meetings.Where(m => (m.StartDate <= meeting.StartDate && m.EndDate >= meeting.StartDate) ||
                                                            (m.StartDate <= meeting.EndDate && m.EndDate >= meeting.EndDate)).ToList();
            if (meetings.Any(m => m.Members.Contains(person)))
            {
                throw new ArgumentException("This person is attendig other meeting at the same time");
            }

            members.Add(person);
        }
        /// <summary>
        /// Removes a person from a meeting
        /// </summary>
        /// <param name="name">Name of a meeting</param>
        /// <param name="person">Name and surname of a person to remove</param>
        /// <exception cref="ArgumentException"></exception>
        public void RemovePerson(string name, string person)
        {
            Meeting meeting = _meetings.Where(m => m.Name == name).First();
            if (meeting.ResponsiblePerson == person)
            {
                throw new ArgumentException("This person is the responsible person of the meeting, so he can not be removed");
            }
                meeting.Members = meeting.Members.Where(M => M != person).ToList();
        }
        /// <summary>
        /// Finds all the members of the meeting and return List<string>
        /// </summary>
        /// <param name="name">Name of a meeting</param>
        /// <returns></returns>
        public List<string> FindMembers(string name)
        {
            Meeting meeting = _meetings.Where(m => m.Name == name).First();
            return meeting.Members;
        }
        public void Write()
        {
            InOutService.WriteToJSON(@"Meetings.json", _meetings);
        }
        /// <summary>
        /// Checks if a meeting exists
        /// </summary>
        /// <param name="name">Name of a meeting</param>
        /// <exception cref="ArgumentException"></exception>
        public void CheckIFMeetingExists(string name)
        {
            if (!_meetings.Any(m => m.Name == name))
            {
                throw new ArgumentException("The meeting does not exist");
            }
        }
        // Hera are all the methods for filtering and they all return a filtered List<Meeting>
        public List<Meeting> FilterByDescription(string description)
        {
            List<Meeting> filterd = _meetings.Where(m => m.Description.Contains(description)).ToList();
            return filterd;
        }
        public List<Meeting> FilterByResponsibleP(string responsiblePerson)
        {
            List<Meeting> filterd = _meetings.Where(m => m.ResponsiblePerson == responsiblePerson).ToList();
            return filterd;
        }
        public List<Meeting> FilterByCategory(Category category)
        {
            List<Meeting> filterd = _meetings.Where(m => m.Category == category).ToList();
            return filterd;
        }
        public List<Meeting> FilterByType(MeetingType type)
        {
            List<Meeting> filterd = _meetings.Where(m => m.Type == type).ToList();
            return filterd;
        }
        public List<Meeting> FilterFromStartDate(DateTime date)
        {
            List<Meeting> filterd = _meetings.Where(m => m.StartDate >= date).ToList();
            return filterd;
        }
        public List<Meeting> FilterbyDates(DateTime startDate, DateTime endDate)
        {
            List<Meeting> filterd = _meetings.Where(m => m.StartDate >= startDate && m.EndDate >= startDate &&
                                                            m.StartDate <= endDate && m.EndDate <= endDate).ToList();
            return filterd;
        }
        public List<Meeting> FilterByNumberOfAttendees(int number)
        {
            List<Meeting> filterd = _meetings.Where(m => m.Members.Count() >= number).ToList();
            return filterd;
        }
    }
}
