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


