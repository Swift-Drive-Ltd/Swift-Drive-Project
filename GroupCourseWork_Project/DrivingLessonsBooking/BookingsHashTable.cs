// Responsible: Nnamdi
namespace DrivingLessonsBooking
{
    public class CustomHashTable<TKey, TValue>
    {
        private class HashNode
        {
            public TKey Key;
            public TValue Value;
            public HashNode? Next;

            public HashNode(TKey key, TValue value)
            {
                Key = key;
                Value = value;
                Next = null;
            }
        }

        private HashNode[] table;
        private int size;

        public CustomHashTable(int capacity)
        {
            size = capacity;
            table = new HashNode[size];
        }

        private int GetHash(TKey key)
        {
            return Math.Abs(key.GetHashCode()) % size;
        }

        // Insert
        public void Insert(TKey key, TValue value)
        {
            int index = GetHash(key);
            HashNode newNode = new HashNode(key, value);

            if (table[index] == null)
            {
                table[index] = newNode;
            }
            else
            {
                HashNode current = table[index];
                while (current.Next != null)
                {
                    if (current.Key.Equals(key))
                    {
                        current.Value = value; // Update
                        return;
                    }
                    current = current.Next;
                }
                current.Next = newNode;
            }
        }

        // Search
        public TValue Search(TKey key)
        {
            int index = GetHash(key);
            HashNode? current = table[index];

            while (current != null)
            {
                if (current.Key.Equals(key))
                {
                    return current.Value;
                }
                current = current.Next;
            }
            return default;
        }

        // Delete
        public bool Delete(TKey key)
        {
            int index = GetHash(key);
            HashNode current = table[index];
            HashNode prev = null;

            while (current != null)
            {
                if (current.Key.Equals(key))
                {
                    if (prev == null)
                    {
                        table[index] = current.Next;
                    }
                    else
                    {
                        prev.Next = current.Next;
                    }
                    return true;
                }
                prev = current;
                current = current.Next;
            }
            return false;
        }

        // Get
        public void DisplayAll()
        {
            for (int i = 0; i < size; i++)
            {
                HashNode? current = table[i];
                while (current != null)
                {
                    Console.WriteLine(current.Value);
                    current = current.Next;
                }
            }
        }
    }
}