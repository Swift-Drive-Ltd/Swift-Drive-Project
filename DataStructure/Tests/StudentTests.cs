using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DrivingSchool.Models;

namespace DrivingSchool.Tests
{
    [TestClass]
    public class StudentTests
    {
        [TestMethod]
        public void Validate_ValidStudent_ReturnsTrue()
        {
            // Arrange
            var student = new Student
            {
                StudentId = 1,
                Name = "John Smith",
                DateOfBirth = DateTime.Now.AddYears(-20),
                Email = "john@example.com",
                LicenseNumber = "ABC123"
            };
            
            // Act
            var result = student.Validate();
            
            // Assert
            Assert.IsTrue(result);
        }
        
        [TestMethod]
        public void Validate_EmptyName_ReturnsFalse()
        {
            // Arrange
            var student = new Student
            {
                StudentId = 1,
                Name = "",
                DateOfBirth = DateTime.Now.AddYears(-20),
                Email = "john@example.com",
                LicenseNumber = "ABC123"
            };
            
            // Act
            var result = student.Validate();
            
            // Assert
            Assert.IsFalse(result);
        }
        
        [TestMethod]
        public void Validate_InvalidEmail_ReturnsFalse()
        {
            // Arrange
            var student = new Student
            {
                StudentId = 1,
                Name = "John Smith",
                DateOfBirth = DateTime.Now.AddYears(-20),
                Email = "invalid-email",
                LicenseNumber = "ABC123"
            };
            
            // Act
            var result = student.Validate();
            
            // Assert
            Assert.IsFalse(result);
        }
        
        [TestMethod]
        public void Validate_TooYoung_ReturnsFalse()
        {
            // Arrange
            var student = new Student
            {
                StudentId = 1,
                Name = "John Smith",
                DateOfBirth = DateTime.Now.AddYears(-15),
                Email = "john@example.com",
                LicenseNumber = "ABC123"
            };
            
            // Act
            var result = student.Validate();
            
            // Assert
            Assert.IsFalse(result);
        }
        
        [TestMethod]
        public void CompareTo_SmallerStudentId_ReturnsNegativeValue()
        {
            // Arrange
            var student1 = new Student { StudentId = 1 };
            var student2 = new Student { StudentId = 2 };
            
            // Act
            var result = student1.CompareTo(student2);
            
            // Assert
            Assert.IsTrue(result < 0);
        }
        
        [TestMethod]
        public void ToString_ValidStudent_ReturnsFormattedString()
        {
            // Arrange
            var student = new Student
            {
                StudentId = 1,
                Name = "John Smith",
                DateOfBirth = new DateTime(2000, 1, 1),
                LicenseNumber = "ABC123"
            };
            
            // Act
            var result = student.ToString();
            
            // Assert
            Assert.IsTrue(result.Contains("1"));
            Assert.IsTrue(result.Contains("John Smith"));
            Assert.IsTrue(result.Contains("ABC123"));
        }
    }
}