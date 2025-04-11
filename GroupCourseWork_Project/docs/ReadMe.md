Our initial commit 4th February 2025(We created the repository on github)

- Today we 9/3/2025 we implemented the Pseudocode for the design implementation at DesignImplementation.md

- Today 12/3/2025 Implemented Visual Documentation showing the following:
1. **Entity-Relationship Diagrams**: Showing relationships between students, instructors, cars, and bookings
2. **Class Diagrams**: Detailing the structure and relationships of all classes in the system
3. **Flowcharts**: Illustrating the execution flow of key algorithms
4. **Sequence Diagrams**: Showing the interaction between system components

To preview the diagram click on the tab of the file and click open preview to view the diagram.

 Driving Lessons Booking System

 Custom Hash Table Implementation
- **Date Added:** March 23, 2025
- **Description:** Implemented a generic CustomHashTable class within the DrivingLessonsBooking namespace that uses chaining for collision resolution.

### Features Added:
1. **Generic Implementation:**
   - Created a generic hash table that can store any key-value pair types using `TKey` and `TValue` type parameters
   - Uses a private nested `HashNode` class to store key-value pairs and maintain the linked list structure

2. **Core Functionality:**
   - **Constructor:** Initializes the hash table with a specified capacity
   - **Insert:** Adds or updates key-value pairs using chaining for collision handling
   - **Search:** Retrieves values by key with O(1) average case performance
   - **Delete:** Removes key-value pairs from the table
   - **DisplayAll:** Prints all stored values in the hash table

3. **Hashing Mechanism:**
   - Implements a simple hash function using `GetHashCode()` with modulo operation
   - Handles negative hash codes using `Math.Abs()`
   - Distributes keys across the table based on array size

4. **Collision Resolution:**
   - Uses chaining with linked lists to handle hash collisions
   - Maintains a linked list at each table index using the `Next` pointer in HashNode

### Technical Details:
- **Structure:** Array of HashNode references with linked list chaining
- **Time Complexity:**
  - Average case: O(1) for Insert, Search, and Delete
  - Worst case: O(n) when many keys hash to the same index
- **Space Complexity:** O(n) where n is the number of key-value pairs stored.

- Today **Date Added:** March 28, 2025
-  **Description:** Implemenented a unit test code that ensures the Booking constructor properly initializes its properties. If the test passes, the constructor is working correctly. If it fails, there is an issue with how the Booking class handles its properties.
- Today **Date Added:** March 29, 2025
-  **Description:** Updated the unit test code that ensures the Booking constructor properly initializes its properties.

-  ### Booking Linked List Tests
- **Date Added:** April 11, 2025
- **Description:** Created unit tests for the `CustomLinkedList<T>` class within the `DrivingLessonsBooking` namespace to validate its core functionality.

### Features Tested:
1. **Insert Method**: Ensures elements are added to the list in sorted order.
2. **Search Method**: Verifies that elements can be retrieved correctly if they exist in the list.
3. **Delete Method**: Confirms that an attempt to delete a non-existent element returns `false`.

### Technical Details:
- **Testing Framework:** Microsoft Visual Studio Test Framework (MSTest)
- **Namespace:** `DrivingLessonsBookingTests`
- **Responsible Developer:** Nnamdi
