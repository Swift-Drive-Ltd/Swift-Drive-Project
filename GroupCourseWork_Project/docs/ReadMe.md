Our initial commit 4th February 2025(We created the repository on github)

- Today we 9/3/2025 we implemented the Pseudocode for the design implementation at DesignImplementation.md

- Today 12/3/2025 Implemented Visual Documentation showing the following:
1. **Entity-Relationship Diagrams**: Showing relationships between students, instructors, cars, and bookings
2. **Class Diagrams**: Detailing the structure and relationships of all classes in the system
3. **Flowcharts**: Illustrating the execution flow of key algorithms
4. **Sequence Diagrams**: Showing the interaction between system components

To preview the diagram click on the tab of the file and click open preview to view the diagram.

- Today 23/3/2025

Custom Hash Table Implementation

1.Feature Added: Implemented a generic CustomHashTable<TKey, TValue> class in the DrivingLessonsBooking namespace.
Details:
    •	Structure: Uses a hash table with chaining (linked list) to handle collisions.
	•	Key Methods:
	•	Insert(TKey key, TValue value): Adds or updates a key-value pair. If the key exists, updates the value; otherwise, adds a new node to the chain.
	•	Search(TKey key): Retrieves the value associated with a given key, returns default if not found.
	•	Delete(TKey key): Removes a key-value pair from the table, handles cases with and without predecessors in the chain.
	•	DisplayAll(): Prints all values stored in the hash table to the console.
	•	Hashing: Uses GetHashCode() with modulo operation to determine the index, ensuring non-negative results with Math.Abs.
	•	Capacity: Constructor accepts a size parameter to initialize the table array.
	•	Purpose: Provides a custom, flexible data structure for storing and managing key-value pairs, potentially for booking-related data in the DrivingLessonsBooking system.