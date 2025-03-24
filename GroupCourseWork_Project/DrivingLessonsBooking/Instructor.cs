// Responsible: Pauline
namespace DrivingLessonsBooking
{
    // Represents an instructor with ID, name, and phone number
    public class Instructor : IComparable<Instructor>
    {
        // Properties for instructor's ID, name, and phone number
        public string InstructorID { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }

        // Default constructor
        public Instructor() { }

        // Parameterized constructor to initialize properties
        public Instructor(string id, string name, string phone)
        {
            InstructorID = id;
            Name = name;
            Phone = phone;
        }

        // Compares instructors by their names
        public int CompareTo(Instructor? other)
        {
            return Name.CompareTo(other?.Name);
        }

        // Returns a string representation of the instructor
        public override string ToString()
        {
            return $"InstructorID: {InstructorID}, Name: {Name}, Phone: {Phone}";
        }
    }
}
