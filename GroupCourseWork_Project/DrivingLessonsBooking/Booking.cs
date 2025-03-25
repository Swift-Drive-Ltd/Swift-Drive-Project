namespace DrivingLessonsBooking
{
    public class Booking : IComparable<Booking>
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

        public int CompareTo(Booking? other)
        {
            if (other == null) return 1;
            return LessonDate.CompareTo(other.LessonDate);
        }

        public override string ToString()
        {
            return $"BookingID: {BookingID}, Student: {StudentID}, Instructor: {InstructorID}, Date: {LessonDate:yyyy-MM-dd}, Car: {CarID}";
        }
    }
}