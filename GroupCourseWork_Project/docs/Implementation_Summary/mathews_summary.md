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