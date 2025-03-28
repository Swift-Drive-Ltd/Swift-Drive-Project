namespace DrivingLessonsBooking
{
    public class CarLogic
    {
        private CustomHashTable<string, Car> cars;
        private SortedLinkedList<Car> sortedCars;
        //Figure out code for connection string (SQL Database Management Studio) 
        private string connectionString =

        public CarLogic(int capacity)
        {
            // Initialize the hash table and sorted linked list
            cars = new CustomHashTable<string, Car>(capacity);
            sortedCars = new SortedLinkedList<Car>();

            // Load cars from the database into the in-memory data structures
            LoadCarsFromDatabase();
        }

        // Placeholder for LoadCarsFromDatabase method to be implemented later
        private void LoadCarsFromDatabase()
        {
            // Implementation will be added in Day 2
        }
    }
}