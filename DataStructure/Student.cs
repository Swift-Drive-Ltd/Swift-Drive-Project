namespace DrivingSchool.Models
{
    /// <summary>
    /// Represents a student in the driving school system
    /// </summary>
    public class Student : IComparable<Student>
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public string LicenseNumber { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public Student()
        {
        }

        /// <summary>
        /// Constructor with all properties
        /// </summary>
        public Student(int id, string name, DateTime dob, string contactNumber, string email, string licenseNumber)
        {
            StudentId = id;
            Name = name;
            DateOfBirth = dob;
            ContactNumber = contactNumber;
            Email = email;
            LicenseNumber = licenseNumber;
        }

        /// <summary>
        /// Validates if the student data is correct
        /// </summary>
        /// <returns>True if valid, false otherwise</returns>
        public bool Validate()
        {
            // Basic validation
            if (string.IsNullOrEmpty(Name)) return false;
            if (string.IsNullOrEmpty(Email)) return false;
            if (string.IsNullOrEmpty(LicenseNumber)) return false;
            
            // Email should contain @ symbol
            if (!Email.Contains("@")) return false;
            
            // Student should be at least 16 years old
            var age = DateTime.Now.Year - DateOfBirth.Year;
            if (DateTime.Now.DayOfYear < DateOfBirth.DayOfYear) age--;
            if (age < 16) return false;
            
            return true;
        }

        /// <summary>
        /// Compares this student to another student
        /// </summary>
        /// <param name="other">The student to compare with</param>
        /// <returns>Comparison result (-1, 0, or 1)</returns>
        public int CompareTo(Student other)
        {
            // Compare by ID by default
            return this.StudentId.CompareTo(other.StudentId);
        }

        /// <summary>
        /// Returns a string representation of the student
        /// </summary>
        public override string ToString()
        {
            return $"Student {StudentId}: {Name}, DOB: {DateOfBirth:d}, License: {LicenseNumber}";
        }
    }
}