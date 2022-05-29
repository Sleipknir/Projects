using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meetings
{
    public class Attendees : IEquatable<Attendees>
    {
        public string Attendee { get; set; }
        public DateTime DateTime { get; set; }
        public bool IsResponsible { get; set; }

        public bool Equals(Attendees? other)
        {
            if (other == null) return false;
            return (this.Attendee.Equals(other.Attendee));
        }
    }
   
    

}
