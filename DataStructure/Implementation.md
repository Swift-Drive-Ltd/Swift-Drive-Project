
## Updates (16/3/2025)

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
