using System;
using System.Data.SQLite;

namespace DrivingLessonsBooking
{
    public class DatabaseSeeder
    {
        // Renamed from Main() to SeedDatabase()
        public static void SeedDatabase()
        {
            string connectionString = "Data Source=C:\\sqlite\\gui\\driving-lessons.db;Version=3;";

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                
                Random random = new Random();
                string[] carModels = { "Toyota Corolla", "Ford Fiesta", "Honda Civic", "Volkswagen Golf" };
                string[] carTypes = { "Manual", "Automatic" };
                string[] studentNames = { "Alice", "Bob", "Charlie", "Diana", "Ethan", "Fiona" };
                string[] instructorNames = { "Mr. Smith", "Ms. Johnson", "Mr. Brown", "Ms. Davis" };

                // Insert Students
                for (int i = 0; i < 500; i++)
                {
                    string studentId = Guid.NewGuid().ToString();
                    string name = studentNames[random.Next(studentNames.Length)] + " " + random.Next(1000);
                    string email = "student" + i + "@email.com";

                    var cmd = new SQLiteCommand("INSERT INTO Students (StudentID, Name, Email) VALUES (@id, @name, @email)", connection);
                    cmd.Parameters.AddWithValue("@id", studentId);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.ExecuteNonQuery();
                }

                // Insert Instructors
                for (int i = 0; i < 50; i++)
                {
                    string instructorId = Guid.NewGuid().ToString();
                    string name = instructorNames[random.Next(instructorNames.Length)] + " " + random.Next(100);
                    string phone = "07" + random.Next(100000000, 999999999).ToString();

                    var cmd = new SQLiteCommand("INSERT INTO Instructors (InstructorID, Name, Phone) VALUES (@id, @name, @phone)", connection);
                    cmd.Parameters.AddWithValue("@id", instructorId);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@phone", phone);
                    cmd.ExecuteNonQuery();
                }

                // Insert Cars
                for (int i = 0; i < 50; i++)
                {
                    string carId = Guid.NewGuid().ToString();
                    string model = carModels[random.Next(carModels.Length)];
                    string type = carTypes[random.Next(carTypes.Length)];
                    string plate = "CAR" + random.Next(1000, 9999);

                    var cmd = new SQLiteCommand("INSERT INTO Cars (CarID, Model, Type, LicensePlate) VALUES (@id, @model, @type, @plate)", connection);
                    cmd.Parameters.AddWithValue("@id", carId);
                    cmd.Parameters.AddWithValue("@model", model);
                    cmd.Parameters.AddWithValue("@type", type);
                    cmd.Parameters.AddWithValue("@plate", plate);
                    cmd.ExecuteNonQuery();
                }

                // Insert Bookings (StudentID, InstructorID, CarID, LessonDate)
                var studentIds = new System.Collections.Generic.List<string>();
                var instructorIds = new System.Collections.Generic.List<string>();
                var carIds = new System.Collections.Generic.List<string>();

                var getStudentIds = new SQLiteCommand("SELECT StudentID FROM Students", connection);
                using (var reader = getStudentIds.ExecuteReader())
                {
                    while (reader.Read()) studentIds.Add(reader.GetString(0));
                }

                var getInstructorIds = new SQLiteCommand("SELECT InstructorID FROM Instructors", connection);
                using (var reader = getInstructorIds.ExecuteReader())
                {
                    while (reader.Read()) instructorIds.Add(reader.GetString(0));
                }

                var getCarIds = new SQLiteCommand("SELECT CarID FROM Cars", connection);
                using (var reader = getCarIds.ExecuteReader())
                {
                    while (reader.Read()) carIds.Add(reader.GetString(0));
                }

                for (int i = 0; i < 500; i++)
                {
                    string bookingId = Guid.NewGuid().ToString();
                    string studentId = studentIds[random.Next(studentIds.Count)];
                    string instructorId = instructorIds[random.Next(instructorIds.Count)];
                    string carId = carIds[random.Next(carIds.Count)];
                    string lessonDate = DateTime.Now.AddDays(random.Next(1, 365)).ToString("yyyy-MM-dd");

                    var cmd = new SQLiteCommand("INSERT INTO Bookings (BookingID, StudentID, InstructorID, CarID, LessonDate) VALUES (@id, @student, @instructor, @car, @date)", connection);
                    cmd.Parameters.AddWithValue("@id", bookingId);
                    cmd.Parameters.AddWithValue("@student", studentId);
                    cmd.Parameters.AddWithValue("@instructor", instructorId);
                    cmd.Parameters.AddWithValue("@car", carId);
                    cmd.Parameters.AddWithValue("@date", lessonDate);
                    cmd.ExecuteNonQuery();
                }

                Console.WriteLine("Database seeded successfully!");
            }
        }
    }
}
