// Responsible: David

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

        public void LoadBookingsFromDatabase()
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
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
            }
        }

      

