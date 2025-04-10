using System.Data.SQLite;

namespace DrivingLessonsBooking
{
    public class CarLogic
    {
        private CustomHashTable<string, Car> cars;
        private SortedLinkedList<Car> sortedCars;
        private string connectionString = "Data Source=C:\\sqlite\\gui\\driving-lessons.db;Version=3;";

        public CarLogic(int capacity)
        {
            // Initialise the hash table and sorted linked list
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
         public Car GetCar(string id)
        {
            return cars.Search(id);
        }

        public void ModifyCar(string id, string model, string type, string licensePlate)
        {
            Car existingCar = cars.Search(id);
            if (existingCar == null)
            {
                Console.WriteLine("Car not found.");
                return;
            }

            sortedCars.Delete(existingCar);

            existingCar.Model = model;
            existingCar.Type = type;
            existingCar.LicensePlate = licensePlate;

            sortedCars.Insert(existingCar);

            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand(
                    "UPDATE Cars SET Model = @model, Type = @type, LicensePlate = @license WHERE CarID = @id",
                    conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@model", model);
                    cmd.Parameters.AddWithValue("@type", type);
                    cmd.Parameters.AddWithValue("@license", licensePlate);
                    cmd.ExecuteNonQuery();
                }
            }

            Console.WriteLine("Car updated successfully.");
        }
        public bool DeleteCar(string id)
        {
            Car toRemove = cars.Search(id);
            if (toRemove == null)
            {
                Console.WriteLine("Car not found.");
                return false;
            }

            cars.Delete(id);
            sortedCars.Delete(toRemove);

            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("DELETE FROM Cars WHERE CarID = @id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }

            Console.WriteLine("Car deleted successfully.");

            return true;
         }

            public void DisplayCarsSorted()
        {
            sortedCars.Display();
        }
       

    }

}
    
