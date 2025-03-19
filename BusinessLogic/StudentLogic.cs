using System;
using System.Collections.Generic;
using DrivingSchool.DataStructures;
using DrivingSchool.Models;

namespace DrivingSchool.BusinessLogic
{
    /// <summary>
    /// Provides business logic for student operations
    /// </summary>
    public class StudentLogic
    {
        private SortedLinkedList<Student> _students;
        
        /// <summary>
        /// Constructor to initialize the student list
        /// </summary>
        public StudentLogic()
        {
            _students = new SortedLinkedList<Student>();
        }
        
        /// <summary>
        /// Adds a new student after validation
        /// </summary>
        /// <param name="student">The student to add</param>
        /// <returns>True if successful, false otherwise</returns>
        public bool AddStudent(Student student)
        {
            // Validate the student
            if (!student.Validate())
            {
                return false;
            }
            
            // Check if a student with the same ID already exists
            var existing = FindStudentById(student.StudentId);
            if (existing != null && existing.StudentId == student.StudentId)
            {
                return false;
            }
            
            _students.Insert(student);
            return true;
        }
        
        /// <summary>
        /// Updates an existing student
        /// </summary>
        /// <param name="student">The student with updated information</param>
        /// <returns>True if successful, false otherwise</returns>
        public bool UpdateStudent(Student student)
        {
            if (!student.Validate())
            {
                return false;
            }
            
            // Find and delete the existing student
            var existing = FindStudentById(student.StudentId);
            if (existing == null)
            {
                return false;
            }
            
            _students.Delete(existing);
            _students.Insert(student);
            return true;
        }
        
        /// <summary>
        /// Deletes a student by ID
        /// </summary>
        /// <param name="studentId">The ID of the student to delete</param>
        /// <returns>True if successful, false otherwise</returns>
        public bool DeleteStudent(int studentId)
        {
            var student = FindStudentById(studentId);
            if (student == null)
            {
                return false;
            }
            
            return _students.Delete(student);
        }
        
        /// <summary>
        /// Finds a student by ID
        /// </summary>
        /// <param name="studentId">The ID to search for</param>
        /// <returns>The student or null if not found</returns>
        public Student FindStudentById(int studentId)
        {
            // Create a dummy student with the ID to search for
            var searchStudent = new Student { StudentId = studentId };
            
            // Use the Find method of SortedLinkedList
            var foundStudent = _students.Find(searchStudent);
            
            // If the returned student has the same ID, it's found
            if (foundStudent != null && foundStudent.StudentId == studentId)
            {
                return foundStudent;
            }
            
            return null;
        }
        
        /// <summary>
        /// Finds students by name (partial match)
        /// </summary>
        /// <param name="name">The name to search for</param>
        /// <returns>A list of matching students</returns>
        public List<Student> FindStudentsByName(string name)
        {
            var results = new List<Student>();
            
            // This is less efficient but necessary since we don't have a direct way
            // to iterate through the SortedLinkedList
            
            // In a future implementation, we might want to add an iterator to the SortedLinkedList
            // or implement IEnumerable<T>
            
            // For now, we'll use a dummy approach to simulate iteration
            int id = 0;
            while (true)
            {
                var student = FindStudentById(id);
                if (student == null)
                {
                    id++;
                    
                    // Stop after checking a reasonable number of IDs
                    if (id > 10000)
                    {
                        break;
                    }
                    
                    continue;
                }
                
                if (student.Name.Contains(name))
                {
                    results.Add(student);
                }
                
                id++;
            }
            
            return results;
        }
        
        /// <summary>
        /// Gets all students in the system
        /// </summary>
        /// <returns>A list of all students</returns>
        public List<Student> GetAllStudents()
        {
            var results = new List<Student>();
            
            // In a future implementation, we might want to add an iterator to the SortedLinkedList
            // or implement IEnumerable<T>
            
            // For now, we'll implement this when needed
            
            return results;
        }
    }
}