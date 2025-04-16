using System;
using System.Data.SQLite;

namespace DrivingLessonsBooking
{
    public static class DatabaseStatistics
    {
        private static readonly string connectionString = "Data Source=C:\\sqlite\\gui\\driving-lessons.db;Version=3;";

        public static int GetBookingsCount()
        {
            return GetCountFromTable("Bookings");
        }

        public static int GetStudentsCount()
        {
            return GetCountFromTable("Students");
        }

        public static int GetInstructorsCount()
        {
            return GetCountFromTable("Instructors");
        }

        public static int GetCarsCount()
        {
            return GetCountFromTable("Cars");
        }

        private static int GetCountFromTable(string tableName)
        {
            try
            {
                using (var conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();
                    
                    // Check if the table exists first
                    using (var checkCmd = new SQLiteCommand($"SELECT name FROM sqlite_master WHERE type='table' AND name='{tableName}'", conn))
                    {
                        if (checkCmd.ExecuteScalar() == null)
                            return 0; // Table doesn't exist
                    }

                    // Get count from the table
                    using (var cmd = new SQLiteCommand($"SELECT COUNT(*) FROM {tableName}", conn))
                    {
                        object result = cmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            return Convert.ToInt32(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting {tableName} count: {ex.Message}");
            }
            return 0;
        }
    }
}
