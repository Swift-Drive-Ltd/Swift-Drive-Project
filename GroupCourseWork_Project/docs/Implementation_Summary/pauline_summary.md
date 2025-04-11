## Day2
# Summary of Implemented Features in Instructor.cs

## Namespace
**DrivingLessonsBooking**

## Class
**Instructor**

### Implements
`IComparable<Instructor>`

## Properties
- **InstructorID**: `string` - Unique identifier for the instructor
- **Name**: `string` - Name of the instructor
- **Phone**: `string` - Phone number of the instructor

## Constructors
- **Default constructor**: Initializes a new instance of the Instructor class with default values
- **Parameterized constructor**: Initializes a new instance of the Instructor class with specified InstructorID, Name, and Phone

## Methods
- **CompareTo(Instructor? other)**: Compares instructors by their names
- **ToString()**: Returns a string representation of the instructor in the format `InstructorID: {InstructorID}, Name: {Name}, Phone: {Phone}`




## Day 3
# Summary of Implemented Features in InstructorLogic.cs

# Namespace:
 DrivingLessonsBooking

# Class:
 InstructorLogic

- Manages instructor data and interactions with the SQLite database.

# Fields:

- CustomHashTable<string, Instructor> instructors: A custom hash table to store instructors with their IDs as keys.

- SortedLinkedList<Instructor> sortedInstructors: A sorted linked list to maintain instructors in a sorted order.

- string connectionString: Connection string for the SQLite database.

# Constructor:

- Initializes the custom hash table and sorted linked list with a specified capacity.

- Loads existing instructors from the SQLite database into memory.

# Methods:
|LoadInstructorsFromDatabase():

- Connects to the SQLite database.
- Retrieves all instructors from the Instructors table.
- Populates the hash table and sorted linked list with the retrieved data.



## Day 4
# Summary of Implemented Features in InstructorLogic.cs

# Namespace:
|DrivingLessonsBooking

# Class:
|InstructorLogic

- Manages instructor data and interactions with the SQLite database.

# Fields:
- CustomHashTable<string, Instructor> instructors: A custom hash table to store instructors with their IDs as keys.

- SortedLinkedList<Instructor> sortedInstructors: A sorted linked list to maintain instructors in a sorted order. 

- string connectionString: Connection string for the SQLite database.

# Constructor:
- Initializes the custom hash table and sorted linked list with a specified capacity.

- Loads existing instructors from the SQLite database into memory.

# Methods:
|LoadInstructorsFromDatabase():

- Connects to the SQLite database.
- Retrieves all instructors from the Instructors table.
- Populates the hash table and sorted linked list with the retrieved data.

|AddInstructor(Instructor instructor):

- Checks if an instructor with the same ID already exists in the hash table.
- Inserts the new instructor into the hash table and sorted linked list.
- Opens a connection to the SQLite database.
- Executes a parameterized SQL query to insert the new instructor into the database, preventing SQL injection.
- Notifies the user upon successful addition of the instructor.


## Day 5
# Summary of Implemented Features in InstructorLogic.cs
# Namespace:
|DrivingLessonsBooking

# Class:
|InstructorLogic

- Manages instructor data and interactions with the SQLite database.

# Methods:
|GetInstructor(string id):

- Searches for an instructor in the hash table using their ID.
- Returns the instructor object if found.

# ModifyInstructor(string id, string name, string phone):

- Searches for an instructor in the hash table using their ID.
- If the instructor is not found, notifies the user and exits the method.
- Removes the instructor from the sorted linked list to update their details.
- Updates the instructor's name and phone number.
- Reinserts the updated instructor into the sorted linked list.
- Opens a connection to the SQLite database.
- Executes a parameterized SQL query to update the instructor's details in the database, preventing SQL injection.
- Notifies the user upon successful update of the instructor.