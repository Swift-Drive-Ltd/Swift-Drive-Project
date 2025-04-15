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

## 2. Data Structure Overview

```mermaid
flowchart TD
    UI[User Interface]
    BM[Booking Manager]
    HT[Hash Table]
    SLL[Sorted Linked List]
    ENT[Entities]
    DB[(Database)]
    
    UI --> BM
    BM --> HT
    BM --> SLL
    HT --> ENT
    SLL --> ENT
    ENT <--> DB
    
    style BM fill:#f9f,stroke:#333,stroke-width:2px
    style HT fill:#bbf,stroke:#333,stroke-width:2px
    style SLL fill:#bbf,stroke:#333,stroke-width:2px
```

## 3. Hash Table Structure

```mermaid
flowchart TD
    HT[Hash Table] --> B0[Bucket 0]
    HT --> B1[Bucket 1]
    HT --> B2[Bucket 2]
    HT --> B3[Bucket 3]
    
    B1 --> KV1["Key: 'B001'\nValue: Booking Object"]
    B1 --> KV2["Key: 'B101'\nValue: Booking Object"]
    B3 --> KV3["Key: 'B003'\nValue: Booking Object"]
    
    style HT fill:#f9f,stroke:#333,stroke-width:2px
```

## 4. Sorted Linked List Structure

```mermaid
flowchart LR
    HEAD[Head] --> N1
    N1["Booking: Mar 1"] --> N2
    N2["Booking: Mar 3"] --> N3
    N3["Booking: Mar 7"] --> N4
    N4["Booking: Mar 10"] --> NULL
    
    style HEAD fill:#bbf,stroke:#333,stroke-width:2px
```

## 5. Key Classes

```mermaid
classDiagram
    class Student {
        -string studentID
        -string name
        -string email
        +ToString()
    }
    
    class Instructor {
        -string instructorID
        -string name
        -string phone
        +ToString()
    }
    
    class Car {
        -string carID
        -string model
        -string type
        -string licensePlate
        +ToString()
    }
    
    class Booking {
        -string bookingID
        -string studentID
        -string instructorID
        -string carID
        -DateTime lessonDate
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

## 6. Hash Table Operations

```mermaid
flowchart LR
    PUT["Put: O(1)"] --- GET["Get: O(1)"] --- REM["Remove: O(1)"] --- RES["Resize: O(n)"]
```

## 7. Sorted Linked List Operations

```mermaid
flowchart LR
    INS["Insert: O(n)"] --- REM["Remove: O(n)"] --- SRCH["Search: O(n)"] --- RANGE["Range: O(k)"]
```

## 8. Booking Management Basic Flow

```mermaid
sequenceDiagram
    actor User
    participant BM as BookingManager
    participant HT as HashTable
    participant SLL as SortedLinkedList
    
    User->>BM: AddBooking(booking)
    BM->>HT: Put(bookingID, booking)
    BM->>SLL: Insert(booking)
    BM-->>User: Confirmation
    
    User->>BM: GetBookingById(bookingID)
    BM->>HT: Get(bookingID)
    HT-->>BM: Return booking
    BM-->>User: Display booking
```

## 9. Hash Function Visualization

```mermaid
flowchart LR
    KEY[bookingID] --> HF[GetHashCode]
    HF --> MOD[Modulo capacity]
    MOD --> IDX[Bucket Index]
    
    style HF fill:#bbf,stroke:#333,stroke-width:2px
```

## 10. Performance Comparison

```mermaid
flowchart TD
    HT_GET["Hash Table Lookup\nO(1)"] -->|"Much faster than"| ARR_SEARCH["Array Search\nO(n)"]
    SLL_RANGE["SLL Range Query\nO(k)"] -->|"More efficient for ranges"| HT_GET
    
    style HT_GET fill:#bfb,stroke:#333,stroke-width:2px
    style SLL_RANGE fill:#bfb,stroke:#333,stroke-width:2px
```
