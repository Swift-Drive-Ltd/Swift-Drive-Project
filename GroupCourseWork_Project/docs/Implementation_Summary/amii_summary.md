# Note # 
New Commits & Implementations ( As from 24 March) because of ssues with file structures, leading to merging conflict.

 # New files responsible for # 
1. CarLogic

## Intended Plan ##

### Initialisation ###

The CarLogic constructor initialises a custom hash table and a sorted linked list to store car objects.
It also loads car data from a database into these data structures.

NOTE : Still deciding whether to use SQLite or SQL Server Management Studio.

### Database Operations ###

LoadCarsFromDatabase: Reads car data from the database and populates the in-memory data structures.

AddCar: Inserts a new car into the database and updates the in-memory data structures.

ModifyCar: Updates an existing car's details in the database and the in-memory data structures.

DeleteCar: Removes a car from the database and the in-memory data structures.
In-Memory Data Management:

Uses a custom hash table (CustomHashTable<string, Car>) to store and retrieve cars by their ID.

Uses a sorted linked list (SortedLinkedList<Car>) to maintain cars in a sorted order.

### User Interaction ### 

Provides methods to add, modify, delete, and display cars, with appropriate console output for user feedback.

## Concepts to be used ## 

1. Custom Data Structures

CustomHashTable<string, Car>: A hash table implementation for efficient car ID lookups.
SortedLinkedList<Car>: A linked list that maintains cars in a sorted order.

 2. Database Interaction:

Uses SQL Sever Management System for database operations.

Executes SQL commands to perform CRUD (Create, Read, Update, Delete) operations on the Cars table.

 3. Object-Oriented Programming:

Encapsulation: The CarLogic class encapsulates all car-related logic and data management.
Abstraction: Provides a simplified interface for managing cars without exposing the underlying data structures.

4. Error Handling:

Checks for existing car IDs before adding a new car.

Provides console output for error messages and successful operations.

5. Resource Management:

Uses using statements to ensure proper disposal of database connections and commands.
This systematic approach ensures that car data is efficiently managed both in-memory and in the database, providing a robust solution for the driving lessons booking system.


### Next Step : Figure out for car.cs and other small details###