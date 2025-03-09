# Driving Lessons Booking System: Data Structure Design

## 1. Introduction and Overview

The Driving Lessons Booking System requires an efficient method of storing, retrieving, and manipulating data related to students, instructors, cars, and bookings. Based on the system requirements and database schema, we need to design appropriate data structures that will:

- Allow fast retrieval of booking information by ID
- Support efficient insertion and deletion operations
- Enable searching and filtering of bookings by various criteria
- Maintain relationships between entities (students, instructors, cars, and bookings)

## 2. Entity Classes Design

Before designing the data structures, we need to define the core entities in our system. These will be represented as classes that encapsulate the relevant data for each entity.

### 2.1 Student Entity

```
CLASS Student
    PROPERTIES:
        studentID: STRING
        name: STRING
        email: STRING
    
    CONSTRUCTOR(id, name, email)
        this.studentID ← id
        this.name ← name
        this.email ← email
    
    FUNCTION ToString(): STRING
        RETURN "Student: " + studentID + " - " + name
END CLASS
```

### 2.2 Instructor Entity

```
CLASS Instructor
    PROPERTIES:
        instructorID: STRING
        name: STRING
        phone: STRING
    
    CONSTRUCTOR(id, name, phone)
        this.instructorID ← id
        this.name ← name
        this.phone ← phone
    
    FUNCTION ToString(): STRING
        RETURN "Instructor: " + instructorID + " - " + name
END CLASS
```

### 2.3 Car Entity

```
CLASS Car
    PROPERTIES:
        carID: STRING
        model: STRING
        type: STRING  // "Manual" or "Automatic"
        licensePlate: STRING
    
    CONSTRUCTOR(id, model, type, license)
        this.carID ← id
        this.model ← model
        this.type ← type
        this.licensePlate ← license
    
    FUNCTION ToString(): STRING
        RETURN "Car: " + carID + " - " + model + " (" + type + ")"
END CLASS
```

### 2.4 Booking Entity

```
CLASS Booking
    PROPERTIES:
        bookingID: STRING
        studentID: STRING
        instructorID: STRING
        carID: STRING
        lessonDate: DATETIME
    
    CONSTRUCTOR(bookingID, studentID, instructorID, carID, date)
        this.bookingID ← bookingID
        this.studentID ← studentID
        this.instructorID ← instructorID
        this.carID ← carID
        this.lessonDate ← date
    
    FUNCTION ToString(): STRING
        RETURN "Booking: " + bookingID + " on " + lessonDate.ToShortDateString()
END CLASS
```

## 3. Data Structure Selection and Justification

### 3.1 Primary Data Structure: Custom Hash Table

For our primary data structure, we have selected a **hash table** implementation for the following reasons:

1. **Fast Lookup Performance**: Hash tables provide O(1) average time complexity for lookup operations, which is critical for quickly retrieving booking data by ID.

2. **Efficient Insertion and Deletion**: Hash tables also offer O(1) average time complexity for insertion and deletion operations.

3. **Key-Value Pairing**: The system naturally uses unique IDs (bookingID, studentID, etc.) as keys, making the hash table's key-value structure a perfect fit.

4. **Custom Implementation Requirement**: The assignment requires custom implementation rather than using built-in library data structures.

5. **Flexible Load Factor Management**: Our custom implementation will allow us to manage collisions and load factor according to our specific needs.

### 3.2 Secondary Data Structure: Sorted Linked List

To support efficient sorting and filtering of bookings (e.g., by date), we will implement a **sorted linked list**:

1. **Maintained Ordering**: Allows bookings to be kept in chronological order
2. **Efficient Insertion**: O(n) insertion time complexity, but still efficient for the expected size of our dataset
3. **Sequential Access**: Makes it easy to iterate through bookings in order
4. **Simple Implementation**: Relatively easy to implement while maintaining good performance

## 4. Hash Table Design

### 4.1 Hash Table Structure

```
CLASS HashTable<K, V>
    PROPERTIES:
        buckets: ARRAY OF LinkedList<KeyValuePair<K, V>>
        capacity: INTEGER
        size: INTEGER
        loadFactor: FLOAT
    
    CONSTRUCTOR(initialCapacity)
        this.capacity ← initialCapacity
        this.size ← 0
        this.loadFactor ← 0.75
        this.buckets ← NEW ARRAY OF LinkedList<KeyValuePair<K, V>>(capacity)
        
        // Initialize each bucket with an empty linked list
        FOR i ← 0 TO capacity - 1 DO
            buckets[i] ← NEW LinkedList<KeyValuePair<K, V>>()
        END FOR
END CLASS
```

### 4.2 Hash Function

```
FUNCTION GetHashCode(key: K): INTEGER
    IF key IS NULL THEN
        RETURN 0
    END IF
    
    hashCode ← key.GetHashCode()
    
    // Ensure positive hash code
    IF hashCode < 0 THEN
        hashCode ← hashCode * -1
    END IF
    
    // Return index within capacity bounds
    RETURN hashCode MOD capacity
END FUNCTION
```

### 4.3 Insert Operation

```
FUNCTION Put(key: K, value: V): VOID
    // Check if resizing is needed
    IF (size + 1) / capacity > loadFactor THEN
        Resize(capacity * 2)
    END IF
    
    bucketIndex ← GetHashCode(key)
    bucket ← buckets[bucketIndex]
    
    // Check if key already exists
    FOR pair IN bucket DO
        IF pair.Key EQUALS key THEN
            pair.Value ← value  // Update existing value
            RETURN
        END IF
    END FOR
    
    // Add new key-value pair
    bucket.Add(NEW KeyValuePair<K, V>(key, value))
    size ← size + 1
END FUNCTION
```

### 4.4 Retrieve Operation

```
FUNCTION Get(key: K): V
    bucketIndex ← GetHashCode(key)
    bucket ← buckets[bucketIndex]
    
    // Search for the key
    FOR pair IN bucket DO
        IF pair.Key EQUALS key THEN
            RETURN pair.Value
        END IF
    END FOR
    
    // Key not found
    RETURN NULL
END FUNCTION
```

### 4.5 Remove Operation

```
FUNCTION Remove(key: K): BOOLEAN
    bucketIndex ← GetHashCode(key)
    bucket ← buckets[bucketIndex]
    
    // Find and remove the key
    FOR i ← 0 TO bucket.Count - 1 DO
        IF bucket[i].Key EQUALS key THEN
            bucket.RemoveAt(i)
            size ← size - 1
            RETURN TRUE
        END IF
    END FOR
    
    RETURN FALSE  // Key not found
END FUNCTION
```

### 4.6 Resize Operation

```
FUNCTION Resize(newCapacity: INTEGER): VOID
    oldBuckets ← this.buckets
    
    // Create new array of buckets
    this.buckets ← NEW ARRAY OF LinkedList<KeyValuePair<K, V>>(newCapacity)
    this.capacity ← newCapacity
    this.size ← 0
    
    // Initialize new buckets
    FOR i ← 0 TO newCapacity - 1 DO
        buckets[i] ← NEW LinkedList<KeyValuePair<K, V>>()
    END FOR
    
    // Re-hash all existing entries
    FOR oldBucket IN oldBuckets DO
        FOR pair IN oldBucket DO
            Put(pair.Key, pair.Value)
        END FOR
    END FOR
END FUNCTION
```

### 4.7 Iteration Operation

```
FUNCTION GetAllValues(): ARRAY OF V
    result ← NEW ARRAY OF V(size)
    index ← 0
    
    FOR bucket IN buckets DO
        FOR pair IN bucket DO
            result[index] ← pair.Value
            index ← index + 1
        END FOR
    END FOR
    
    RETURN result
END FUNCTION
```

## 5. Sorted Linked List Design

### 5.1 Node Structure

```
CLASS Node<T>
    PROPERTIES:
        data: T
        next: Node<T>
    
    CONSTRUCTOR(data: T)
        this.data ← data
        this.next ← NULL
END CLASS
```

### 5.2 Sorted Linked List Structure

```
CLASS SortedLinkedList<T>
    PROPERTIES:
        head: Node<T>
        size: INTEGER
        comparator: FUNCTION(T, T): INTEGER
    
    CONSTRUCTOR(comparator: FUNCTION(T, T): INTEGER)
        this.head ← NULL
        this.size ← 0
        this.comparator ← comparator
END CLASS
```

### 5.3 Insert Operation (Sorted)

```
FUNCTION Insert(item: T): VOID
    newNode ← NEW Node<T>(item)
    
    // Empty list case
    IF head IS NULL THEN
        head ← newNode
        size ← size + 1
        RETURN
    END IF
    
    // Item belongs at the beginning
    IF comparator(item, head.data) < 0 THEN
        newNode.next ← head
        head ← newNode
        size ← size + 1
        RETURN
    END IF
    
    // Find the insertion point
    current ← head
    WHILE current.next IS NOT NULL AND comparator(item, current.next.data) > 0 DO
        current ← current.next
    END WHILE
    
    // Insert node
    newNode.next ← current.next
    current.next ← newNode
    size ← size + 1
END FUNCTION
```

### 5.4 Remove Operation

```
FUNCTION Remove(item: T): BOOLEAN
    // Empty list case
    IF head IS NULL THEN
        RETURN FALSE
    END IF
    
    // Head is the item to remove
    IF comparator(head.data, item) = 0 THEN
        head ← head.next
        size ← size - 1
        RETURN TRUE
    END IF
    
    // Search for the item
    current ← head
    WHILE current.next IS NOT NULL AND comparator(current.next.data, item) != 0 DO
        current ← current.next
    END WHILE
    
    // Item not found
    IF current.next IS NULL THEN
        RETURN FALSE
    END IF
    
    // Remove the item
    current.next ← current.next.next
    size ← size - 1
    RETURN TRUE
END FUNCTION
```

### 5.5 Search Operation

```
FUNCTION Contains(item: T): BOOLEAN
    current ← head
    
    WHILE current IS NOT NULL DO
        IF comparator(current.data, item) = 0 THEN
            RETURN TRUE
        END IF
        
        // Early exit if we've passed where the item would be
        IF comparator(current.data, item) > 0 THEN
            RETURN FALSE
        END IF
        
        current ← current.next
    END WHILE
    
    RETURN FALSE
END FUNCTION
```

### 5.6 Get All Items Operation

```
FUNCTION GetAllItems(): ARRAY OF T
    result ← NEW ARRAY OF T(size)
    index ← 0
    current ← head
    
    WHILE current IS NOT NULL DO
        result[index] ← current.data
        index ← index + 1
        current ← current.next
    END WHILE
    
    RETURN result
END FUNCTION
```

## 6. Booking Manager Design

The Booking Manager will use both the HashTable and SortedLinkedList to efficiently manage booking operations.

```
CLASS BookingManager
    PROPERTIES:
        bookingsById: HashTable<STRING, Booking>
        bookingsByDate: SortedLinkedList<Booking>
        
    CONSTRUCTOR()
        this.bookingsById ← NEW HashTable<STRING, Booking>(100)
        this.bookingsByDate ← NEW SortedLinkedList<Booking>(CompareBookingsByDate)
        
    // Comparator function for sorting bookings by date
    FUNCTION CompareBookingsByDate(booking1: Booking, booking2: Booking): INTEGER
        RETURN booking1.lessonDate.CompareTo(booking2.lessonDate)
END CLASS
```

### 6.1 Add Booking Operation

```
FUNCTION AddBooking(booking: Booking): BOOLEAN
    // Check if booking already exists
    IF bookingsById.Get(booking.bookingID) IS NOT NULL THEN
        RETURN FALSE  // Booking already exists
    END IF
    
    // Add to hash table
    bookingsById.Put(booking.bookingID, booking)
    
    // Add to sorted linked list
    bookingsByDate.Insert(booking)
    
    RETURN TRUE
END FUNCTION
```

### 6.2 Remove Booking Operation

```
FUNCTION RemoveBooking(bookingId: STRING): BOOLEAN
    booking ← bookingsById.Get(bookingId)
    
    IF booking IS NULL THEN
        RETURN FALSE  // Booking doesn't exist
    END IF
    
    // Remove from hash table
    bookingsById.Remove(bookingId)
    
    // Remove from sorted linked list
    bookingsByDate.Remove(booking)
    
    RETURN TRUE
END FUNCTION
```

### 6.3 Get Booking By ID Operation

```
FUNCTION GetBookingById(bookingId: STRING): Booking
    RETURN bookingsById.Get(bookingId)
END FUNCTION
```

### 6.4 Get Bookings By Date Range Operation

```
FUNCTION GetBookingsByDateRange(startDate: DATETIME, endDate: DATETIME): ARRAY OF Booking
    allBookings ← bookingsByDate.GetAllItems()
    resultList ← NEW List<Booking>()
    
    FOR booking IN allBookings DO
        IF booking.lessonDate >= startDate AND booking.lessonDate <= endDate THEN
            resultList.Add(booking)
        END IF
        
        // Early exit once we've passed the end date
        IF booking.lessonDate > endDate THEN
            BREAK
        END IF
    END FOR
    
    RETURN resultList.ToArray()
END FUNCTION
```

## 7. Time Complexity Analysis

### 7.1 Hash Table Operations

| Operation | Average Case | Worst Case |
|-----------|--------------|------------|
| Insert    | O(1)         | O(n)       |
| Lookup    | O(1)         | O(n)       |
| Delete    | O(1)         | O(n)       |
| Resize    | O(n)         | O(n)       |

The worst-case scenario occurs when there are many collisions, causing the hash table to degenerate into a linked list. However, with a good hash function and proper management of the load factor, the average case performance is constant time.

### 7.2 Sorted Linked List Operations

| Operation | Average Case | Worst Case |
|-----------|--------------|------------|
| Insert    | O(n)         | O(n)       |
| Delete    | O(n)         | O(n)       |
| Search    | O(n)         | O(n)       |
| Iteration | O(n)         | O(n)       |

The sorted linked list operations are all linear time. However, for operations like getting bookings within a date range, this structure provides better performance than a hash table because we can exploit the sorted nature of the data.

### 7.3 Booking Manager Operations

| Operation                | Average Case | Worst Case |
|--------------------------|--------------|------------|
| AddBooking               | O(n)         | O(n)       |
| RemoveBooking            | O(n)         | O(n)       |
| GetBookingById           | O(1)         | O(n)       |
| GetBookingsByDateRange   | O(k)         | O(n)       |

Where:
- n is the total number of bookings
- k is the number of bookings in the specified date range

## 8. Space Complexity Analysis

Hash Table: O(n) where n is the number of key-value pairs stored
Sorted Linked List: O(n) where n is the number of elements stored
Overall System: O(2n) = O(n) for storing n bookings in both data structures

## 9. Conclusion

The proposed design combines the strengths of hash tables (fast lookups by ID) and sorted linked lists (efficient range queries by date) to create an efficient booking management system. This hybrid approach ensures that:

1. Looking up bookings by ID is very fast (constant time on average)
2. Retrieving bookings by date range is efficient (linear time proportional to the results)
3. The system can handle various operations required by the driving school booking application

The design meets all requirements specified in the assignment and provides a solid foundation for implementation.
