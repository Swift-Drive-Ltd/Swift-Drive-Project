namespace DrivingLessonsBooking
{
    public class SortedLinkedList<T> where T : IComparable<T>
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

        public void Delete(T data)
        {
            if (head == null) return;

            if (head.Data.Equals(data))
            {
                head = head.Next;
                return;
            }

            Node current = head;
            Node? prev = null;

            while (current != null && !current.Data.Equals(data))
            {
                prev = current;
                current = current.Next;
            }

            if (current != null) prev.Next = current.Next;
        }

        public void Display()
        {
            Node? current = head;
            while (current != null)
            {
                Console.WriteLine(current.Data);
                current = current.Next;
            }
        }
    }
}


