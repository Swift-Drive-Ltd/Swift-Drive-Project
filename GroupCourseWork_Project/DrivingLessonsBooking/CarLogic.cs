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
            //Fix connection string, instead of SQLite
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("SELECT * FROM Cars", conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Car car = new Car(
                            reader["CarID"].ToString(),
                            reader["Model"].ToString(),
                            reader["Type"].ToString(),
                            reader["LicensePlate"].ToString()
                        );

                        cars.Insert(car.CarID, car);
                        sortedCars.Insert(car);
                    }
                }
            }
        }
    }
}