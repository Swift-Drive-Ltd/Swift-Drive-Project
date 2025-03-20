# 🚀 Project Progress Report  

---

## 🗓️ Day 1  

### ✅ Implemented:  
- 🏗️ **Initialized** basic structure for `Student` class with core properties  
- 🔧 **Created** skeleton for `SortedLinkedList<T>` generic class with `IComparable` constraint  

### ⏳ In Progress:  
- 📝 Implementing `Student` class methods according to design specification  
- 🔗 Developing the `Node<T>` structure for `SortedLinkedList<T>` implementation  

### 📌 Student Class  
✅ **Current State:**  
- Contains basic properties: `StudentId`, `Name`, and `Email`  

❌ **Pending:**  
- Implementing **constructor** and `ToString()` method  
- `StudentId` type mismatch (currently `int`, needs to be `string`)  

### 📌 SortedLinkedList<T> Class  
✅ **Current State:**  
- Only includes class declaration with generic type constraint for `IComparable<T>`  

❌ **Pending:**  
- Implement `Insert`, `Delete`, `Find`, and other methods  
- Define the `Node<T>` class  

### 🔜 Next Steps:  
1️⃣ Complete `Student` class implementation (constructor, `ToString()`)  
2️⃣ Implement `Node<T>` class for linked list  
3️⃣ Add `Insert`, `Delete`, and `Find` methods to `SortedLinkedList<T>`  
4️⃣ Develop `HashTable` implementation  
5️⃣ Implement entity classes (`Instructor`, `Car`, `Booking`)  
6️⃣ Develop `BookingManager` class  

---

## 🗓️ Day 2  

### ✅ Implemented:  
- 🏗️ **Completed full implementation of** `SortedLinkedList<T>` with core methods:  
  - 📌 `Insert`: Adds elements in sorted order  
  - ❌ `Delete`: Removes elements while handling predecessor/successor  
  - 🔍 `Find`: Searches for an element and returns it  
  - 📜 `Display`: Lists all elements  

- ✨ **Enhanced Student class** with:  
  - 🔹 Complete properties (`StudentId`, `Name`, `DateOfBirth`, `ContactNumber`, `Email`, `LicenseNumber`)  
  - 🏗️ Constructor with validation  
  - 🖋️ `ToString()` method for display  
  - 🔗 `IComparable<Student>` implementation for sorting  
  - ✅ Validation method ensuring data integrity  

### ⏳ In Progress:  
- 🧪 Writing initial test cases for `SortedLinkedList<T>` and `Student` classes  
- 🏗️ Planning `StudentLogic` class to use `SortedLinkedList<T>`  

### 📌 Technical Details  

#### 📜 SortedLinkedList<T> Class  
- ✅ Implemented a **private `Node<T>` inner class** for data storage  
- 🔄 `Insert()` method places elements in **ascending order** (`CompareTo`)  
- 🗑️ `Delete()` properly handles **head node removal & middle node deletion**  
- 🔍 `Find()` method traverses the list using **`Equals()`**  
- 📖 **XML documentation** added for all methods  

#### 📜 Student Class  
- 🔗 Implements `IComparable<Student>` for sorting  
- 🏗️ `CompareTo()` method prioritizes `StudentId`  
- ✅ Validation method checks:  
  - 📝 Required fields (`Name`, `Email`, `LicenseNumber`)  
  - ✉️ Valid email format (basic regex check)  
  - 🎂 Minimum age **(16 years)**  
- 🖋️ `ToString()` provides a **formatted display output**  

### 🔜 Next Steps:  
1️⃣ Create unit tests for `Student` and `SortedLinkedList<T>`  
2️⃣ Implement `StudentLogic` class  
3️⃣ Plan **database structure** for `Student` persistence  
4️⃣ Coordinate with **Amii** on `Booking` integration  

---

## 🗓️ Day 3  

### ✅ Implemented:  
- 🧪 **Developed unit test suite**:  
  - 🎯 `StudentTests`: Validates `Student` entity behavior  
  - 📌 `SortedLinkedListTests`: Ensures sorting & data operations  

- 🏗️ **Developed `StudentLogic` class** with:  
  - ➕ `AddStudent()`: Validates & adds students, **prevents duplicates**  
  - ✏️ `UpdateStudent()`: Modifies student data with validation  
  - 🗑️ `DeleteStudent()`: Removes students by ID  
  - 🔍 `FindStudentById()`: Retrieves specific student records  
  - 🔎 `FindStudentsByName()`: Supports **partial name matching**  

### ⏳ In Progress:  
- 🚀 Enhancing `StudentLogic` with **optimized search capabilities**  
- 🔗 Planning `HashTable` for next entity classes  
- 🗄️ **Designing database integration strategy**  

### 📌 Technical Details  

#### 🧪 Unit Testing  
- ✅ **MSTest framework** used for unit testing  
- 📝 Test cases include:  
  - 🎂 Student validation (age requirements, email format)  
  - 🔄 Student sorting & string representation  
  - 🔗 `SortedLinkedList<T>` **insertion order verification**  
  - 🗑️ `SortedLinkedList<T>` **deletion & retrieval**  
  - 🛑 **Edge cases** (empty lists, non-existent elements)  

#### 📜 StudentLogic Class  
- 🏗️ Implements **repository pattern** for student management  
- 🔗 Uses `SortedLinkedList<T>` as **core data structure**  
- 📝 Provides **CRUD operations** with validation  
- 🔍 Specialized search functionality (`FindById`, `FindByName`)  
- 🚨 Handles **duplicate detection & validation errors**  

### 🔜 Next Steps:  
1️⃣ Implement `IEnumerable<T>` in `SortedLinkedList<T>` for iteration  
2️⃣ Begin `HashTable` implementation for **O(1) lookups**  
3️⃣ Create `Instructor` and `Car` entity classes  
4️⃣ Design `BookingManager` with **entity relationships**  
5️⃣ Add **exception handling** throughout the system  

---

📌 **Summary:** Progressing towards a structured & optimized system with well-validated entities, linked lists, and planned database integration. 🚀
