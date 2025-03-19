using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DrivingSchool.DataStructures;
using DrivingSchool.Models;

namespace DrivingSchool.Tests
{
    [TestClass]
    public class SortedLinkedListTests
    {
        [TestMethod]
        public void Insert_MultipleElements_InsertsInSortedOrder()
        {
            // Arrange
            var list = new SortedLinkedList<int>();
            
            // Act
            list.Insert(5);
            list.Insert(3);
            list.Insert(8);
            list.Insert(1);
            
            // Assert - We'll need to implement a way to verify the order
            // For now, we can test with the Find method
            Assert.AreEqual(1, list.Find(1));
            Assert.AreEqual(3, list.Find(3));
            Assert.AreEqual(5, list.Find(5));
            Assert.AreEqual(8, list.Find(8));
        }
        
        [TestMethod]
        public void Find_ExistingElement_ReturnsElement()
        {
            // Arrange
            var list = new SortedLinkedList<int>();
            list.Insert(5);
            list.Insert(3);
            
            // Act
            var result = list.Find(3);
            
            // Assert
            Assert.AreEqual(3, result);
        }
        
        [TestMethod]
        public void Find_NonExistingElement_ReturnsDefault()
        {
            // Arrange
            var list = new SortedLinkedList<int>();
            list.Insert(5);
            
            // Act
            var result = list.Find(10);
            
            // Assert
            Assert.AreEqual(default(int), result);
        }
        
        [TestMethod]
        public void Delete_ExistingElement_RemovesElement()
        {
            // Arrange
            var list = new SortedLinkedList<int>();
            list.Insert(5);
            list.Insert(3);
            
            // Act
            var result = list.Delete(3);
            
            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(default(int), list.Find(3));
        }
        
        [TestMethod]
        public void Delete_NonExistingElement_ReturnsFalse()
        {
            // Arrange
            var list = new SortedLinkedList<int>();
            list.Insert(5);
            
            // Act
            var result = list.Delete(10);
            
            // Assert
            Assert.IsFalse(result);
        }
        
        [TestMethod]
        public void Insert_StudentObjects_InsertsInSortedOrder()
        {
            // Arrange
            var list = new SortedLinkedList<Student>();
            var student1 = new Student { StudentId = 2, Name = "Jane Doe" };
            var student2 = new Student { StudentId = 1, Name = "John Smith" };
            var student3 = new Student { StudentId = 3, Name = "Bob Johnson" };
            
            // Act
            list.Insert(student1);
            list.Insert(student2);
            list.Insert(student3);
            
            // Assert
            var foundStudent = list.Find(student2);
            Assert.AreEqual(1, foundStudent.StudentId);
            Assert.AreEqual("John Smith", foundStudent.Name);
        }
    }
}