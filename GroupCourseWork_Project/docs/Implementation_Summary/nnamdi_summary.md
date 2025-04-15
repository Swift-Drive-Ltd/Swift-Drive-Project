# Implementation Summary

### Commit 1: Add Basic Structure and Hash Function for CustomHashTable
**Implemented:**
- Created `CustomHashTable<TKey, TValue>` class with the following structure:
  - `HashNode` inner class to represent key-value pairs with:
    - `Key`: Stores the key for the hash entry.
    - `Value`: Stores the corresponding value.
    - `Next`: Reference for linked list chaining in case of collisions.
  - `table` array to store hash nodes.
  - Constructor to initialize the hash table's size.
- Added `GetHash` method to compute hash index:
  - Uses `Math.Abs(key.GetHashCode()) % size` to ensure the index is within bounds.
  - Handles negative hash codes.

**Technical Details:**
- The `HashNode` class encapsulates individual hash table entries.
- The hash function ensures efficient key distribution and prevents index overflow.

**Next Steps:**
- Implement core operations: Insert, Search, Delete, and Display.

---

### Commit 2: Implement Insert and Search Operations
**Implemented:**
- **Insert Method:**
  - Computes the hash index using `GetHash`.
  - Adds new key-value pairs:
    - Handles empty bucket case.
    - Resolves collisions using linked list chaining.
    - Updates value if the key already exists.
- **Search Method:**
  - Computes the hash index using `GetHash`.
  - Traverses the linked list at the index to find the key.
  - Returns the value if found or `default` if not.

**Technical Details:**
- **Insert:**
  - Handles collisions efficiently by chaining.
  - Maintains O(1) average time complexity; O(n) in worst-case collisions.
- **Search:**
  - Provides O(1) average time complexity; O(n) in worst-case collisions.
  - Ensures robustness with proper null handling.

**Next Steps:**
- Implement Delete operation.
- Add Display method for debugging and inspection.

---

### Commit 3: Complete CustomHashTable with Delete and Display Methods
**Implemented:**
- **Delete Method:**
  - Removes key-value pairs from the hash table:
    - Handles empty bucket case.
    - Deletes head node or nodes within the chain.
    - Updates references to maintain list integrity.
  - Returns `true` if deletion is successful; `false` otherwise.
- **Display Method:**
  - Iterates through the hash table and prints stored values.
  - Traverses each bucket and outputs all elements in the chain.

**Technical Details:**
- **Delete:**
  - O(1) average time complexity; O(n) in worst-case collisions.
  - Properly manages edge cases (empty list, key not found).
- **Display:**
  - Utilizes the `ToString` method of stored values for display.
  - Provides a simple way to debug and inspect hash table contents.

**Completed Features:**
- A fully functional hash table implementation with:
  - Core operations: Insert, Search, Delete.
  - Collision handling using linked list chaining.
  - Display method for viewing all stored elements.
- Robust handling of null values and edge cases.
