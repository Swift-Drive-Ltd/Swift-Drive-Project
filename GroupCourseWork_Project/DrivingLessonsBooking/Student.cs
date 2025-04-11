// Define the namespace for the driving lessons booking system
namespace DrivingLessonsBooking
{
    // The Student class represents a student with basic properties.
    // It implements IComparable<Student> to allow sorting or comparing students by name.
    public class Student : IComparable<Student>
    {
        // Property to store the student's unique identifier
        public string StudentID { get; set; }
        
        // Property to store the student's name
        public string Name { get; set; }
        
        // Property to store the student's email address
        public string Email { get; set; }

        // Default constructor.
        // This is required for scenarios such as database retrieval or serialization.
        public Student() { }

        // Parameterized constructor to initialize a new Student object with specific values.
        public Student(string id, string name, string email)
        {
            StudentID = id;  // Set the student's unique identifier
            Name = name;     // Set the student's name
            Email = email;   // Set the student's email address
        }

        // Implementation of the CompareTo method from the IComparable<Student> interface.
        // This method is used to compare one Student object to another, based on the student's name.
        public int CompareTo(Student? other)
        {
            // The '?' operator checks if 'other' is null.
            // If 'other' is null, the current instance is considered greater.
            // Otherwise, compare the names of the two Student objects.
            return Name.CompareTo(other?.Name);
        }

        // Overriding the ToString method to provide a custom string representation of the Student object.
        public override string ToString()
        {
            // Return a formatted string that includes the student's ID, name, and email.
            return $"StudentID: {StudentID}, Name: {Name}, Email: {Email}";
        }
    }
}
