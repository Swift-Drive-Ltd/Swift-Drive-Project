// Responsible: David

using System;
using System.Data.SQLite;
using System.Collections.Generic;

namespace DrivingLessonsBooking
{
    public class StudentLogic
    {
        private CustomHashTable<string, Student> students;
        private SortedLinkedList<Student> sortedStudents;
        private string connectionString = "Data Source=C:\\sqlite\\gui\\driving-lessons.db;Version=3;";

        public StudentLogic(int capacity)
        {
            students = new CustomHashTable<string, Student>(capacity);
            sortedStudents = new SortedLinkedList<Student>();
            LoadStudentsFromDatabase();

 public void LoadStudentsFromDatabase()
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("SELECT * FROM Students", conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Student student = new Student(
                            reader["StudentID"].ToString(),
                            reader["Name"].ToString(),
                            reader["Email"].ToString()
                        );

                        students.Insert(student.StudentID, student);
                        sortedStudents.Insert(student);
                    }
                }
            }
        }
         public void AddStudent(Student student)
        {
            if (students.Search(student.StudentID) != null)
            {
                Console.WriteLine("Student ID already exists.");
                return;
            }

            students.Insert(student.StudentID, student);
            sortedStudents.Insert(student);

            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand(
                    "INSERT INTO Students (StudentID, Name, Email) VALUES (@id, @name, @email)",
                    conn))
                {
                    cmd.Parameters.AddWithValue("@id", student.StudentID);
                    cmd.Parameters.AddWithValue("@name", student.Name);
                    cmd.Parameters.AddWithValue("@email", student.Email);
                    cmd.ExecuteNonQuery();
                }
            }

            Console.WriteLine("Student successfully added.");
        }