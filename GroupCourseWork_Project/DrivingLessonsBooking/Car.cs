namespace DrivingLessonsBooking
{
    public class Car : IComparable<Car>
    {
        public string CarID { get; set; }
        public string Model { get; set; }
        public string Type { get; set; }  // Manual or Automatic
        public string LicensePlate { get; set; }

        public Car() { } // Database requirement

        public Car(string id, string model, string type, string licensePlate)
        {
            CarID = id;
            Model = model;
            Type = type;
            LicensePlate = licensePlate;
        }

        public int CompareTo(Car? other)
        {
            return Model.CompareTo(other?.Model);
        }

        public override string ToString()
        {
            return $"Car ID: {CarID}, Model: {Model}, Type: {Type}, License: {LicensePlate}";
        }
    }
}
