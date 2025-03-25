// Responsible: Mathews
namespace DrivingLessonsBooking
{
    public class CustomLinkedList<T> where T : IComparable<T>
    {
        private class Node
        {
            public T Data;
            public Node? Next;

            public Node(T data)
            {
                Data = data;
                Next = null;
            }
        }

        private Node? head;

        // Basic constructor
        public CustomLinkedList()
        {
            head = null;
        }
    }
}