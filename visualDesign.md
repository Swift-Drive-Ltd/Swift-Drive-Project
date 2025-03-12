# Driving Lessons Booking System - Simplified Visual Documentation

## 1. Core Entities Relationship

```mermaid
erDiagram
    STUDENT ||--o{ BOOKING : books
    INSTRUCTOR ||--o{ BOOKING : conducts
    CAR ||--o{ BOOKING : "is used in"
    
    STUDENT {
        string studentID PK
        string name
        string email
    }
    
    INSTRUCTOR {
        string instructorID PK
        string name
        string phone
    }
    
    CAR {
        string carID PK
        string model
        string type
        string licensePlate
    }
    
    BOOKING {
        string bookingID PK
        string studentID FK
        string instructorID FK
        string carID FK
        datetime lessonDate
    }
```
## 2. Logical Model (System Components)

```mermaid
flowchart TB
    subgraph Entities
        Student["Student Entity"]
        Instructor["Instructor Entity"]
        Car["Car Entity"]
        Booking["Booking Entity"]
    end
    
    subgraph "Data Structures"
        HT["Hash Table\n(Fast ID Lookups)"]
        SLL["Sorted Linked List\n(Date-ordered Access)"]
    end
    
    subgraph "Management Layer"
        BM["Booking Manager"]
    end
    
    Student --> Booking
    Instructor --> Booking
    Car --> Booking
    
    Booking --> HT
    Booking --> SLL
    
    HT --> BM
    SLL --> BM
```


## 3. Physical Model (Class Diagram - Simplified)

```mermaid
classDiagram
    class Student {
        -string studentID
        -string name
        -string email
        +Student(id, name, email)
        +ToString()
    }
    
    class Instructor {
        -string instructorID
        -string name
        -string phone
        +Instructor(id, name, phone)
        +ToString()
    }
    
    class Car {
        -string carID
        -string model
        -string type
        -string licensePlate
        +Car(id, model, type, license)
        +ToString()
    }
    
    class Booking {
        -string bookingID
        -string studentID
        -string instructorID
        -string carID
        -DateTime lessonDate
        +Booking(bookingID, studentID, instructorID, carID, date)
        +ToString()
    }
    
    class BookingManager {
        -HashTable bookingsById
        -SortedLinkedList bookingsByDate
        +AddBooking()
        +RemoveBooking()
        +GetBookingById()
        +GetBookingsByDateRange()
    }
    
    Student "1" -- "0..*" Booking
    Instructor "1" -- "0..*" Booking
    Car "1" -- "0..*" Booking
    BookingManager o-- Booking
```

## 4. Complete UML Class Diagram (Detailed)

```mermaid
classDiagram
    %% Entity Classes
    class Student {
        -string studentID
        -string name
        -string email
        +Student(string id, string name, string email)
        +string ToString()
    }
    
    class Instructor {
        -string instructorID
        -string name
        -string phone
        +Instructor(string id, string name, string phone)
        +string ToString()
    }
    
    class Car {
        -string carID
        -string model
        -string type
        -string licensePlate
        +Car(string id, string model, string type, string license)
        +string ToString()
    }
    
    class Booking {
        -string bookingID
        -string studentID
        -string instructorID
        -string carID
        -DateTime lessonDate
        +Booking(string bookingID, string studentID, string instructorID, string carID, DateTime date)
        +string ToString()
    }
    
    %% Data Structure Classes
    class HashTable~K,V~ {
        -LinkedList~KeyValuePair~K,V~~[] buckets
        -int capacity
        -int size
        -float loadFactor
        +HashTable(int initialCapacity)
        -int GetHashCode(K key)
        +void Put(K key, V value)
        +V Get(K key)
        +bool Remove(K key)
        -void Resize(int newCapacity)
        +V[] GetAllValues()
    }
    
    class Node~T~ {
        -T data
        -Node~T~ next
        +Node(T data)
    }
    
    class SortedLinkedList~T~ {
        -Node~T~ head
        -int size
        -Function~T,T,int~ comparator
        +SortedLinkedList(Function~T,T,int~ comparator)
        +void Insert(T item)
        +bool Remove(T item)
        +bool Contains(T item)
        +T[] GetAllItems()
    }
    
    class KeyValuePair~K,V~ {
        +K Key
        +V Value
        +KeyValuePair(K key, V value)
    }
    
    %% Manager Class
    class BookingManager {
        -HashTable~string,Booking~ bookingsById
        -SortedLinkedList~Booking~ bookingsByDate
        +BookingManager()
        -int CompareBookingsByDate(Booking booking1, Booking booking2)
        +bool AddBooking(Booking booking)
        +bool RemoveBooking(string bookingId)
        +Booking GetBookingById(string bookingId)
        +Booking[] GetBookingsByDateRange(DateTime startDate, DateTime endDate)
    }
    
    %% Relationships
    Student "1" -- "0..*" Booking : is booked by
    Instructor "1" -- "0..*" Booking : conducts
    Car "1" -- "0..*" Booking : is used in
    
    BookingManager o-- HashTable : contains
    BookingManager o-- SortedLinkedList : contains
    SortedLinkedList o-- Node : contains
    HashTable ..> KeyValuePair : uses
    BookingManager ..> Booking : manages
```



## 5. Data Structure Performance Comparison

```mermaid
graph LR
    subgraph "Hash Table Operations"
        HT_INS["Insert: O(1) avg"]
        HT_GET["Get: O(1) avg"]
        HT_REM["Remove: O(1) avg"]
        HT_RES["Resize: O(n)"]
    end
    
    subgraph "Sorted Linked List Operations"
        SLL_INS["Insert: O(n)"]
        SLL_REM["Remove: O(n)"]
        SLL_SRCH["Search: O(n)"]
        SLL_RNG["Range Query: O(k)"]
    end
    
    subgraph "Combined Approach Benefits"
        FAST_ID["Fast ID Lookups"]
        ORD_ACC["Ordered Access"]
        RNG_QRY["Efficient Range Queries"]
        SCAL["Scalable Solution"]
    end
    
    HT_INS & HT_GET & HT_REM --> FAST_ID
    SLL_INS & SLL_SRCH --> ORD_ACC
    SLL_RNG --> RNG_QRY
    HT_RES & SLL_INS --> SCAL
    
    style FAST_ID fill:#bfb,stroke:#333,stroke-width:2px
    style ORD_ACC fill:#bfb,stroke:#333,stroke-width:2px
    style RNG_QRY fill:#bfb,stroke:#333,stroke-width:2px
    style SCAL fill:#bfb,stroke:#333,stroke-width:2px
```

## 6. BookingManager Method Execution Flow

```mermaid
flowchart TD
    subgraph "AddBooking Method"
        ADD_START([Start]) --> CHECK_EXISTS{Booking exists?}
        CHECK_EXISTS -- Yes --> ADD_RET_F[Return False]
        CHECK_EXISTS -- No --> HT_PUT[Add to Hash Table]
        HT_PUT --> SLL_INS[Insert into Sorted Linked List]
        SLL_INS --> ADD_RET_T[Return True]
        ADD_RET_F & ADD_RET_T --> ADD_END([End])
    end
    
    subgraph "GetBookingById Method"
        GET_START([Start]) --> HT_GET[Query Hash Table]
        HT_GET --> GET_RET[Return Booking Object]
        GET_RET --> GET_END([End])
    end
    
    subgraph "GetBookingsByDateRange Method"
        RANGE_START([Start]) --> SLL_ALL[Get All Items from SLL]
        SLL_ALL --> FILTER[Filter by Date Range]
        FILTER --> RANGE_RET[Return Filtered Array]
        RANGE_RET --> RANGE_END([End])
    end
    
    subgraph "RemoveBooking Method"
        REM_START([Start]) --> CHECK_REM{Booking exists?}
        CHECK_REM -- No --> REM_RET_F[Return False]
        CHECK_REM -- Yes --> HT_REM[Remove from Hash Table]
        HT_REM --> SLL_REM[Remove from Sorted Linked List]
        SLL_REM --> REM_RET_T[Return True]
        REM_RET_F & REM_RET_T --> REM_END([End])
    end
    
    style ADD_START fill:#bbf,stroke:#333,stroke-width:2px
    style GET_START fill:#bbf,stroke:#333,stroke-width:2px
    style RANGE_START fill:#bbf,stroke:#333,stroke-width:2px
    style REM_START fill:#bbf,stroke:#333,stroke-width:2px
```

## 7. Hash Table Insertion and Collision Handling

```mermaid
flowchart TD
    START([Start]) --> CHECK_RESIZE{Need resize?}
    CHECK_RESIZE -- Yes --> RESIZE[Resize to 2x capacity]
    CHECK_RESIZE -- No --> HASH[Calculate hash code]
    RESIZE --> HASH
    
    HASH --> GET_BUCKET[Get bucket at index]
    GET_BUCKET --> ITERATE[Iterate through bucket]
    
    ITERATE --> KEY_EXISTS{Key exists?}
    KEY_EXISTS -- Yes --> UPDATE[Update existing value]
    KEY_EXISTS -- No --> ADD[Add new key-value pair]
    
    UPDATE & ADD --> INC_SIZE[Increment size if new]
    INC_SIZE --> END([End])
    
    subgraph "Collision Handling"
        direction LR
        BUCKET[Bucket] --> KV1[Key-Value 1]
        BUCKET --> KV2[Key-Value 2]
        BUCKET --> KV3[Key-Value 3]
    end
    
    GET_BUCKET -.-> BUCKET
    
    style START fill:#bbf,stroke:#333,stroke-width:2px
    style RESIZE fill:#f9f,stroke:#333,stroke-width:2px
    style BUCKET fill:#bfb,stroke:#333,stroke-width:2px
```

## 8. Sorted Linked List Insertion Process

```mermaid
flowchart TD
    START([Start]) --> EMPTY{List empty?}
    EMPTY -- Yes --> INS_HEAD[Set as head]
    
    EMPTY -- No --> HEAD_CHECK{Less than head?}
    HEAD_CHECK -- Yes --> INS_BEG[Insert at beginning]
    HEAD_CHECK -- No --> TRAVERSE[Traverse list]
    
    TRAVERSE --> COMPARE{Compare with next}
    COMPARE -- "Item <= Next" --> INSERT[Insert before next]
    COMPARE -- "Item > Next" --> HAS_NEXT{Has next?}
    HAS_NEXT -- Yes --> MOVE_NEXT[Move to next node]
    HAS_NEXT -- No --> INS_END[Insert at end]
    
    MOVE_NEXT --> COMPARE
    
    INS_HEAD & INS_BEG & INSERT & INS_END --> INC_SIZE[Increment size]
    INC_SIZE --> END([End])
    
    subgraph "Example of Insertion"
        direction LR
        L_HEAD[Head] --> L_N1["Mar 1"]
        L_N1 --> L_N2["Mar 3"]
        L_N2 --> L_NEW["Mar 5 (New)"]
        L_NEW --> L_N3["Mar 7"]
        L_N3 --> L_NULL[null]
    end
    
    style START fill:#bbf,stroke:#333,stroke-width:2px
    style L_NEW fill:#f9f,stroke:#333,stroke-width:2px
```
