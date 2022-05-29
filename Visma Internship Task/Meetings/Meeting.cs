using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meetings
{
    public class Meeting : IEquatable<Meeting>
    {
        public string? Name { get; set; }
        public string ResponsiblePerson { get; set; }
        public string Description { get; set; }

        public string Category { get; set; }

        public string Type { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public List<Attendees> Attendees { get; set; }

    public override bool Equals(object obj)
        {
            if (obj == null) return false;
            Meeting objAsMeeting = obj as Meeting;
            if (objAsMeeting == null) return false;
            else return Equals(objAsMeeting);
        }
        public bool Equals(Meeting other)
        {
            if (other == null) return false;
            return (this.Name.Equals(other.Name) && this.ResponsiblePerson.Equals(other.ResponsiblePerson));
        }
      
    }
}
