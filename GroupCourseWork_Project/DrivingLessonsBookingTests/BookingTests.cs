// Responsible: Nnamdiusing DrivingLessonsBooking;

namespace DrivingLessonsBookingTests
{
    [TestClass]
    public class BookingTests
    {
        [TestMethod]
        public void Booking_Constructor_ShouldInitializeProperties()
        {
            string id = "B001";
            string student = "John Doe";
            string instructor = "Jane Smith";
            DateTime date = new DateTime(2025, 5, 10);
            string car = "Toyota";

            Booking booking = new Booking(id, student, instructor, date, car);

            Assert.AreEqual(id, booking.BookingID);
            Assert.AreEqual(student, booking.studentID);
            Assert.AreEqual(instructor, booking.instructorID);
            Assert.AreEqual(date, booking.LessonDate);
            Assert.AreEqual(car, booking.carID);
        }
        [TestMethod]
        public void Booking_CompareTo_ShouldReturnCorrectValue()
        {
            Booking earlierBooking = new Booking("B001", "Alice", "Bob", new DateTime(2025, 5, 10), "Honda");
            Booking laterBooking = new Booking("B002", "Charlie", "Dan", new DateTime(2025, 6, 10), "Ford");

            Assert.IsTrue(earlierBooking.CompareTo(laterBooking) < 0);
            Assert.IsTrue(laterBooking.CompareTo(earlierBooking) > 0);
            Assert.IsTrue(earlierBooking.CompareTo(earlierBooking) == 0);
        }
        [TestMethod]
        public void Booking_CompareTo_ShouldReturnOneWhenOtherIsNull()
        {
            Booking booking = new Booking("B001", "Alice", "Bob", DateTime.Now, "Honda");


            int result = booking.CompareTo(null);


            Assert.AreEqual(1, result);
        }

    }
}
