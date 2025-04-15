# Driving Lessons Booking System  

This project is entails a **Driving Lessons Booking System** implemented in **C#** and **SQLite** for data persistence. It includes custom data structures such as a **hash table** and **sorted linked list** to manage bookings efficiently.

---

## Setup Guide  

### **1. Prerequisites**  
Ensure you have the following installed on your machine:  
- **.NET SDK** (>= .NET 8.0) [Download](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)  
- **SQLite** (for database) [Guide](https://www.sqlitetutorial.net/download-install-sqlite/)  
- **Visual Studio Community Edition** (Recommended for C# development) [Download](https://visualstudio.microsoft.com/downloads/)

---

### **2️. Clone the Repository**  
```sh
git clone https://github.com/Swift-Drive-Ltd/Swift-Drive-Project.git
cd Swift-Drive-Project
```

---

### **3️. Setup SQLite Database**  
The system uses SQLite to store bookings. The query for database and tables creation is in the txt file attached with this assignment. Otherwise, just grab the _driving-lessons.db_ file and place it in the directory C:\sqlite\gui then confirm the correctness of the connection string in the files **BookingLogic.cs**, **CarLogic.cs**, **InstructorLogic.cs**, and **StudentLogic.cs**. The coeection string should look similar to:

```csharp
private string connectionString = "Data Source=C:\\sqlite\\gui\\driving-lessons.db;Version=3;";
```

---

### **4. Setup SQLite Database**  
The system uses SQLite to store bookings. The query for database and tables creation is in the txt file attached with this assignment. Otherwise, just grab the _driving-lessons.db_ file and place it in the directory C:\sqlite\gui then confirm the correctness of the connection string in the files **BookingLogic.cs**, **CarLogic.cs**, **InstructorLogic.cs**, and **StudentLogic.cs**. The coeection string should look similar to:

---

### **5. Run the Project**  
1. Open `DrivingLessonsBooking.sln` in Visual Studio  
2. Set `DrivingLessonsBooking` as the **Startup Project**  
3. Click **Run** ▶️ or press `F5`. In case of any issues check that the Nuget package (System.data.SQLite) is installed

---

### **6. Running Tests**  
This project uses **MSTest** for unit testing. To run the tests: Add reference from __DrivingLessonsBookingTests__ to __DrivingLessonsBooking__ project by selecting the test project then going to **Project** ▶️ **Add Project Reference** ▶️ **Seleect the Checkbox for the source code** and click add.

#### **Using Visual Studio**  
1. Open the **Test Explorer** (`Ctrl + E, T`)  
2. Click **Run All Tests**  

---

## Troubleshooting  

### **Database Connection Issues**  
- Ensure the database file exists in the specified location (`C:\sqlite\gui\driving-lessons.db`).  
- If missing, create the database and table manually.  

### **MSTest Not Finding Tests**  
- Ensure `Microsoft.NET.Test.Sdk` is installed:  
```sh
dotnet add package Microsoft.NET.Test.Sdk
dotnet add package MSTest.TestAdapter
dotnet add package MSTest.TestFramework
```

or 

```sh
> dotnet restore
```

on the Nuget packages terminal

---
