using Meetings;
using System.Text.Json;

Console.WriteLine("Hello, the program Meetings is running!");
Console.WriteLine("What is your name?");
string userName;
userName = Console.ReadLine();

// You can change pathMeeting to where you want
string pathMeetings = @"C:\Visma Internship Task\Data.json";
List<Meeting> meetingList = new List<Meeting>();
if (File.Exists(pathMeetings))
{
    var readMeetings = File.ReadAllText(pathMeetings);
    meetingList = JsonSerializer.Deserialize<List<Meeting>>(readMeetings);
}

List<Attendees> attendeesList = new List<Attendees>();

var meet = new AddMeeting();

bool loopContinue = true;
while (loopContinue == true) 
{
    Console.WriteLine("\nCommands:\nAddMeet - adds a new meeting\nAllMeetings - provides the list of all meetings\nAllByDescription- list meetings by description" +
        "\nAllByRP - lists meetings by responsible person\nAllByCategory- lists all meetings of a category\nAllByType- lists meetings of a type" +
        "\nAllByDates- lists meetings by date parameters\nAllByNumA- lists meetings by number of attendees\nDelete - deletes meeting\nAddAttendee - adds a new person to the meeting" +
    "\nRemoveAttendee- removes a person from a meeting\nQuit - quits the program.\n");

    string command;
    command = Console.ReadLine();
    switch(command)
    {
        case "AddMeet":
           
            meetingList.Add(new Meeting()
            { Name = meet.Name(),
              ResponsiblePerson = userName,
              Description = meet.Description(),
              Category = meet.ChooseCateory(), 
              Type = meet.ChooseType(), 
              StartDate = meet.StartDate(),
              EndDate = meet.EndDate(),
              Attendees = new List<Attendees>() 
              {
                  new Attendees()
                  {
                       Attendee = userName,
                       DateTime = meet.AddAttendeeDate(),
                       IsResponsible = true
                  }  
              }
            });
            string jsonData = JsonSerializer.Serialize(meetingList);
            Console.WriteLine(jsonData);
            File.WriteAllText(pathMeetings, jsonData);

            break;
        case "AddAttendee":
            Console.WriteLine("Exsisting meetings:");
            foreach (Meeting meeting in meetingList)
            {
                Console.WriteLine(meeting.Name);

            }
            Console.WriteLine("\nWrite the meetings name ");
            var pickedMeeting = Console.ReadLine();

            var checkAttendee = meet.AddAttendee();
            var checkStartDate = meetingList.First(x => x.Name == pickedMeeting).StartDate;
            var checkEndDate = meetingList.First(x => x.Name == pickedMeeting).EndDate;
            var selectedMeeting = meetingList.First(x => x.Name == pickedMeeting);
            
            if (!meetingList.Any(x => x.Name == pickedMeeting && x.Attendees.Any(y => y.Attendee == checkAttendee)))
            {
                bool test = meetingList.Any(x => x.Attendees.Any(y => y.Attendee == checkAttendee) && ((x.StartDate <= checkStartDate && x.EndDate > checkStartDate) || (x.StartDate < checkEndDate && x.EndDate >= checkEndDate)));

                if (test ==true)
                {
                    Console.WriteLine("\n---------------------------------------------------------------------------------------");
                    Console.WriteLine("!WARNIG! this person is already added to another meeting which intersects with this one.");
                    Console.WriteLine("---------------------------------------------------------------------------------------\n");
                    Console.WriteLine("Do you still want to add him? Y or N");
                    var answer = Console.ReadLine();
                    if (answer =="Y") 
                    {
                        meetingList.First(x => x.Name == pickedMeeting).Attendees.Add(new Attendees()
                        {
                            Attendee = checkAttendee,
                            DateTime = meet.AddAttendeeDate(),
                            IsResponsible = false
                        });
                        Console.WriteLine(checkAttendee + " was added to " + pickedMeeting);

                        jsonData = JsonSerializer.Serialize(meetingList);
                        File.WriteAllText(pathMeetings, jsonData);
                        break;
                    }
                    else if( answer == "N")
                    {
                        break;
                    }
                    else 
                    {
                        Console.WriteLine("You choose something else returning to main menu");
                        break;
                    }
                }
                else 
                {
                    meetingList.First(x => x.Name == pickedMeeting).Attendees.Add(new Attendees()
                    {
                        Attendee = checkAttendee,
                        DateTime = meet.AddAttendeeDate(),
                        IsResponsible = false
                    });
                    Console.WriteLine(checkAttendee + " was added to " + pickedMeeting);
                }    
                
                
            }
            else 
            {
                Console.WriteLine("This person is already added");
                break;
            }
            
            foreach (Meeting meeting in meetingList)
            {
                foreach (Attendees attendee in meeting.Attendees)
                {
                    Console.WriteLine(attendee.Attendee + " " + meeting.Name);
                }
            }
            
            jsonData = JsonSerializer.Serialize(meetingList);
            File.WriteAllText(pathMeetings, jsonData);

            break;
        case "RemoveAttendee":

            foreach (Meeting meeting in meetingList)
            {
                foreach (Attendees attendee in meeting.Attendees)
                {
                    Console.WriteLine(attendee.Attendee + " " + meeting.Name);
                }
            }
            Console.WriteLine("Write meetings name:");
            var pickMeeting = Console.ReadLine();
            Console.WriteLine("Attendee name:");
            var pickAttendee = Console.ReadLine();
            var choosenMeeting = meetingList.First(x => x.Name == pickMeeting);
            if (choosenMeeting.ResponsiblePerson == pickAttendee)
                {
                    Console.WriteLine("\nSelected Person is responsible for the meeting he cannot be removed!");
                }
            else
                {
                    choosenMeeting.Attendees.RemoveAll(v => v.Attendee == pickAttendee);
                }

            jsonData = JsonSerializer.Serialize(meetingList);
            File.WriteAllText(pathMeetings, jsonData);
            break;
        case "Delete":
            Console.WriteLine("Write the name of the meeting to be removed. NOTE! YOU CAN ONLY DELETE MEETINGS YOU ARE RESPONSIBLE FOR, YOU WILL NOT BE ABLE TO DELETE OTHER MEETINGS ");
            var removeMeetingName = Console.ReadLine();
            
            meetingList.Remove(new Meeting() { Name = removeMeetingName, ResponsiblePerson = userName} );

            jsonData = JsonSerializer.Serialize(meetingList);          
            File.WriteAllText(@"C:\Projects\Data.json", jsonData);

            break;
        case "AllMeetings":
            Console.WriteLine("\nMeetings, people responsible for them, description\n");
            foreach(Meeting meeting in meetingList)
            {
                Console.WriteLine("Meeting name: " + meeting.Name + "  Responsible person: " + meeting.ResponsiblePerson + "\nMeeting description: " + meeting.Description);
                Console.WriteLine("-----------------------------");
            }

            break;
        case "AllByDescription":
            Console.WriteLine("Enter description or part of it");

            var searchDescription = Console.ReadLine();
            var resD = meetingList.Where(x => x.Description.Contains(searchDescription));

            if (resD.Count() == 0)
            {
                Console.WriteLine("There are no meetings containing this description");
            }
            else
            {
                foreach (Meeting meeting in resD)
                {
                    Console.WriteLine("Meeting name: " + meeting.Name + "  Responsible person: " + meeting.ResponsiblePerson + "\nMeeting description: " + meeting.Description);
                    Console.WriteLine("-----------------------------");
                }
            }


            break;
        case "AllByCategory":
            Console.WriteLine("Enter category : CodeMonkey, Hub, Short, TeamBuilding ");

            var searchCategory = Console.ReadLine();
            var resC = meetingList.Where(x => x.Category.Equals(searchCategory));

            if (resC.Count() == 0)
            {
                Console.WriteLine("There are no meetings of this category");
            }
            else
            {
                foreach (Meeting meeting in resC)
                {
                    Console.WriteLine("Meeting name: " + meeting.Name + "  Responsible person: " + meeting.ResponsiblePerson + "\nMeeting description: " + meeting.Description);
                    Console.WriteLine("-----------------------------");
                }
            }
            

            break;
        case "AllByType":
            Console.WriteLine("Enter type : Live, InPerson ");

            var searchType= Console.ReadLine();
            var resT = meetingList.Where(x => x.Category.Equals(searchType));

            if (resT.Count() == 0)
            {
                Console.WriteLine("There are no meetings of this type");
            }
            else
            {
                foreach (Meeting meeting in resT)
                {
                    Console.WriteLine("Meeting name: " + meeting.Name + "  Responsible person: " + meeting.ResponsiblePerson + "\nMeeting description: " + meeting.Description);
                    Console.WriteLine("-----------------------------");
                }
            }
            

            break;
        case "AllByDates":
            Console.WriteLine("\nChoose method (write the command): AfterDate, BeforeDate, BetweenDates");
            var selectMethod = Console.ReadLine();
            switch (selectMethod)
            {
                case "AfterDate":
                    Console.WriteLine("Enter the Date in form DD/MM/YYYY");

                    DateOnly dateOnly = DateOnly.Parse(Console.ReadLine());
                    DateTime readDate = dateOnly.ToDateTime(TimeOnly.Parse("00:00 AM"));
                    var resDate = meetingList.Where(x => x.StartDate > readDate);

                    if (resDate.Count() == 0)
                    {
                        Console.WriteLine("There are no meetings after this date");
                    }
                    else
                    {
                        foreach (Meeting meeting in resDate)
                        {
                            Console.WriteLine("Meeting name: " + meeting.Name + "  Responsible person: " + meeting.ResponsiblePerson + "\nMeeting description: " + meeting.Description);
                            Console.WriteLine("-----------------------------");
                        }

                    }
                    break;
                case "BeforeDate":
                    Console.WriteLine("Enter the Date in form DD/MM/YYYY : ");

                    dateOnly = DateOnly.Parse(Console.ReadLine());
                    readDate = dateOnly.ToDateTime(TimeOnly.Parse("00:00 AM"));
                    resDate = meetingList.Where(x => x.StartDate < readDate);

                    if (resDate.Count() == 0)
                    {
                        Console.WriteLine("There are no meetings with such number of attendees");
                    }
                    else
                    {
                        foreach (Meeting meeting in resDate)
                        {
                            Console.WriteLine("Meeting name: " + meeting.Name + "  Responsible person: " + meeting.ResponsiblePerson + "\nMeeting description: " + meeting.Description);
                            Console.WriteLine("-----------------------------");
                        }

                    }
                    break;
                case "BetweenDates":
                    Console.WriteLine("Enter the first Date in form DD/MM/YYYY: ");

                    dateOnly = DateOnly.Parse(Console.ReadLine());
                    readDate = dateOnly.ToDateTime(TimeOnly.Parse("00:00 AM"));

                    Console.WriteLine("Enter the second Date in form DD/MM/YYYY: ");

                    DateOnly dateOnly2 = DateOnly.Parse(Console.ReadLine());
                    DateTime readDate2 = dateOnly2.ToDateTime(TimeOnly.Parse("00:00 AM"));
                    resDate = meetingList.Where(x => x.StartDate > readDate && x.StartDate < readDate2);

                    if (resDate.Count() == 0)
                    {
                        Console.WriteLine("There are no meetings with such number of attendees");
                    }
                    else
                    {
                        foreach (Meeting meeting in resDate)
                        {
                            Console.WriteLine("Meeting name: " + meeting.Name + "  Responsible person: " + meeting.ResponsiblePerson + "\nMeeting description: " + meeting.Description);
                            Console.WriteLine("-----------------------------");
                        }

                    }
                    break;
            }
            break;
        case "AllByRP":
            Console.WriteLine("Enter the name of the person responsible for the meeting: ");

            var searchResPerson = Console.ReadLine();
            var resRP = meetingList.Where(x => x.ResponsiblePerson.Equals(searchResPerson));

            if(resRP.Count() == 0)
            {
                Console.WriteLine("This person is not responsible for any meetings");
            }
            else
            {
                foreach (Meeting meeting in resRP)
                {
                    Console.WriteLine("Meeting name: " + meeting.Name + "  Responsible person: " + meeting.ResponsiblePerson + "\nMeeting description: " + meeting.Description);
                    Console.WriteLine("-----------------------------");
                }

            }

            break;
        case "AllByNumA":
            Console.WriteLine("Choose method (write the command): MoreThen, LessThen, EqualsTo");
            var select = Console.ReadLine();
            switch(select)
            {
                case "MoreThen":
                    Console.WriteLine("Enter the number of attendees: ");

                    int readNum = int.Parse(Console.ReadLine());
                    var resNumA = meetingList.Where(x => x.Attendees.Count > readNum);

                    if (resNumA.Count() == 0)
                    {
                        Console.WriteLine("There are no meetings with such number of attendees");
                    }
                    else
                    {
                        foreach (Meeting meeting in resNumA)
                        {
                            Console.WriteLine("Meeting name: " + meeting.Name + "  Responsible person: " + meeting.ResponsiblePerson + "\nMeeting description: " + meeting.Description);
                            Console.WriteLine("-----------------------------");
                        }

                    }
                    break;
                case "LessThen":
                    Console.WriteLine("Enter the number of attendees: ");

                    readNum = int.Parse(Console.ReadLine());
                    resNumA = meetingList.Where(x => x.Attendees.Count < readNum);

                    if (resNumA.Count() == 0)
                    {
                        Console.WriteLine("There are no meetings with such number of attendees");
                    }
                    else
                    {
                        foreach (Meeting meeting in resNumA)
                        {
                            Console.WriteLine("Meeting name: " + meeting.Name + "  Responsible person: " + meeting.ResponsiblePerson + "\nMeeting description: " + meeting.Description);
                            Console.WriteLine("-----------------------------");
                        }

                    }
                    break;
                case "EqualsTo":
                    Console.WriteLine("Enter the number of attendees: ");

                    readNum = int.Parse(Console.ReadLine());
                    resNumA = meetingList.Where(x => x.Attendees.Count == readNum);

                    if (resNumA.Count() == 0)
                    {
                        Console.WriteLine("There are no meetings with such number of attendees");
                    }
                    else
                    {
                        foreach (Meeting meeting in resNumA)
                        {
                            Console.WriteLine("Meeting name: " + meeting.Name + "  Responsible person: " + meeting.ResponsiblePerson + "\nMeeting description: " + meeting.Description);
                            Console.WriteLine("-----------------------------");
                        }

                    }
                    break;
            }
     
        break;
        case "Quit":
            Environment.Exit(0);
            break;
        default:
            Console.WriteLine("Incorect command");
            break;
    }
}
