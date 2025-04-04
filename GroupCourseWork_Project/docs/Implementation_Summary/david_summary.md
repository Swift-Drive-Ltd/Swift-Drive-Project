# Booking Logic - Day 1 Commit

# Initial Setup
Created the BookingLogic class to handle booking operations.

Initialized key properties such as CustomHashTable<string, Booking> for storing bookings and SortedLinkedList<Booking> to maintain sorted order by booking date.

Set up the database connection string for SQLite integration.

# Features Added
LoadBookingsFromDatabase Method:

This method connects to the SQLite database and loads booking details from the Bookings table.

Each booking is added to both the hash table and the sorted linked list to maintain booking data in memory.

# Next Steps
Implement methods for creating, updating, and deleting bookings.

Improve database integration for additional functionality such as booking validation and updates

# Booking Logic - Day 2 Commit

### **Enhancements in Booking Management**
- Implemented the `AddBooking` method:
  - Prevents duplicate booking IDs.
  - Inserts new bookings into both the hash table and sorted linked list.
  - Saves booking details into the SQLite database.

- Implemented the `GetBooking` method:
  - Retrieves a booking by its ID from the hash table.

- Implemented the `ModifyBooking` method:
  - Updates existing booking details.
  - Ensures modifications reflect in both data structures and the database.

### **Next Steps**
- Implement method for deleting bookings.
- Improve error handling for database operations.
- Optimize database queries for performance.

# Booking Logic - Updated Commit  

## Enhancements in Booking Management  

### Implemented the `AddBooking` method:  
- Prevents duplicate booking IDs.  
- Inserts new bookings into both the hash table and sorted linked list.  
- Saves booking details into the SQLite database.  

### Implemented the `GetBooking` method:  
- Retrieves a booking by its ID from the hash table.  

### Implemented the `ModifyBooking` method:  
- Updates existing booking details.  
- Ensures modifications reflect in both data structures and the database.  

### Implemented the `DeleteBooking` method:  
- Removes a booking from the hash table and sorted linked list.  
- Deletes the booking from the SQLite database.  

### Implemented the `DisplayBookingsSorted` method:  
- Displays bookings in sorted order by date.  

# Student Logic - Initial Commit
## Implemented Student Management System
## Created the StudentLogic class to manage student records.

# Initialized data structures:

CustomHashTable<string, Student> for quick student lookup.

SortedLinkedList<Student> for maintaining students in sorted order.

### Integrated SQLite database for persistent student data storage.

### Implemented LoadStudentsFromDatabase Method
Establishes a connection to the SQLite database.

Retrieves all student records from the Students table.

Populates the hash table and sorted linked list with student data.

## **Next Steps
Implement methods to add, update, and delete student records.

## Improve error handling for database operations.

## Optimize database queries for better performance.

Student Logic - Database Integration
Added LoadStudentsFromDatabase method:

### Connects to the SQLite database.

Fetches all records from the Students table.

Instantiates Student objects and inserts them into:

CustomHashTable<string, Student> for fast access.

SortedLinkedList<Student> for ordered display.

## Next Steps
Add functionality to create, update, and delete student records.

Ensure error handling and data validation for robustness.

