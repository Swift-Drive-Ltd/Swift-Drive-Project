using System.Data.SQLite;

namespace DrivingLessonsBooking
{
    public class BookingLogic
    {
        private CustomHashTable<string, Booking> bookings;
        private SortedLinkedList<Booking> sortedBookings; // Maintain sorted order by date
        private string connectionString = "Data Source=C:\\sqlite\\gui\\driving-lessons.db;Version=3;";

        public BookingLogic(int capacity)
        {
            bookings = new CustomHashTable<string, Booking>(capacity);
            sortedBookings = new SortedLinkedList<Booking>();
            LoadBookingsFromDatabase();
        }

        public void CheckDatabaseConnection()
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                Console.WriteLine("Connection to SQLite database established.");

                // Check if the Bookings table exists
                using (var cmd = new SQLiteCommand("SELECT name FROM sqlite_master WHERE type='table' AND name='Bookings';", conn))
                {
                    var result = cmd.ExecuteScalar();
                    if (result == null)
                    {
                        Console.WriteLine("Table 'Bookings' does not exist.");
                    }
                    else
                    {
                        Console.WriteLine("Table 'Bookings' exists.");
                    }
                }

                conn.Close();
                Console.WriteLine("Connection to SQLite database closed.");
            }
        }

        public void LoadBookingsFromDatabase()
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                Console.WriteLine("Connection to SQLite database established.");

                // Check if the Bookings table exists
                using (var cmd = new SQLiteCommand("SELECT name FROM sqlite_master WHERE type='table' AND name='Bookings';", conn))
                {
                    var result = cmd.ExecuteScalar();
                    if (result == null)
                    {
                        Console.WriteLine("Table 'Bookings' does not exist.");
                        return;
                    }
                }

                using (var cmd = new SQLiteCommand("SELECT * FROM Bookings", conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Booking booking = new Booking(
                            reader["BookingID"].ToString(),
                            reader["StudentID"].ToString(),
                            reader["InstructorID"].ToString(),
                            DateTime.Parse(reader["LessonDate"].ToString()),
                            reader["CarID"].ToString()
                        );

                        bookings.Insert(booking.BookingID, booking);
                        sortedBookings.Insert(booking);
                    }
                }

                conn.Close();
                Console.WriteLine("Connection to SQLite database closed.");
            }
        }

        public void AddBooking(Booking booking)
        {
            if (bookings.Search(booking.BookingID) != null)
            {
                Console.WriteLine("Booking ID already exists.");
                return;
            }

            bookings.Insert(booking.BookingID, booking);
            sortedBookings.Insert(booking);

            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand(
                    "INSERT INTO Bookings (BookingID, StudentID, InstructorID, LessonDate, CarID) VALUES (@id, @student, @instructor, @date, @car)",
                    conn))
                {
                    cmd.Parameters.AddWithValue("@id", booking.BookingID);
                    cmd.Parameters.AddWithValue("@student", booking.studentID);
                    cmd.Parameters.AddWithValue("@instructor", booking.instructorID);
                    cmd.Parameters.AddWithValue("@date", booking.LessonDate);
                    cmd.Parameters.AddWithValue("@car", booking.carID);
                    cmd.ExecuteNonQuery();
                }
            }

            Console.WriteLine("Booking successfully added.");
        }

        public Booking GetBooking(string id)
        {
            return bookings.Search(id);
        }

        public void ModifyBooking(string id, string studentName, string instructorName, DateTime lessonDate, string carType)
        {
            Booking existingBooking = bookings.Search(id);
            if (existingBooking == null)
            {
                Console.WriteLine("Booking not found.");
                return;
            }

            sortedBookings.Delete(existingBooking);

            existingBooking.studentID = studentName;
            existingBooking.instructorID = instructorName;
            existingBooking.LessonDate = lessonDate;
            existingBooking.carID = carType;

            sortedBookings.Insert(existingBooking);

            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand(
                    "UPDATE Bookings SET StudentName = @student, InstructorName = @instructor, LessonDate = @date, CarType = @car WHERE BookingID = @id",
                    conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@student", studentName);
                    cmd.Parameters.AddWithValue("@instructor", instructorName);
                    cmd.Parameters.AddWithValue("@date", lessonDate);
                    cmd.Parameters.AddWithValue("@car", carType);
                    cmd.ExecuteNonQuery();
                }
            }

            Console.WriteLine("Booking updated successfully.");
        }

        public bool DeleteBooking(string id)
        {
            Booking toRemove = bookings.Search(id);
            if (toRemove == null)
            {
                Console.WriteLine("Booking not found.");
                return false;
            }

            bookings.Delete(id);
            sortedBookings.Delete(toRemove);

            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("DELETE FROM Bookings WHERE BookingID = @id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }

            Console.WriteLine("Booking deleted successfully.");
            return true;
        }

        public void DisplayBookingsSorted()
        {
            sortedBookings.Display();
        }
    }
}
