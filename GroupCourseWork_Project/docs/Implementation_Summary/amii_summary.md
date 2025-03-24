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

## Next goal : Refine concepts to be used ### 