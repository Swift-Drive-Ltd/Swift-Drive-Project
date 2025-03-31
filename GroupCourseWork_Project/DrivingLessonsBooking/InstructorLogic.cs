using System;
using System.Data.SQLite; // For SQLite database interaction.
using System.Collections.Generic; // For collections like SortedLinkedList.

namespace DrivingLessonsBooking
{
    public class InstructorLogic
    {
        // A custom hash table to store instructors with their IDs as keys.
        private CustomHashTable<string, Instructor> instructors;

        // A sorted linked list to maintain instructors in a sorted order.
        private SortedLinkedList<Instructor> sortedInstructors;

        // Connection string for the SQLite database.
        private string connectionString = "Data Source=C:\\sqlite\\gui\\driving-lessons.db;Version=3;";

        // Constructor initializes the hash table and sorted linked list with a given capacity.
        public InstructorLogic(int capacity)
        {
            // Initialize the custom hash table with the specified capacity.
            instructors = new CustomHashTable<string, Instructor>(capacity);

            // Initialize the sorted linked list.
            sortedInstructors = new SortedLinkedList<Instructor>();

            // Load existing instructors from the database into memory.
            LoadInstructorsFromDatabase();
        }

        // Method to load all instructors from the SQLite database into memory.
        public void LoadInstructorsFromDatabase()
        {
            // Open a connection to the SQLite database.
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                // Create a SQL command to select all rows from the Instructors table.
                using (var cmd = new SQLiteCommand("SELECT * FROM Instructors", conn))
                using (var reader = cmd.ExecuteReader()) // Execute the query and get a data reader.
                {
                    // Iterate through each row returned by the query.
                    while (reader.Read())
                    {
                        // Create an Instructor object from the database row.
                        Instructor instructor = new Instructor(
                            reader["InstructorID"].ToString(), // ID of the instructor.
                            reader["Name"].ToString(),         // Name of the instructor.
                            reader["Phone"].ToString()         // Phone number of the instructor.
                        );

                        
                        // Insert the instructor into the hash table using their ID as the key.
                        instructors.Insert(instructor.InstructorID, instructor);

                        // Insert the instructor into the sorted linked list.
                        sortedInstructors.Insert(instructor);
                    }
                }
            }
        }

        // Method to add a new instructor to the system and database.
        public void AddInstructor(Instructor instructor)
        {
            // Check if an instructor with the same ID already exists in the hash table.
            if (instructors.Search(instructor.InstructorID) != null)
            {
                Console.WriteLine("Instructor ID already exists."); // Notify the user.
                return; // Exit the method if the ID is not unique.
            }

            // Insert the new instructor into the hash table and sorted linked list.
            instructors.Insert(instructor.InstructorID, instructor);
            sortedInstructors.Insert(instructor);

            // Open a connection to the SQLite database.
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                // Create a SQL command to insert the new instructor into the database.
                using (var cmd = new SQLiteCommand(
                    "INSERT INTO Instructors (InstructorID, Name, Phone) VALUES (@id, @name, @phone)",
                    conn))
                {
                    // Add parameters to prevent SQL injection.
                    cmd.Parameters.AddWithValue("@id", instructor.InstructorID);
                    cmd.Parameters.AddWithValue("@name", instructor.Name);
                    cmd.Parameters.AddWithValue("@phone", instructor.Phone);

                    // Execute the command to insert the record into the database.
                    cmd.ExecuteNonQuery();
                }
            }

            Console.WriteLine("Instructor successfully added."); // Notify the user of success.
        }


