# Module 13: Hash Tables

## 📋 Overview

This module explores hash table data structures, one of the most important and widely-used data structures in computer science. You'll learn to implement different collision resolution strategies, understand hash functions, and master the concepts of load factors and dynamic resizing for optimal performance.

## 🎯 Learning Objectives

- Implement hash tables with different collision resolution strategies
- Understand hash functions and their importance
- Master collision handling techniques (chaining vs open addressing)
- Implement dynamic resizing and load factor management
- Analyze time complexity of hash table operations
- Write comprehensive unit tests for hash data structures
- Apply hash tables to solve real-world problems efficiently

## 📁 Project Structure

```
module13/
├── Hashing/
│   ├── Program.cs                  # Main demonstration program
│   ├── HashSet.cs                 # Interface defining hash set operations
│   ├── HashSetChaining.cs         # Collision resolution via linked lists
│   ├── HashSetLinearProbing.cs    # Open addressing with linear probing
│   ├── Hashing.Tests.cs           # Comprehensive unit tests
│   └── hashing.csproj             # Project configuration
└── eksempel/
    ├── Program.cs                 # Hash function examples with User class
    ├── eksempel.csproj           # Example project configuration
    └── README.md                 # This documentation
```

## 🎯 HashSet Interface

Common interface for all hash set implementations:

```csharp
using Hashing;

public interface HashSet 
{
    public bool Add(Object x);        // Add element to set
    public bool Remove(Object x);     // Remove element from set
    public bool Contains(Object x);   // Check if element exists
    public int Size();               // Get number of elements
}
```

**Key Operations:**
- **Add**: Insert element, return true if new element
- **Remove**: Delete element, return true if element existed
- **Contains**: Check membership, return true if found
- **Size**: Return current number of elements

## 🔗 Hash Set with Chaining

### Implementation Overview

Uses linked lists (separate chaining) to handle hash collisions:

```csharp
public class HashSetChaining : HashSet
{
    private Node[] buckets;          // Array of linked list heads
    private int currentSize;         // Number of elements stored

    private class Node
    {
        public Node(Object data, Node next)
        {
            this.Data = data;
            this.Next = next;
        }
        public Object Data { get; set; }
        public Node Next { get; set; }
    }

    public HashSetChaining(int size)
    {
        buckets = new Node[size];
        currentSize = 0;
    }
}
```

### Hash Function

Simple modular hash function with collision protection:

```csharp
private int HashValue(Object x)
{
    int h = x.GetHashCode();
    if (h < 0)
    {
        h = -h;                    // Handle negative hash codes
    }
    h = h % buckets.Length;        // Map to bucket range
    return h;
}
```

**How it works:**
1. **Get Hash Code**: Use object's built-in `GetHashCode()`
2. **Handle Negatives**: Convert negative values to positive
3. **Modular Arithmetic**: Map to valid bucket index range

### Add Operation

Inserts element with collision handling via chaining:

```csharp
public bool Add(Object x)
{
    int h = HashValue(x);
    int size = buckets.Length;
    float loadFactor = currentSize / size;
    
    // Check if rehashing is needed
    if (loadFactor > 0.75)
    {
        ReHash(size * 2);
    }

    Node bucket = buckets[h];
    bool found = false;
    
    // Check if element already exists
    while (!found && bucket != null)
    {
        if (bucket.Data.Equals(x))
        {
            found = true;
        }
        else
        {
            bucket = bucket.Next;
        }
    }

    // Add new element if not found
    if (!found)
    {
        Node newNode = new Node(x, buckets[h]);
        buckets[h] = newNode;        // Insert at head of chain
        currentSize++;
    }

    return !found;                   // Return true if element was added
}
```

**Time Complexity:**
- **Average Case**: O(1) - assuming good hash distribution
- **Worst Case**: O(n) - all elements hash to same bucket

### Dynamic Resizing (ReHash)

Grows hash table when load factor exceeds threshold:

```csharp
private Node[] ReHash(int newSize)
{
    Node[] newBuckets = new Node[newSize];
    
    // Rehash all existing elements
    for (int i = 0; i < buckets.Length; i++)
    {
        Node temp = buckets[i];
        while (temp != null)
        {
            // Calculate new hash for new size
            int h = temp.Data.GetHashCode();
            if (h < 0) h = -h;
            h = h % newSize;

            // Insert into new bucket array
            Node newNode = new Node(temp.Data, newBuckets[h]);
            newBuckets[h] = newNode;
            temp = temp.Next;
        }
    }
    buckets = newBuckets;
    return buckets;
}
```

**Load Factor Management:**
- **Threshold**: 0.75 (75% full)
- **Growth Strategy**: Double the size
- **Rehashing**: All elements must be rehashed for new size

### Contains Operation

Searches for element in appropriate chain:

```csharp
public bool Contains(Object x)
{
    int h = HashValue(x);
    Node bucket = buckets[h];
    bool found = false;
    
    while (!found && bucket != null)
    {
        if (bucket.Data.Equals(x))
        {
            found = true;
        }
        else
        {
            bucket = bucket.Next;
        }
    }
    return found;
}
```

### Remove Operation

Removes element from appropriate chain:

```csharp
public bool Remove(Object x)
{
    int h = HashValue(x);
    Node bucket = buckets[h];
    bool found = false;
    
    while (!found && bucket != null)
    {
        if (bucket.Next != null && bucket.Next.Data.Equals(x))
        {
            // Remove middle/end node
            found = true;
            bucket.Next = bucket.Next.Next;
            currentSize--;
        }
        else if (bucket.Data.Equals(x))
        {
            // Remove head node
            buckets[h] = bucket.Next;
            currentSize--;
            return true;
        }
        else
        {
            bucket = bucket.Next;
        }
    }
    return found;
}
```

## 📍 Hash Set with Linear Probing

### Open Addressing Implementation

Uses linear probing to resolve collisions:

```csharp
public class HashSetLinearProbing : HashSet
{
    private Object[] buckets;        // Direct storage array
    private int currentSize;         // Number of elements
    private enum State { DELETED }  // Marker for deleted elements

    public HashSetLinearProbing(int bucketsLength)
    {
        buckets = new Object[bucketsLength];
        currentSize = 0;
    }
}
```

**Key Differences from Chaining:**
- **Direct Storage**: Elements stored directly in array
- **No Linked Lists**: Uses probing to find empty slots
- **Deleted Markers**: Special markers for removed elements

### Add with Linear Probing

Finds next available slot using linear probing:

```csharp
public bool Add(Object x)
{
    if (currentSize >= buckets.Length)
    {
        return false;                // Hash table is full
    }

    int h = HashValue(x);
    int i = h % buckets.Length;
    
    // Linear probe until empty slot or existing element found
    while (buckets[i] != null && !buckets[i].Equals(State.DELETED))
    {
        if (buckets[i].Equals(x))
        {
            return false;            // Element already exists
        }
        i++;
        if (i >= buckets.Length)
        {
            i = 0;                   // Wrap around to beginning
        }
    }
    
    buckets[i] = x;                  // Insert at found position
    currentSize++;
    return true;                     // Note: Implementation bug - should return true
}
```

**Probing Strategy:**
1. **Start**: At hash value position
2. **Check**: If slot is empty or contains target
3. **Probe**: Move to next slot (linear)
4. **Wrap**: Around to beginning if needed

### Contains with Linear Probing

Searches using same probing strategy:

```csharp
public bool Contains(Object x)
{
    int h = HashValue(x);
    int i = h % buckets.Length;
    int start = i;                   // Remember starting position
    
    while (buckets[i] != null)
    {
        if (buckets[i].Equals(x))
        {
            return true;             // Found element
        }
        i++;
        if (i >= buckets.Length)
        {
            i = 0;                   // Wrap around
        }
        if (i == start)              // Full circle - not found
        {
            return false;
        }
    }
    return false;                    // Hit empty slot - not found
}
```

### Remove with Soft Delete

Uses deletion markers to maintain probe sequences:

```csharp
public bool Remove(Object x)
{
    int h = HashValue(x);
    int i = h % buckets.Length;
    int start = i;
    
    while (buckets[i] != null)
    {
        if (buckets[i].Equals(x))
        {
            buckets[i] = State.DELETED;  // Mark as deleted
            currentSize--;
            return true;
        }
        i++;
        if (i >= buckets.Length)
        {
            i = 0;                       // Wrap around
        }
        if (i == start)                  // Full circle - not found
        {
            break;
        }
    }
    return false;
}
```

**Why Soft Delete?**
- **Probe Sequences**: Hard delete would break search paths
- **Example**: If A and B hash to same slot, A goes to slot 0, B to slot 1
- **Problem**: If A is deleted (hard), search for B would stop at empty slot 0
- **Solution**: Mark A as DELETED, search continues through marked slots

## 🧪 Hash Function Examples

### User Class with Custom Hash

**eksempel/Program.cs** demonstrates custom hash function implementation:

```csharp
User user1 = new User { Id = 42, Username = "Kristian" };
User user2 = new User { Id = 22, Username = "Mads" };
User user3 = new User { Id = 42, Username = "Kristian" };
User user4 = user1;

Console.WriteLine(user1.GetHashCode());  // Custom hash code
Console.WriteLine(user2.GetHashCode());  // Different hash code
Console.WriteLine(user3.GetHashCode());  // Same as user1

Console.WriteLine(user1.Equals(user3));  // True - same content
Console.WriteLine(user1 == user3);       // False - different references

public class User 
{
    public int Id { get; set; }
    public string Username { get; set; }

    public override bool Equals(object obj)
    {
        User input = (User) obj;
        return input.Id == this.Id;      // Equality based on ID
    }

    public override int GetHashCode()
    {
        return Id + Username.GetHashCode();  // Combine ID and username hash
    }
}
```

**Key Concepts:**
- **Equals Contract**: Objects that are equal must have same hash code
- **Hash Function**: Combines multiple fields for better distribution
- **Reference vs Value**: `==` checks references, `Equals()` checks content

## 📊 Performance Comparison

### Chaining vs Linear Probing

| Aspect | Chaining | Linear Probing |
|--------|----------|----------------|
| **Collision Handling** | Separate linked lists | Probe for next empty slot |
| **Memory Usage** | Higher (pointers) | Lower (direct storage) |
| **Cache Performance** | Poor (scattered nodes) | Better (contiguous array) |
| **Load Factor Limit** | Can exceed 1.0 | Must stay below 1.0 |
| **Implementation** | More complex | Simpler |
| **Deletion** | Simple removal | Requires soft delete |

### Time Complexity Analysis

**Average Case (good hash distribution):**
- **Add**: O(1) for both implementations
- **Remove**: O(1) for both implementations
- **Contains**: O(1) for both implementations

**Worst Case (poor hash distribution):**
- **Chaining**: O(n) - all elements in one chain
- **Linear Probing**: O(n) - linear search through array

**Load Factor Impact:**
```
Chaining Performance:
Load Factor 0.5: ~1.5 probes average
Load Factor 1.0: ~2.0 probes average
Load Factor 2.0: ~3.0 probes average

Linear Probing Performance:
Load Factor 0.5: ~1.5 probes average
Load Factor 0.75: ~4.0 probes average
Load Factor 0.9: ~10+ probes average
```

## 🧪 Comprehensive Unit Tests

### Chaining Tests

```csharp
[TestClass]
public class Hashing_Tests
{
    [TestMethod]
    public void TestAddChaining()
    {
        HashSetChaining names = new HashSetChaining(20);
        names.Add("Harry");
        names.Add("Sue");
        names.Add("Nina");
        names.Add("Susannah");
        names.Add("Larry");
        names.Add("Eve");
        names.Add("Sarah");
        names.Add("Adam");
        names.Add("Tony");
        names.Add("Katherine");
        names.Add("Juliet");
        names.Add("Romeo");
        
        Assert.AreEqual(12, names.Size());
    }

    [TestMethod]
    public void TestRehashing()
    {
        // Create small hash set to force rehashing
        HashSetChaining hashSet = new HashSetChaining(2);

        // Add elements to trigger rehashing
        hashSet.Add("One");   // 1/2 = 0.5 load factor
        hashSet.Add("Two");   // 2/2 = 1.0 - triggers rehash to size 4
        hashSet.Add("Three"); // 3/4 = 0.75 load factor
        hashSet.Add("Four");  // 4/4 = 1.0 - triggers rehash to size 8
        hashSet.Add("Five");  // 5/8 = 0.625 load factor

        // Verify all elements present after rehashing
        Assert.IsTrue(hashSet.Contains("One"));
        Assert.IsTrue(hashSet.Contains("Two"));
        Assert.IsTrue(hashSet.Contains("Three"));
        Assert.IsTrue(hashSet.Contains("Four"));
        Assert.IsTrue(hashSet.Contains("Five"));
        Assert.AreEqual(5, hashSet.Size());
    }

    [TestMethod]
    public void TestContainsChaining()
    {
        HashSetChaining names = new HashSetChaining(4);
        names.Add("Harry");
        names.Add("Sue");
        names.Add("Nina");
        names.Add("Romeo");
        
        Assert.IsTrue(names.Contains("Nina"));
        Assert.IsTrue(names.Contains("Romeo"));
        
        names.Remove("Romeo");
        Assert.IsFalse(names.Contains("Romeo"));
    }
}
```

### Linear Probing Tests

```csharp
[TestMethod]
public void TestAddLinearProbing()
{
    HashSetLinearProbing names = new HashSetLinearProbing(13);
    names.Add("Harry");
    names.Add("Sue");
    names.Add("Nina");
    names.Add("Susannah");
    names.Add("Larry");
    names.Add("Eve");
    names.Add("Sarah");
    names.Add("Adam");
    names.Add("Tony");
    names.Add("Katherine");
    names.Add("Juliet");
    names.Add("Romeo");
    
    Assert.AreEqual(12, names.Size());
}

[TestMethod]
public void TestContainsLinearProbing()
{
    HashSetLinearProbing names = new HashSetLinearProbing(5);
    names.Add("Harry");
    names.Add("Sue");
    names.Add("Nina");
    names.Add("Susannah");
    names.Add("Larry");
    
    Assert.IsTrue(names.Contains("Nina"));
    Assert.IsTrue(names.Contains("Harry"));

    names.Remove("Sue");
    Assert.IsFalse(names.Contains("Sue"));
}
```

## 🚀 Demo Program

**Program.cs** demonstrates hash set operations:

```csharp
using Hashing;

HashSet names = new HashSetChaining(13);

names.Add("Harry");
names.Add("Sue");
names.Add("Nina");
names.Add("Susannah");
names.Add("Larry");
names.Add("Eve");
names.Add("Sarah");
names.Add("Adam");
names.Add("Tony");
names.Add("Katherine");
names.Add("Juliet");
names.Add("Romeo");

Console.WriteLine(names);                        // Display hash table
Console.WriteLine("Size: " + names.Size());     // Output: Size: 12

Console.WriteLine("Contains Romeo: " + names.Contains("Romeo"));  // True
names.Remove("Romeo");
Console.WriteLine("Contains Romeo: " + names.Contains("Romeo"));  // False

names.Remove("Nina");
Console.WriteLine("Contains Nina: " + names.Contains("Nina"));    // False
Console.WriteLine("Size: " + names.Size());     // Output: Size: 10
```

## 🚀 How to Run

### 1. Navigate to Project Directory
```bash
# Main hashing implementation
cd module13/Hashing

# Hash function examples
cd module13/eksempel
```

### 2. Run Demonstrations
```bash
# Run hash set demonstration
cd module13/Hashing
dotnet run

# Run hash function examples
cd module13/eksempel
dotnet run
```

### 3. Run Unit Tests
```bash
cd module13/Hashing
dotnet test

# Run with detailed output
dotnet test --verbosity normal

# Run specific test method
dotnet test --filter TestRehashing
```

## 🔑 Key Hash Table Concepts

### Hash Functions Properties

**Good Hash Function Characteristics:**
1. **Deterministic**: Same input always produces same hash
2. **Uniform Distribution**: Spreads elements evenly across buckets
3. **Fast Computation**: O(1) time to compute
4. **Avalanche Effect**: Small input changes cause large hash changes

**Common Hash Function Techniques:**
```csharp
// Simple modular hashing
hash = key.GetHashCode() % tableSize;

// Multiplication method
hash = (int)((key * 0.6180339887) % 1 * tableSize);

// Universal hashing
hash = ((a * key + b) % p) % tableSize;  // a, b random, p prime
```

### Load Factor Management

**Load Factor Definition:**
```
Load Factor = Number of Elements / Table Size
```

**Optimal Load Factors:**
- **Chaining**: 0.7 - 1.0 (can exceed 1.0)
- **Linear Probing**: 0.5 - 0.7 (must stay below 1.0)
- **Quadratic Probing**: 0.5 (performance degrades quickly)

### Collision Resolution Strategies

**Open Addressing Variants:**
1. **Linear Probing**: `h(k, i) = (h'(k) + i) mod m`
2. **Quadratic Probing**: `h(k, i) = (h'(k) + i²) mod m`
3. **Double Hashing**: `h(k, i) = (h₁(k) + i × h₂(k)) mod m`

**Separate Chaining Variants:**
1. **Linked Lists**: Standard implementation
2. **Dynamic Arrays**: Better cache performance
3. **Binary Search Trees**: Better worst-case performance

## 🎓 Practice Exercises

### Beginner Level

1. **Robin Hood Hashing**: Implement linear probing with Robin Hood heuristic
2. **Quadratic Probing**: Replace linear probing with quadratic probing
3. **Double Hashing**: Implement secondary hash function for probing
4. **String Hash Functions**: Implement polynomial rolling hash

### Intermediate Level

5. **Cuckoo Hashing**: Implement guaranteed O(1) worst-case lookup
6. **Consistent Hashing**: Implement for distributed systems
7. **Bloom Filters**: Implement probabilistic set membership
8. **Count-Min Sketch**: Implement frequency estimation data structure

### Advanced Level

9. **Thread-Safe Hash Table**: Implement concurrent hash table with locks
10. **Lock-Free Hash Table**: Use atomic operations and CAS
11. **Cache-Conscious Hash Table**: Optimize for CPU cache performance
12. **External Hashing**: Handle data larger than memory

### Expert Level

13. **Hopscotch Hashing**: Advanced open addressing scheme
14. **Extendible Hashing**: Dynamic hashing for databases
15. **Linear Hashing**: Gradual resizing technique
16. **MinHash and LSH**: Locality-sensitive hashing for similarity

## 🔧 Common Issues and Solutions

### Hash Function Problems

**Poor Distribution:**
```csharp
// ❌ Bad: Only uses last few bits
public override int GetHashCode()
{
    return Id % 1000;  // Many collisions if IDs are sequential
}

// ✅ Good: Uses more bits, better distribution
public override int GetHashCode()
{
    return Id * 31 + Username.GetHashCode();
}
```

**Equals/HashCode Contract Violation:**
```csharp
// ❌ Dangerous: Hash code can change
public override int GetHashCode()
{
    return MutableField.GetHashCode();  // Don't hash mutable fields!
}

// ✅ Safe: Hash only immutable fields
public override int GetHashCode()
{
    return ImmutableId.GetHashCode();
}
```

### Resizing Issues

**Infinite Growth:**
```csharp
// Add maximum size check
if (newSize > MAX_CAPACITY)
{
    throw new InvalidOperationException("Hash table too large");
}
```

**Rehashing Performance:**
```csharp
// Amortize cost by doubling size
// O(1) amortized time per insertion
private void ResizeIfNeeded()
{
    if (currentSize > buckets.Length * LOAD_FACTOR_THRESHOLD)
    {
        ReHash(buckets.Length * 2);  // Double size
    }
}
```

## 📊 Performance Visualization

### Load Factor Impact on Performance

```
Chaining Performance vs Load Factor:
Load Factor: 0.25 |████                | Avg Probes: 1.125
Load Factor: 0.50 |████████            | Avg Probes: 1.25  
Load Factor: 0.75 |████████████        | Avg Probes: 1.375
Load Factor: 1.00 |████████████████    | Avg Probes: 1.5
Load Factor: 2.00 |████████████████████| Avg Probes: 2.0

Linear Probing Performance vs Load Factor:
Load Factor: 0.25 |████                | Avg Probes: 1.17
Load Factor: 0.50 |████████            | Avg Probes: 1.5
Load Factor: 0.75 |████████████        | Avg Probes: 2.5
Load Factor: 0.90 |██████████████████  | Avg Probes: 5.5
Load Factor: 0.95 |███████████████████ | Avg Probes: 10.5
```

## 📚 Further Reading

- [Introduction to Algorithms (CLRS) - Hash Tables](https://mitpress.mit.edu/books/introduction-algorithms-third-edition)
- [Hash Table Good Hash Functions](https://research.cs.vt.edu/AVresearch/hashing/)
- [Cuckoo Hashing](https://en.wikipedia.org/wiki/Cuckoo_hashing)
- [Robin Hood Hashing](https://cs.uwaterloo.ca/research/tr/1986/CS-86-14.pdf)
- [Consistent Hashing](https://en.wikipedia.org/wiki/Consistent_hashing)
- [Hash Functions for Hash Table Lookup](http://www.burtleburtle.net/bob/hash/doobs.html)