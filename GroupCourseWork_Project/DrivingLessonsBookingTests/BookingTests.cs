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
    }
}
