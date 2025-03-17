
#### Updates (16/3/2025)

### Implemented:
- Initialized basic structure for Student class with core properties
- Created skeleton for SortedLinkedList generic class with IComparable constraint

### In Progress:
- Implementing the Student class methods according to design specification
- Developing the Node structure for the SortedLinkedList implementation


# Student Class
Currently has basic properties: StudentId, Name, and Email
Still needs implementation of constructor and ToString() method as defined in the design document
The current implementation uses int for StudentId while the design document specifies string

# SortedLinkedList Class
Only contains the class declaration with a generic type constraint for IComparable<T>
Missing implementation of all methods described in the design document (Insert, Delete, Find, etc.)
No node class has been implemented yet

### Next Steps:
1. Complete Student class implementation (constructor, ToString())
2. Implement Node<T> class for linked list
3. Add Insert, Delete, Find methods to SortedLinkedList
4. Create HashTable implementation
5. Implement remaining entity classes (Instructor, Car, Booking)
6. Develop BookingManager class





##### Updates (17/3/2025)

### Implemented:
- Completed full implementation of SortedLinkedList<T> with all core methods:
  - Insert: Adds elements in sorted order
  - Delete: Removes elements with proper predecessor/successor handling
  - Find: Locates an element and returns it
  - Display: Shows all elements in the list
- Enhanced Student class with:
  - Complete properties (StudentId, Name, DateOfBirth, ContactNumber, Email, LicenseNumber)
  - Constructor with validation
  - ToString method for display
  - IComparable implementation for use with SortedLinkedList
  - Validation method to ensure data integrity

### In Progress:
- Setting up initial test cases for SortedLinkedList and Student classes
- Planning implementation of StudentLogic class that will use the SortedLinkedList

### Technical Details:

# SortedLinkedList Class
- Implemented a private Node inner class to store data and next references
- Insert method places elements in ascending order based on CompareTo
- Delete method properly handles head node deletion and middle node removal
- Find method traverses the list and uses Equals for comparison
- All methods include XML documentation

# Student Class
- Implements IComparable<Student> to enable sorting in the list
- CompareTo method uses StudentId as the primary comparison field
- Added validation method to check for:
  - Required fields (Name, Email, LicenseNumber)
  - Valid email format (basic check)
  - Minimum age requirement (16 years)
- ToString provides a formatted string representation for display

### Next Steps:
1. Create initial unit tests for Student and SortedLinkedList
2. Begin implementation of StudentLogic class
3. Plan database structure for Student persistence
4. Coordinate with Amii on Booking integration requirements