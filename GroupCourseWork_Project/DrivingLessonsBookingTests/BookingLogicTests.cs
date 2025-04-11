
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data.SQLite;
using DrivingLessonsBooking;

namespace DrivingLessonsBookingTests
{
    [TestClass]
    public class BookingLogicTests
    {
        private BookingLogic bookingLogic;

        [TestInitialize]
        public void Setup()
        {
            // Use an in-memory SQLite database for testing
            string testConnectionString = "Data Source=:memory:;Version=3;";
            bookingLogic = new BookingLogic(10);

            using (var conn = new SQLiteConnection(testConnectionString))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand(
                    "CREATE TABLE Bookings (BookingID TEXT PRIMARY KEY, StudentID TEXT, InstructorID TEXT, LessonDate TEXT, CarID TEXT);", conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
         [TestMethod]
        public void AddBooking_ShouldInsertBooking()
        {
            // Arrange
            var booking = new Booking("B100", "S001", "I001", DateTime.Now, "C001");

            // Act
            bookingLogic.AddBooking(booking);
            var result = bookingLogic.GetBooking("B100");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("S001", result.studentID);
        }

        [TestMethod]
        public void GetBooking_ShouldReturnCorrectBooking()
        {
            // Arrange
            var booking = new Booking("B002", "Alice", "Bob", DateTime.Now, "Honda");
            bookingLogic.AddBooking(booking);

            // Act
            var result = bookingLogic.GetBooking("B002");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Alice", result.studentID);
            [TestMethod]
        public void DeleteBooking_ShouldRemoveBooking()
        {
            var booking = new Booking("B004", "Eve", "Frank", DateTime.Now, "Mazda");
            bookingLogic.AddBooking(booking);

            bool isDeleted = bookingLogic.DeleteBooking("B004");
            var result = bookingLogic.GetBooking("B004");

            Assert.IsTrue(isDeleted);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void DeleteBooking_ShouldReturnFalseForNonExistentBooking()
        {
            bool isDeleted = bookingLogic.DeleteBooking("B999");

            Assert.IsFalse(isDeleted);
        }
    }
}
