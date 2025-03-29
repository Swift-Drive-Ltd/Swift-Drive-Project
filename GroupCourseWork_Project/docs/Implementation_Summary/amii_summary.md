# Note # 
New Commits & Implementations ( As from 24 March) because of ssues with file structures, leading to merging conflict.

 # New files responsible for # 

 1. Car.cs 

Summary of Car Class Implementation


The Car class is a foundational component of the DrivingLessonsBooking namespace. It represents a car entity with properties and methods to manage its data and behavior.

Properties:

CarID: A unique identifier for the car.
Model: The model name of the car.
Type: Indicates whether the car is manual or automatic.
LicensePlate: The car's license plate number.


Constructors:

Default Constructor: A parameterless constructor required for database operations.
Parameterized Constructor: Initializes a car object with specific values for CarID, Model, Type, and LicensePlate.


Methods:

CompareTo(Car? other):
Implements the IComparable<Car> interface.
Compares cars based on their Model property for sorting purposes.
ToString():
Overrides the default ToString method to provide a formatted string representation of the car's details.


Purpose:
The Car class serves as a data model for car objects, encapsulating their attributes and providing methods for comparison and display.
It is designed to integrate seamlessly with other components like CarLogic for managing car data in-memory and in the database.


2. CarLogic

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

Day 1 : 

This code sets up the foundation for the CarLogic class, including its fields and constructor, while leaving the LoadCarsFromDatabase method as a placeholder for Day 2.

Pending : Connection string for Database

Day 2 :

Implemented the LoadCarsFromDatabase method to connect to the  database and retrieve car data.


Populated the CustomHashTable and SortedLinkedList with the retrieved car objects.

Ensured the CarLogic class can initialize its in-memory data structures from persistent storage.

Pending : Use SQL Studio Management instead of SQLite