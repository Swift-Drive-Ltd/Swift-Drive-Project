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

      // Method to retrieve an instructor by their ID.
        public Instructor GetInstructor(string id)
        {
            // Search for the instructor in the hash table using their ID.
            return instructors.Search(id);
        }

        // Method to modify an existing instructor's details.
        public void ModifyInstructor(string id, string name, string phone)
        {
            // Search for the instructor in the hash table using their ID.
            Instructor existingInstructor = instructors.Search(id);

            // If the instructor is not found, notify the user and exit the method.
            if (existingInstructor == null)
            {
                Console.WriteLine("Instructor not found.");
                return;
            }

            // Remove the instructor from the sorted linked list to update their details.
            sortedInstructors.Delete(existingInstructor);

            // Update the instructor's name and phone number.
            existingInstructor.Name = name;
            existingInstructor.Phone = phone;

            // Reinsert the updated instructor into the sorted linked list.
            sortedInstructors.Insert(existingInstructor);

            // Open a connection to the SQLite database.
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                // Create a SQL command to update the instructor's details in the database.
                using (var cmd = new SQLiteCommand(
                    "UPDATE Instructors SET Name = @name, Phone = @phone WHERE InstructorID = @id",
                    conn))
                {
                    // Add parameters to prevent SQL injection.
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@phone", phone);

                    // Execute the command to update the record in the database.
                    cmd.ExecuteNonQuery();
                }
            }

            Console.WriteLine("Instructor updated successfully."); // Notify the user of success.
        }

 // Method to delete an instructor from the system and database.
        public bool DeleteInstructor(string id)
        {
            // Search for the instructor in the hash table using their ID.
            Instructor toRemove = instructors.Search(id);

            // If the instructor is not found, notify the user and exit the method.
            if (toRemove == null)
            {
                Console.WriteLine("Instructor not found.");
                return false; // Return false to indicate failure.
            }

            // Remove the instructor from the hash table and sorted linked list.
            instructors.Delete(id);
            sortedInstructors.Delete(toRemove);

            // Open a connection to the SQLite database.
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                // Create a SQL command to delete the instructor from the database.
                using (var cmd = new SQLiteCommand("DELETE FROM Instructors WHERE InstructorID = @id", conn))
                {
                    // Add the ID parameter to prevent SQL injection.
                    cmd.Parameters.AddWithValue("@id", id);

                    // Execute the command to delete the record from the database.
                    cmd.ExecuteNonQuery();
                }
            }

            Console.WriteLine("Instructor deleted successfully."); // Notify the user of success.
            return true; // Return true to indicate success.
        }

        // Method to display all instructors in sorted order.
        public void DisplayInstructorsSorted()
        {
            // Call the display method of the sorted linked list to print instructors.
            sortedInstructors.Display();
        }
    }
}
