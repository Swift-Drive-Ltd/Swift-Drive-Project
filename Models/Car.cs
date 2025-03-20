namespace DrivingSchool.Models
{
    public class Car
    {
        public int CarId { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string RegistrationNumber { get; set; }

        // TODO: Add remaining properties and methods
    }
}
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