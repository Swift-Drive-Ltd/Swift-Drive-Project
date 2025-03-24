// Responsible: Mathews
namespace DrivingLessonsBooking
{
    public class Booking
    {
        public string BookingID { get; set; }
        public string StudentID { get; set; }
        public string InstructorID { get; set; }
        public DateTime LessonDate { get; set; }
        public string CarID { get; set; }

        public Booking() { }

        public Booking(string id, string student, string instructor, DateTime date, string car)
        {
            BookingID = id;
            StudentID = student;
            InstructorID = instructor;
            LessonDate = date;
            CarID = car;
        }
    }
}