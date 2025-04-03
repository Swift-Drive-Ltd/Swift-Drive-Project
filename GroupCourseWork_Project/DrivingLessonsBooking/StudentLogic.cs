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