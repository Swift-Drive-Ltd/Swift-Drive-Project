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

        public CustomLinkedList()
        {
            head = null;
        }

        // Insert
        public void Insert(T data)
        {
            Node newNode = new Node(data);
            if (head == null || head.Data.CompareTo(data) > 0)
            {
                newNode.Next = head;
                head = newNode;
                return;
            }

            Node current = head;
            while (current.Next != null && current.Next.Data.CompareTo(data) < 0)
            {
                current = current.Next;
            }
            newNode.Next = current.Next;
            current.Next = newNode;
        }

        // Search
        public T? Search(T data)
        {
            Node? current = head;
            while (current != null)
            {
                if (current.Data.Equals(data))
                {
                    return current.Data;
                }
                current = current.Next;
            }
            return default;
        }
    }
}