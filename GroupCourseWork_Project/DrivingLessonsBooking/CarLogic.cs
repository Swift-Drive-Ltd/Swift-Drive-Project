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
         public void AddCar(Car car)
        {
            if (cars.Search(car.CarID) != null)
            {
                Console.WriteLine("Car ID already exists.");
                return;
            }

            cars.Insert(car.CarID, car);
            sortedCars.Insert(car);

            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand(
                    "INSERT INTO Cars (CarID, Model, Type, LicensePlate) VALUES (@id, @model, @type, @license)",
                    conn))
                {
                    cmd.Parameters.AddWithValue("@id", car.CarID);
                    cmd.Parameters.AddWithValue("@model", car.Model);
                    cmd.Parameters.AddWithValue("@type", car.Type);
                    cmd.Parameters.AddWithValue("@license", car.LicensePlate);
                    cmd.ExecuteNonQuery();
                }
            }

            Console.WriteLine("Car successfully added.");
        }
    }
}
    
