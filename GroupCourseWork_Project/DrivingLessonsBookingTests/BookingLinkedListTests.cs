// Responsible: Nnamdi
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DrivingLessonsBooking;

namespace DrivingLessonsBookingTests
{
    [TestClass]
    public class CustomLinkedListTests
    {
        [TestMethod]
        public void Insert_ShouldAddElementsInSortedOrder()
        {
            var list = new CustomLinkedList<int>();
            list.Insert(10);
            list.Insert(5);
            list.Insert(20);

            Assert.AreEqual(5, list.Search(5));
            Assert.AreEqual(10, list.Search(10));
            Assert.AreEqual(20, list.Search(20));
        }

        [TestMethod]
        public void Search_ShouldReturnElementIfExists()
        {
            var list = new CustomLinkedList<string>();
            list.Insert("Alice");
            list.Insert("Bob");

            Assert.AreEqual("Alice", list.Search("Alice"));
            Assert.AreEqual("Bob", list.Search("Bob"));
        }

        [TestMethod]
        public void Delete_ShouldReturnFalseIfElementNotFound()
        {
            var list = new CustomLinkedList<int>();
            list.Insert(1);
            list.Insert(2);

            bool result = list.Delete(3);

            Assert.IsFalse(result);
        }
    }
}
