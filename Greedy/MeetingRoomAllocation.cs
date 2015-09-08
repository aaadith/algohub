using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Model;

namespace Greedy
{
    //note - work in progress ; untested code

    public class MeetingRoom
    {
        public List<Meeting> BookedSlots = new List<Meeting>();
    }

    public class Meeting :IComparable<Meeting>
    {
        public Interval<DateTime> Schedule {get; set;}

        public int CompareTo(Meeting other)
        {
            return this.Schedule.CompareTo(other.Schedule);
        }
    }
    public class MeetingRoomAllocation
    {
        public void AssignMeetingsToRooms(List<MeetingRoom> rooms, List<Meeting> meetings)
        {
            meetings.Sort();

            foreach(MeetingRoom room in rooms)
            {
                Meeting[] meetingsToBeAssignedRooms = meetings.ToArray();

                foreach(Meeting meeting in meetingsToBeAssignedRooms)
                {
                    if(room.BookedSlots.Count==0)
                    {
                        meetings.Remove(meeting);
                        room.BookedSlots.Add(meeting);
                        continue;
                    }
                    Meeting lastMeeting = room.BookedSlots.Last();
                    if(!lastMeeting.Schedule.Overlaps(meeting.Schedule))
                    {
                        meetings.Remove(meeting);
                        room.BookedSlots.Add(meeting);
                    }
                }               
            }

            if (meetings.Count > 0)
                throw new Exception("all meetings could not be allocated to meeting rooms");
        }
    }

    
}
