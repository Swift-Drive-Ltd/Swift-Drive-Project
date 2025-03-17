using System;

namespace DrivingSchool.DataStructures
{
    /// <summary>
    /// A generic sorted linked list implementation
    /// </summary>
    /// <typeparam name="T">Type of elements to store, must be comparable</typeparam>
    public class SortedLinkedList<T> where T : IComparable<T>
    {
        private class Node
        {
            public T Data { get; set; }
            public Node Next { get; set; }

            public Node(T data)
            {
                Data = data;
                Next = null;
            }
        }

        private Node head;

        public SortedLinkedList()
        {
            head = null;
        }

        /// <summary>
        /// Inserts an item into the sorted linked list
        /// </summary>
        /// <param name="data">The item to insert</param>
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

        /// <summary>
        /// Finds an item in the linked list
        /// </summary>
        /// <param name="data">The item to find</param>
        /// <returns>The found item or default value if not found</returns>
        public T Find(T data)
        {
            Node current = head;
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

        /// <summary>
        /// Deletes an item from the linked list
        /// </summary>
        /// <param name="data">The item to delete</param>
        /// <returns>True if deleted, false if not found</returns>
        public bool Delete(T data)
        {
            if (head == null) return false;

            if (head.Data.Equals(data))
            {
                head = head.Next;
                return true;
            }

            Node current = head;
            Node prev = null;

            while (current != null && !current.Data.Equals(data))
            {
                prev = current;
                current = current.Next;
            }

            if (current == null) return false;

            prev.Next = current.Next;
            return true;
        }

        /// <summary>
        /// Displays all items in the linked list
        /// </summary>
        public void Display()
        {
            Node current = head;
            while (current != null)
            {
                Console.WriteLine(current.Data);
                current = current.Next;
            }
        }
    }
}