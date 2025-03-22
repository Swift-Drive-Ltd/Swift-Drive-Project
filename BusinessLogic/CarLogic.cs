using System;
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
            cars = new CustomHashTable<string, Car>(capacity);
            sortedCars = new SortedLinkedList<Car>();
            LoadCarsFromDatabase();
        }

        public void LoadCarsFromDatabase()
        {
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
