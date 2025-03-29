## Commit 1: Add Basic Structure and Properties for Booking Class

### Implemented:
- Created `Booking.cs` with the following properties:
  - `BookingID`: Unique identifier for the booking
  - `StudentID`: Identifier for the student
  - `InstructorID`: Identifier for the instructor
  - `LessonDate`: Date and time of the lesson
  - `CarID`: Identifier for the car used in the lesson

## Commit 2: Implement CompareTo and ToString Methods for Booking Class

### Implemented:
- Added `IComparable<Booking>` interface implementation
- Implemented `CompareTo` method to compare bookings by `LessonDate`
- Implemented `ToString` method to provide a string representation of the booking
- Created `CustomLinkedList<T>` class with generic type constraint requiring `IComparable<T>`
- Implemented private `Node` inner class to store data and references:
  - `Data` property to store the actual value
  - `Next` property as a reference to the next node
- Added default constructor
- Applied nullable reference types for better type safety

### Technical Details:
- The generic constraint ensures all stored elements can be compared
- The private Node class encapsulates the linked list implementation details
- Null safety is handled through nullable reference types (?)

### Next Steps:
- Implement core operations (Insert, Search)


## Commit 2: Implement Insert and Search Operations

### Implemented:
- Added `Insert` method that maintains sorted order based on the natural ordering of elements
  - Handles empty list case
  - Handles insertion at the beginning
  - Inserts in the correct position to maintain sorted order
- Added `Search` method to find elements in the list
  - Returns the found element or default value if not found
  - Uses equality comparison for exact matches

### Technical Details:
- Insert operation has O(n) time complexity in worst case
- Insert maintains elements in ascending order based on CompareTo
- Search traverses the list linearly (O(n) time complexity)
- Proper null handling is implemented for robustness

### Next Steps:
- Implement Delete operation
- Add utility methods for display and list management


