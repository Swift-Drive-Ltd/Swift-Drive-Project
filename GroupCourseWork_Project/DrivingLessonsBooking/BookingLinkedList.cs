namespace DrivingLessonsBooking
{
    public class CustomLinkedList<T> where T : IComparable<T>
    {
        // Represents a node in the linked list
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

        // Insert a new element into the linked list in sorted order
        public void Insert(T data)
        {
            Node newNode = new Node(data);

           
            if (head == null || head.Data.CompareTo(data) > 0)
            {
                newNode.Next = head;
                head = newNode;
                return;
            }

            // Traverse the list to find the correct position for the new node
            Node current = head;
            while (current.Next != null && current.Next.Data.CompareTo(data) < 0)
            {
                current = current.Next;
            }

            
            newNode.Next = current.Next;
            current.Next = newNode;
        }

        // Search for an element in the linked list
        public T? Search(T data)
        {
            Node? current = head;

            // Traverse the list to find the matching element
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

        // Delete an element from the linked list
        public bool Delete(T data)
        {
            if (head == null) return false; 

            // If the head is the element to delete
            if (head.Data.Equals(data))
            {
                head = head.Next; 
                return true;
            }

            Node? current = head;
            Node? prev = null;

            // Traverse the list to find the element to delete
            while (current != null && !current.Data.Equals(data))
            {
                prev = current;
                current = current.Next;
            }

            if (current == null) return false; 

            // Remove the node by updating the previous node's reference
            prev!.Next = current.Next;
            return true;
        }

        // Display all elements in the linked list
        public void Display()
        {
            Node? current = head;

            // Traverse the list and print each element
            while (current != null)
            {
                Console.WriteLine(current.Data);
                current = current.Next;
            }
        }
    }
}