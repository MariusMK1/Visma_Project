using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Visma_s.Internal.Meetings.Models
{
    public class Meeting
    {
        public string Name { get; set; }
        public string ResponsiblePerson { get; set; }
        public string Description { get; set; }
        public Category Category { get; set; }
        public MeetingType Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<string> Members { get; set; }

        public Meeting(string name, string responsiblePerson, string Description, Category category, MeetingType type, DateTime startDate, DateTime endDate)
        {
            this.Name = name;
            this.ResponsiblePerson = responsiblePerson;
            this.Description = Description;
            this.Category = category;
            this.Type = type;
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.Members = new List<string>();
        }
    }
}
