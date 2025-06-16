# Module 11: Linked Lists

## 📋 Overview

This module explores linked list data structures, a fundamental concept in computer science. You'll learn to implement different types of linked lists, understand their advantages and disadvantages compared to arrays, and master pointer manipulation and dynamic memory concepts.

## 🎯 Learning Objectives

- Implement singly and doubly linked lists
- Create self-sorting linked list data structures
- Understand pointer/reference manipulation in C#
- Master dynamic data structure operations
- Compare linked lists vs arrays performance characteristics
- Write comprehensive unit tests for data structures
- Apply linked lists to solve real-world problems

## 📁 Project Structure

```
module11/LinkedList/
├── Program.cs                      # Main demonstration program
├── User.cs                        # User data model
├── UserLinkedList.cs              # Basic singly linked list
├── UserLinkedList.Tests.cs        # Unit tests for singly linked list
├── DoubleUserLinkedList.cs        # Doubly linked list implementation
├── DoubleUserLinkedList.Tests.cs  # Unit tests for doubly linked list
├── SortedUserLinkedList.cs        # Auto-sorting linked list
├── SortedUserLinkedList.Tests.cs  # Unit tests for sorted linked list
└── list.csproj                    # Project configuration
```

## 👤 User Data Model

All linked list implementations work with User objects:

```csharp
public class User 
{
    public string Name { get; set; }
    public int Id { get; set; }

    public User(string name, int id) 
    {
        this.Name = name;
        this.Id = id;
    }
}
```

**Key Features:**
- **Simple Structure**: Name and ID properties
- **Constructor**: Ensures proper initialization
- **Reference Type**: Users are stored as references in lists

## 🔗 Singly Linked List Implementation

### Node Structure

Internal node class that links elements together:

```csharp
class Node
{
    public Node(User data, Node next)
    {
        this.Data = data;
        this.Next = next;
    }
    public User Data;
    public Node Next;
}
```

**Key Features:**
- **Data Storage**: Holds User object
- **Next Pointer**: Reference to next node in sequence
- **Constructor**: Initializes both data and next reference

### UserLinkedList Class

Basic singly linked list with essential operations:

```csharp
class UserLinkedList
{
    private Node first = null!;

    // Core operations
    public void AddFirst(User user)
    public User RemoveFirst()
    public void RemoveUser(User user)
    public User GetFirst()
    public User GetLast()
    public int CountUsers()
    public bool Contains(User user)
    public override String ToString()
}
```

### Core Operations

#### Add First (`AddFirst`)

Inserts a new user at the beginning of the list:

```csharp
public void AddFirst(User user)
{
    Node node = new Node(user, first);
    first = node;
}
```

**Time Complexity:** O(1)
**Example:**
```csharp
UserLinkedList list = new UserLinkedList();
list.AddFirst(new User("Alice", 1));
list.AddFirst(new User("Bob", 2));
// List: Bob -> Alice
```

#### Remove First (`RemoveFirst`)

Removes and returns the first user:

```csharp
public User RemoveFirst()
{
    User user = first.Data;
    first = first.Next;
    return user;
}
```

**Time Complexity:** O(1)
**Example:**
```csharp
User removed = list.RemoveFirst(); // Returns Bob
// List: Alice
```

#### Remove User (`RemoveUser`)

Removes a specific user by name:

```csharp
public void RemoveUser(User user)
{
    Node node = first;
    Node previous = null!;
    bool found = false;

    while (!found && node != null)
    {
        if (node.Data.Name == user.Name)
        {
            found = true;
            if (node == first)
            {
                RemoveFirst();
            }
            else
            {
                previous.Next = node.Next;
            }
        }
        else
        {
            previous = node;
            node = node.Next;
        }
    }
}
```

**Time Complexity:** O(n) - may need to traverse entire list

#### Count Users (`CountUsers`)

Returns the number of users in the list:

```csharp
public int CountUsers()
{
    Node node = first;
    int count = 0;
    while (node != null)
    {
        count++;
        node = node.Next;
    }
    return count;
}
```

**Time Complexity:** O(n)

#### Get Last (`GetLast`)

Returns the last user in the list:

```csharp
public User GetLast()
{
    while (first.Next != null)
    {
        first = first.Next;
    }
    return first.Data ?? null!;
}
```

**⚠️ Note:** This implementation modifies the `first` pointer, which is a bug!

#### Contains (`Contains`)

Checks if a user exists in the list:

```csharp
public bool Contains(User user)
{
    while (first != null)
    {
        if (first.Data.Name == user.Name)
        {
            return true;
        }
        first = first.Next;
    }
    return false!;
}
```

**⚠️ Note:** This implementation also modifies the `first` pointer!

## 🔗🔗 Doubly Linked List Implementation

### Enhanced Node Structure

Nodes with both forward and backward references:

```csharp
class Node
{
    public Node(User data, Node next, Node previous)
    {
        this.Data = data;
        this.Next = next;
        this.Previous = previous;
    }
    public User Data;
    public Node Next;
    public Node Previous;
}
```

### DoubleUserLinkedList Class

Doubly linked list with bidirectional traversal:

```csharp
class DoubleUserLinkedList
{
    private Node first = null!;
    private Node last = null!;

    // Operations
    public void AddFirst(User user)
    public void AddLast(User user)
    public User RemoveFirst()
    public User RemoveLast()
    public User GetFirst()
    public User GetLast()
    public User GetUser(int index)
}
```

### Key Operations

#### Add First (`AddFirst`)

Inserts at the beginning with proper link management:

```csharp
public void AddFirst(User user)
{
    Node firstNode = new Node(user, null!, null!);
    Node node = new Node(user, first, null!);
    if (first == null)
    {
        first = firstNode;
        last = firstNode;
    }
    else
    {
        first.Previous = node;
        first = node;
    }
}
```

#### Add Last (`AddLast`)

Inserts at the end efficiently:

```csharp
public void AddLast(User user)
{
    Node lastNode = new Node(user, null!, null!);
    Node node = new Node(user, null!, last);
    if (last == null)
    {
        first = lastNode;
        last = lastNode;
    }
    else
    {
        last.Next = node;
        last = node;
    }
}
```

#### Remove Last (`RemoveLast`)

Efficiently removes from the end:

```csharp
public User RemoveLast()
{
    User user = last.Data;
    last = last.Previous;
    last.Next = null!;
    return user;
}
```

**Advantage:** O(1) removal from end (vs O(n) in singly linked list)

## 📊 Sorted Linked List Implementation

### Auto-Sorting Features

Maintains sorted order based on User ID:

```csharp
class SortedUserLinkedList
{
    private Node first = null!;

    public void Add(User user)      // Inserts in sorted position
    public User RemoveFirst()       // Removes smallest element
    public void RemoveUser(User user)
    public User GetFirst()
    public int CountUsers()
    public User GetUser(int index)
}
```

### Sorted Insertion (`Add`)

Automatically finds correct position for new user:

```csharp
public void Add(User user)
{
    if (first == null || user.Id < first.Data.Id)
    {
        Node newNode = new Node(user, null!);
        first = newNode;
        return;
    }
    
    Node current = first;
    while (current.Next != null && current.Next.Data.Id < user.Id)
    {
        current = current.Next;
    }
    
    Node node = new Node(user, current.Next);
    current.Next = node;
}
```

**Time Complexity:** O(n) - must find correct insertion point
**Maintains Invariant:** List remains sorted by ID after insertion

**Example:**
```csharp
SortedUserLinkedList list = new SortedUserLinkedList();
list.Add(new User("Charlie", 3));  // List: [3]
list.Add(new User("Alice", 1));    // List: [1, 3]
list.Add(new User("Bob", 2));      // List: [1, 2, 3]
```

## 🧪 Comprehensive Unit Tests

### Singly Linked List Tests

```csharp
[TestClass]
public class LinkedList_Tests
{
    [TestMethod]
    public void TestAddFirst()
    {
        User kristian = new User("Kristian", 1);
        UserLinkedList list = new UserLinkedList();
        list.AddFirst(kristian);
        Assert.AreEqual(kristian, list.GetFirst());
    }

    [TestMethod]
    public void TestCountUsers()
    {
        UserLinkedList list = new UserLinkedList();
        list.AddFirst(new User("Kristian", 1));
        list.AddFirst(new User("Mads", 2));
        list.AddFirst(new User("Torill", 3));
        Assert.AreEqual(3, list.CountUsers());
    }

    [TestMethod]
    public void TestRemoveUser()
    {
        UserLinkedList list = new UserLinkedList();
        // Add 5 users
        list.AddFirst(new User("Kristian", 1));
        list.AddFirst(new User("Mads", 2));
        list.AddFirst(new User("Torill", 3));
        list.AddFirst(new User("Henrik", 5));
        list.AddFirst(new User("Klaus", 6));

        // Remove 2 users
        list.RemoveUser(new User("Mads", 2));
        Assert.AreEqual(4, list.CountUsers());
        
        list.RemoveUser(new User("Kristian", 1));
        Assert.AreEqual(3, list.CountUsers());
    }
}
```

### Doubly Linked List Tests

```csharp
[TestClass]
public class DoubleLinkedList_Tests
{
    [TestMethod]
    public void TestAddLast()
    {
        DoubleUserLinkedList list = new DoubleUserLinkedList();
        list.AddFirst(new User("Kristian", 1));
        list.AddFirst(new User("Mads", 2));
        list.AddFirst(new User("Torill", 3));
        list.AddLast(new User("Asger", 4));
        
        Assert.AreEqual("Asger", list.GetLast().Name);
    }

    [TestMethod]
    public void TestRemoveLast()
    {
        DoubleUserLinkedList list = new DoubleUserLinkedList();
        list.AddFirst(new User("Kristian", 1));
        list.AddFirst(new User("Mads", 2));
        list.AddFirst(new User("Torill", 3));
        
        User removed = list.RemoveLast();
        Assert.AreEqual("Kristian", removed.Name);
    }
}
```

### Sorted List Tests

```csharp
[TestClass]
public class SortedLinkedList_Tests
{
    [TestMethod]
    public void TestSortedAdd()
    {
        SortedUserLinkedList list = new SortedUserLinkedList();
        list.Add(new User("Kristian", 1));  // ID: 1
        list.Add(new User("Mads", 3));      // ID: 3
        list.Add(new User("Torill", 2));    // ID: 2
        
        // Should be sorted: [1, 2, 3]
        Assert.AreEqual("Mads", list.GetUser(2).Name);  // User with ID 3 at index 2
    }
}
```

## 🚀 Demo Program (`Program.cs`)

Demonstrates linked list operations:

```csharp
User kristian = new User("Kristian", 1);
User mads = new User("Mads", 2);
User torill = new User("Torill", 3);
User kell = new User("Kell", 4);
User henrik = new User("Henrik", 5);
User klaus = new User("Klaus", 6);

UserLinkedList list = new UserLinkedList();
list.AddFirst(kristian);
list.AddFirst(mads);
list.AddFirst(torill);
list.AddFirst(henrik);
list.AddFirst(klaus);

Console.WriteLine(list.CountUsers());  // Output: 5
Console.WriteLine(list);               // Output: Klaus, Henrik, Torill, Mads, Kristian,

list.RemoveUser(mads);
list.RemoveFirst();

Console.WriteLine(list.CountUsers());  // Output: 3
Console.WriteLine(list);               // Output: Henrik, Torill, Kristian,
```

## 📊 Performance Comparison

### Linked Lists vs Arrays

| Operation | Array | Linked List | Notes |
|-----------|-------|-------------|-------|
| **Access by Index** | O(1) | O(n) | Arrays have direct indexing |
| **Insert at Beginning** | O(n) | O(1) | Arrays need shifting |
| **Insert at End** | O(1)* | O(n) | *Amortized for dynamic arrays |
| **Delete at Beginning** | O(n) | O(1) | Arrays need shifting |
| **Delete at End** | O(1) | O(n) | Need to traverse to end |
| **Search** | O(n) | O(n) | Both require linear search |
| **Memory Usage** | Lower | Higher | Pointers add overhead |

### Linked List Types Comparison

| Feature | Singly Linked | Doubly Linked | Sorted Linked |
|---------|---------------|---------------|---------------|
| **Memory per Node** | 1 pointer | 2 pointers | 1 pointer |
| **Insert at End** | O(n) | O(1) | O(n) |
| **Remove from End** | O(n) | O(1) | O(n) |
| **Bidirectional Traversal** | ❌ | ✅ | ❌ |
| **Maintains Order** | Manual | Manual | Automatic |
| **Insert Complexity** | O(1) at start | O(1) at both ends | O(n) sorted position |

## 🔑 Key Concepts

### Pointer/Reference Manipulation

**Creating Links:**
```csharp
Node newNode = new Node(user, existingFirst);
first = newNode;  // New node becomes first
```

**Breaking Links:**
```csharp
Node nodeToRemove = first;
first = first.Next;  // Skip over node to remove
// nodeToRemove becomes unreachable and eligible for garbage collection
```

**Inserting in Middle:**
```csharp
Node newNode = new Node(user, current.Next);
current.Next = newNode;  // Links: current -> newNode -> current.Next
```

### Memory Management in C#

**Automatic Garbage Collection:**
- Removed nodes become unreachable
- Garbage collector automatically frees memory
- No manual memory management required (unlike C/C++)

**Reference vs Value Types:**
- Nodes store references to User objects
- Multiple nodes can reference same User
- Careful with shared references during removal

### Traversal Patterns

**Basic Traversal:**
```csharp
Node current = first;
while (current != null)
{
    ProcessUser(current.Data);
    current = current.Next;
}
```

**Traversal with Previous Tracking:**
```csharp
Node current = first;
Node previous = null;
while (current != null)
{
    if (ShouldRemove(current.Data))
    {
        if (previous == null)
            first = current.Next;  // Removing first
        else
            previous.Next = current.Next;  // Skip current
    }
    previous = current;
    current = current.Next;
}
```

## 🧠 Design Patterns and Best Practices

### Iterator Pattern Implementation

```csharp
public class UserLinkedList : IEnumerable<User>
{
    public IEnumerator<User> GetEnumerator()
    {
        Node current = first;
        while (current != null)
        {
            yield return current.Data;
            current = current.Next;
        }
    }
}

// Usage:
foreach (User user in list)
{
    Console.WriteLine(user.Name);
}
```

### Generic Implementation

```csharp
public class LinkedList<T>
{
    private class Node
    {
        public T Data { get; set; }
        public Node Next { get; set; }
        
        public Node(T data, Node next = null)
        {
            Data = data;
            Next = next;
        }
    }
    
    private Node first = null;
    
    public void Add(T item) { /* implementation */ }
    public bool Remove(T item) { /* implementation */ }
    public bool Contains(T item) { /* implementation */ }
}
```

## 🎓 Practice Exercises

### Beginner Level

1. **Fix GetLast Bug**: Implement GetLast without modifying the first pointer
2. **Fix Contains Bug**: Implement Contains without modifying the list
3. **Add Size Property**: Maintain count without traversing entire list
4. **Implement Clear**: Remove all elements from the list

### Intermediate Level

5. **Circular Linked List**: Last node points back to first
6. **Reverse List**: Reverse the order of all nodes
7. **Merge Two Lists**: Combine two sorted lists into one sorted list
8. **Find Middle Element**: Find middle element in single traversal

### Advanced Level

9. **Remove Duplicates**: Remove duplicate users (same name or ID)
10. **Implement Stack/Queue**: Use linked list as underlying structure
11. **LRU Cache**: Implement Least Recently Used cache with linked list
12. **Skip List**: Implement probabilistic data structure for faster search

### Expert Level

13. **Thread-Safe List**: Implement concurrent linked list with locks
14. **Memory Pool**: Reuse node objects to reduce garbage collection
15. **Persistent List**: Implement immutable linked list with structural sharing
16. **Lock-Free List**: Implement using atomic operations and compare-and-swap

## 🔧 Common Issues and Solutions

### Memory Leaks (Not in C#, but concept important)

**Problem:** Circular references in doubly linked lists
```csharp
// Potential issue if implementing custom cleanup
node.Previous.Next = node.Next;
node.Next.Previous = node.Previous;
// Don't forget to clear node's references:
node.Next = null;
node.Previous = null;
```

### Null Reference Exceptions

**Problem:** Accessing properties on null nodes
```csharp
// ❌ Dangerous
public User GetLast()
{
    return first.Next.Data;  // Crashes if first or first.Next is null
}

// ✅ Safe
public User GetLast()
{
    if (first == null) return null;
    
    Node current = first;
    while (current.Next != null)
    {
        current = current.Next;
    }
    return current.Data;
}
```

### Infinite Loops

**Problem:** Incorrect loop conditions
```csharp
// ❌ Wrong: modifies loop variable
while (first != null)
{
    Console.WriteLine(first.Data.Name);
    first = first.Next;  // Destroys the list!
}

// ✅ Correct: use temporary variable
Node current = first;
while (current != null)
{
    Console.WriteLine(current.Data.Name);
    current = current.Next;
}
```

## 🚀 How to Run

### 1. Navigate to Project Directory
```bash
cd module11/LinkedList
```

### 2. Run Unit Tests
```bash
# Run all tests
dotnet test

# Run specific test class
dotnet test --filter LinkedList_Tests

# Run with detailed output
dotnet test --verbosity normal
```

### 3. Run Demo Program
```bash
# Build and run
dotnet run
```

**Expected Output:**
```
5
Klaus, Henrik, Torill, Mads, Kristian, 
3
Henrik, Torill, Kristian, 
```

## 📚 Further Reading

- [Data Structures and Algorithms in C#](https://www.manning.com/books/algorithms-and-data-structures-in-action)
- [Linked Lists vs Arrays Performance](https://stackoverflow.com/questions/393556/when-to-use-linkedlist-over-arraylist-in-java)
- [Memory Management in .NET](https://docs.microsoft.com/en-us/dotnet/standard/automatic-memory-management)
- [Generic Collections in C#](https://docs.microsoft.com/en-us/dotnet/standard/collections/commonly-used-collection-types)
- [Iterator Pattern in C#](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/iterators)
- [Lock-Free Programming](https://preshing.com/20120612/an-introduction-to-lock-free-programming/)