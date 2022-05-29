using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meetings
{
    public class AddMeeting
    {
        Meeting meetings = new Meeting();
        Attendees attendees = new Attendees();
        public string Name()
        {
            Console.WriteLine("Meetings name");
            meetings.Name = Console.ReadLine();
            return meetings.Name;
        }
        public string ResponsiblePersonName()
        {
            Console.WriteLine("Responsible persons name");
            meetings.ResponsiblePerson = Console.ReadLine();
            return meetings.ResponsiblePerson;
        }
        public string Description()
        {
            Console.WriteLine("Describe the meeting");
            meetings.Description = Console.ReadLine();
            return meetings.Description;
        }
        public string ChooseCateory()
        {
            Console.WriteLine("Choose category 1-CodeMonkey, 2-Hub, 3-Short, 4-TeamBuilding");
            int categorySelectNum;

            do
            {
                categorySelectNum = int.Parse(Console.ReadLine());

                if (categorySelectNum < 1 || categorySelectNum > 4)
                {
                    Console.WriteLine("You choose wrong number, choose from above mentioned numbers");
                }
            }
            while (categorySelectNum < 1 || categorySelectNum > 4);

                switch (categorySelectNum)
                {
                    case 1:
                        meetings.Category = "CodeMonkey";
                        break;
                    case 2:
                        meetings.Category = "Hub";
                        break;
                    case 3:
                        meetings.Category = "Short";
                        break;
                    case 4:
                        meetings.Category = "TeamBuilding";
                        break;
                   
                }

            Console.WriteLine(meetings.Category);
            return meetings.Category;
        }
        public string ChooseType()
        {
            Console.WriteLine("Choose type 1-Live, 2-InPerson");
            int typeSelectNum;
            do
            {
                typeSelectNum = int.Parse(Console.ReadLine());

                if (typeSelectNum < 1 || typeSelectNum > 2)
                {
                    Console.WriteLine("You choose wrong number, choose from above mentioned numbers");
                }
            }
            while (typeSelectNum < 1 || typeSelectNum > 2);

            switch (typeSelectNum)
            {
                case 1:
                    meetings.Type = "Live";
                    break;
                case 2:
                    meetings.Type = "InPerson";
                    break;
            }
            Console.WriteLine(meetings.Type);
            return meetings.Type;
        }
        public DateTime StartDate()
        {
            Console.WriteLine("Write meeting start date and time in format DD/MM/YYYY  hh:mm");
            meetings.StartDate = DateTime.Parse(Console.ReadLine());
            return meetings.StartDate;
        }
        public DateTime EndDate()
        {
            Console.WriteLine("Write meeting end date and time in format DD/MM/YYYY  hh:mm");
            meetings.EndDate = DateTime.Parse(Console.ReadLine());
            return meetings.EndDate;
        }
        public string AddAttendee()
        {
            Console.WriteLine("\nAttendee name");
            attendees.Attendee = Console.ReadLine();
            return attendees.Attendee;
        }
        public DateTime AddAttendeeDate()
        {
          
            attendees.DateTime = DateTime.Now; 
            return attendees.DateTime;
        }

    }
}
