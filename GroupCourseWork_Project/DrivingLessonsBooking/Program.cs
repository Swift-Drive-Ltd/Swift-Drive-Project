// Responsible: Mathews
namespace DrivingLessonsBooking
{
    class Program
    {
        static void Main()
        {

            BookingLogic bookingSystem = new BookingLogic(10);
            StudentLogic studentSystem = new StudentLogic(10);
            InstructorLogic instructorSystem = new InstructorLogic(10);
            CarLogic carSystem = new CarLogic(10);

            bool exit = false;

            // Load data from databsase
            bookingSystem.LoadBookingsFromDatabase();
            carSystem.LoadCarsFromDatabase();
            instructorSystem.LoadInstructorsFromDatabase();
            studentSystem.LoadStudentsFromDatabase();

            while (!exit)
            {
                Console.WriteLine("\n===== Driving Lesson Booking System =====");
                Console.WriteLine("1. Manage Bookings");
                Console.WriteLine("2. Manage Students");
                Console.WriteLine("3. Manage Instructors");
                Console.WriteLine("4. Manage Cars");
                Console.WriteLine("5. Exit");
                Console.Write("Enter your choice: \n");

                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ManageBookings(bookingSystem, studentSystem, instructorSystem, carSystem);
                        break;
                    case "2":
                        ManageStudents(studentSystem);
                        break;
                    case "3":
                        ManageInstructors(instructorSystem);
                        break;
                    case "4":
                        ManageCars(carSystem);
                        break;
                    case "5":
                        exit = true;
                        Console.WriteLine("\nExiting... Thank you!\n");
                        break;
                    default:
                        Console.WriteLine("\nInvalid choice. Please try again.\n");
                        break;
                }
            }
        }

        // ***************************************** MANAGE BOOKINGS *****************************************
        static void ManageBookings(BookingLogic system, StudentLogic studentSystem, InstructorLogic instructorSystem, CarLogic carSystem)
        {
            bool back = false;
            while (!back)
            {
                Console.WriteLine("\n*** Manage Bookings ***");
                Console.WriteLine("1. Add Booking");
                Console.WriteLine("2. Search Booking");
                Console.WriteLine("3. Delete Booking");
                Console.WriteLine("4. Display All Bookings (Sorted)");
                Console.WriteLine("5. Back to Main Menu");
                Console.Write("Enter your choice: \n");

                string? choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        try
                        {
                            Console.Write("Enter Booking ID: ");
                            string id = Console.ReadLine();

                            Console.Write("Enter Student ID: ");
                            string studentId = Console.ReadLine();
                            Student student = studentSystem.GetStudent(studentId);
                            if (student == null)
                            {
                                Console.WriteLine("\nError: Student ID does not exist. Please add the student first.\n");
                                return;
                            }

                            Console.Write("Enter Instructor ID: ");
                            string instructorId = Console.ReadLine();
                            Instructor instructor = instructorSystem.GetInstructor(instructorId);
                            if (instructor == null)
                            {
                                Console.WriteLine("\nError: Instructor ID does not exist. Please add the instructor first.\n");
                                return;
                            }

                            Console.Write("Enter Lesson Date (YYYY-MM-DD): ");
                            if (!DateTime.TryParse(Console.ReadLine(), out DateTime date))
                            {
                                Console.WriteLine("\nError: Invalid date format.\n");
                                return;
                            }

                            Console.Write("Enter Car ID: ");
                            string carId = Console.ReadLine();
                            Car car = carSystem.GetCar(carId);
                            if (car == null)
                            {
                                Console.WriteLine("\nError: Car ID does not exist. Please add the car first.\n");
                                return;
                            }

                            system.AddBooking(new Booking(id, student.Name, instructor.Name, date, car.Model));
                            Console.WriteLine("\nBooking successfully added.\n");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Unexpected Error: {ex.Message}");
                        }
                        break;

                    case "2":
                        Console.Write("Enter Booking ID to search: \n");
                        Console.WriteLine(system.GetBooking(Console.ReadLine())?.ToString() ?? "Booking not found.\n");
                        break;
                    case "3":
                        Console.Write("Enter Booking ID to delete: ");
                        Console.WriteLine(system.DeleteBooking(Console.ReadLine()) ? "Booking deleted." : "Booking not found.\n");
                        break;
                    case "4":
                        system.DisplayBookingsSorted();
                        break;
                    case "5":
                        back = true;
                        break;
                }
            }
        }

        // ***************************************** MANAGE STUDENTS *****************************************
        static void ManageStudents(StudentLogic system)
        {
            bool back = false;
            while (!back)
            {
                Console.WriteLine("\n*** Manage Students ***");
                Console.WriteLine("1. Add Student");
                Console.WriteLine("2. Search Student");
                Console.WriteLine("3. Delete Student");
                Console.WriteLine("4. Display All Students");
                Console.WriteLine("5. Back to Main Menu");
                Console.Write("Enter your choice: \n");

                string? choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Console.Write("Enter Student ID: ");
                        string? id = Console.ReadLine();
                        Console.Write("Enter Name: ");
                        string? name = Console.ReadLine();
                        Console.Write("Enter Email: ");
                        string? email = Console.ReadLine();
                        system.AddStudent(new Student(id, name, email));
                        break;
                    case "2":
                        Console.Write("Enter Student ID to search: \n");
                        Console.WriteLine(system.GetStudent(Console.ReadLine())?.ToString() ?? "Student not found.");
                        break;
                    case "3":
                        Console.Write("Enter Student ID to delete: \n");
                        Console.WriteLine(system.DeleteStudent(Console.ReadLine()) ? "Student deleted." : "Student not found.");
                        break;
                    case "4":
                        system.DisplayStudentsSorted();
                        break;
                    case "5":
                        back = true;
                        break;
                }
            }
        }

        // ***************************************** MANAGE INSTRUCTORS *****************************************
        static void ManageInstructors(InstructorLogic system)
        {
            bool back = false;
            while (!back)
            {
                Console.WriteLine("\n*** Manage Instructors ***");
                Console.WriteLine("1. Add Instructor");
                Console.WriteLine("2. Search Instructor");
                Console.WriteLine("3. Delete Instructor");
                Console.WriteLine("4. Display All Instructors");
                Console.WriteLine("5. Back to Main Menu");
                Console.Write("Enter your choice: \n");

                string? choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Console.Write("Enter Instructor ID: ");
                        string? id = Console.ReadLine();
                        Console.Write("Enter Name: ");
                        string? name = Console.ReadLine();
                        Console.Write("Enter Phone Number: ");
                        string? phone = Console.ReadLine();

                        system.AddInstructor(new Instructor(id, name, phone));
                        break;
                    case "2":
                        Console.Write("Enter Instructor ID to search: \n");
                        Console.WriteLine(system.GetInstructor(Console.ReadLine())?.ToString() ?? "Instructor not found.");
                        break;
                    case "3":
                        Console.Write("Enter Instructor ID to delete: \n");
                        Console.WriteLine(system.DeleteInstructor(Console.ReadLine()) ? "Instructor deleted." : "Instructor not found.");
                        break;
                    case "4":
                        system.DisplayInstructorsSorted();
                        break;
                    case "5":
                        back = true;
                        break;
                }
            }
        }

        // ***************************************** MANAGE CARS *****************************************
        static void ManageCars(CarLogic system)
        {
            bool back = false;
            while (!back)
            {
                Console.WriteLine("\n=== Manage Cars ===");
                Console.WriteLine("1. Add Car");
                Console.WriteLine("2. Search Car");
                Console.WriteLine("3. Delete Car");
                Console.WriteLine("4. Display All Cars");
                Console.WriteLine("5. Back to Main Menu");
                Console.Write("Enter your choice: \n");

                string? choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Console.Write("Enter Car ID: ");
                        string? id = Console.ReadLine();
                        Console.Write("Enter Model: ");
                        string? model = Console.ReadLine();
                        Console.Write("Enter Type (Manual/Automatic): ");
                        string? type = Console.ReadLine();
                        Console.Write("Enter License Plate: ");
                        string? licensePlate = Console.ReadLine();

                        system.AddCar(new Car(id, model, type, licensePlate));
                        Console.WriteLine("Car added.");
                        break;
                    case "2":
                        Console.Write("Enter Car ID to search: \n");
                        Console.WriteLine(system.GetCar(Console.ReadLine())?.ToString() ?? "Car not found.");
                        break;
                    case "3":
                        Console.Write("Enter Car ID to delete: ");
                        Console.WriteLine(system.DeleteCar(Console.ReadLine()) ? "Car deleted." : "Car not found.");
                        break;
                    case "4":
                        system.DisplayCarsSorted();
                        break;
                    case "5":
                        back = true;
                        break;
                }
            }
        }
    }
}

