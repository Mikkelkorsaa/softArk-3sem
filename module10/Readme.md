# Module 10: Search Algorithms

## 📋 Overview

This module introduces fundamental search algorithms essential for computer science and software development. You'll learn to implement and analyze different search strategies, understand their time complexities, and apply them to solve real-world problems efficiently.

## 🎯 Learning Objectives

- Implement linear and binary search algorithms
- Understand time complexity analysis (Big O notation)
- Master sorted array insertion techniques
- Write comprehensive unit tests for algorithms
- Compare algorithm performance characteristics
- Apply search algorithms to practical problems

## 📁 Project Structure

```
module10/search/
├── Search.cs              # Main search algorithm implementations
├── Search.Tests.cs        # Comprehensive unit tests
├── search.csproj         # Project configuration with testing framework
└── README.md             # This documentation
```

## 🔍 Search Algorithms Implemented

### 1. Linear Search (`FindNumberLinear`)

**Algorithm:** Sequential search through array elements

**Signature:** `public static int FindNumberLinear(int[] array, int tal)`

**Time Complexity:** O(n) - worst case checks every element

**How it works:**
```csharp
public static int FindNumberLinear(int[] array, int tal)
{
    for (int i = 0; i < array.Length; i++)
    {
        if (array[i] == tal)
        {
            return i;  // Return index when found
        }
    }
    return -1;  // Return -1 if not found
}
```

**Examples:**
```csharp
int[] array = { 5, 12, 14, 20, 26, 36, 37, 43, 44, 46 };

int index = Search.FindNumberLinear(array, 20);  // Returns: 3
int notFound = Search.FindNumberLinear(array, 99);  // Returns: -1
int firstElement = Search.FindNumberLinear(array, 5);   // Returns: 0
int lastElement = Search.FindNumberLinear(array, 46);   // Returns: 9
```

**Use Cases:**
- Unsorted arrays
- Small datasets
- When simplicity is more important than performance
- One-time searches

### 2. Binary Search (`FindNumberBinary`)

**Algorithm:** Divide-and-conquer on sorted arrays

**Signature:** `public static int FindNumberBinary(int[] array, int tal)`

**Time Complexity:** O(log n) - eliminates half the search space each iteration

**Prerequisites:** Array must be sorted in ascending order

**How it works:**
```csharp
public static int FindNumberBinary(int[] array, int tal)
{
    int min = 0;
    int max = array.Length - 1;
    
    while (min <= max)
    {
        int mid = (min + max) / 2;  // Find middle point
        
        if (array[mid] == tal)
        {
            return mid;  // Found target
        }
        else if (array[mid] < tal)
        {
            min = mid + 1;  // Search right half
        }
        else
        {
            max = mid - 1;  // Search left half
        }
    }
    return -1;  // Not found
}
```

**Step-by-step example:**
```csharp
// Searching for 26 in [5, 12, 14, 20, 26, 36, 37, 43, 44, 46]
// Step 1: min=0, max=9, mid=4, array[4]=26 ✓ Found!

// Searching for 37 in [5, 12, 14, 20, 26, 36, 37, 43, 44, 46]
// Step 1: min=0, max=9, mid=4, array[4]=26 < 37, search right
// Step 2: min=5, max=9, mid=7, array[7]=43 > 37, search left  
// Step 3: min=5, max=6, mid=5, array[5]=36 < 37, search right
// Step 4: min=6, max=6, mid=6, array[6]=37 ✓ Found!
```

**Examples:**
```csharp
int[] sortedArray = { 5, 12, 14, 20, 26, 36, 37, 43, 44, 46 };

int index = Search.FindNumberBinary(sortedArray, 26);   // Returns: 4
int notFound = Search.FindNumberBinary(sortedArray, 99); // Returns: -1
int first = Search.FindNumberBinary(sortedArray, 5);     // Returns: 0
int last = Search.FindNumberBinary(sortedArray, 46);     // Returns: 9
```

**Use Cases:**
- Large sorted datasets
- Repeated searches on same data
- When search performance is critical
- Database indexes and search trees

## 📝 Sorted Array Insertion

### 3. Sorted Insertion (`InsertSorted`)

**Algorithm:** Insert element while maintaining sorted order

**Signature:** `public static int[] InsertSorted(int tal)`

**Time Complexity:** O(n) - may need to shift elements

**How it works:**
```csharp
public static int[] InsertSorted(int tal)
{
    if (sortedArray.Length == next)  // Array is full
    {
        return sortedArray;
    }
    
    int i = next - 1;
    // Shift elements right until correct position found
    while (sortedArray[i] > tal)
    {
        sortedArray[i + 1] = sortedArray[i];
        i--;
        if (i == -1) break;  // Reached beginning
    }
    
    sortedArray[i + 1] = tal;  // Insert at correct position
    next++;
    return sortedArray;
}
```

**Example walkthrough:**
```csharp
// Initial array: [2, 4, 8, 10, 15, 17, -1, -1, -1, -1], next = 6
Search.InitSortedArray(array, 6);

// Insert 11:
// Step 1: Compare with 17 (position 5): 11 < 17, shift 17 right
// Step 2: Compare with 15 (position 4): 11 < 15, shift 15 right  
// Step 3: Compare with 10 (position 3): 11 > 10, insert here
// Result: [2, 4, 8, 10, 11, 15, 17, -1, -1, -1]

int[] result = Search.InsertSorted(11);
// Returns: [2, 4, 8, 10, 11, 15, 17, -1, -1, -1]
```

**Supporting Methods:**
```csharp
// Initialize the sorted array for insertion operations
public static void InitSortedArray(int[] sortedArray, int next)
{
    Search.sortedArray = sortedArray;
    Search.next = next;
}
```

## 🧪 Comprehensive Unit Tests

The module includes thorough unit tests covering all scenarios:

### Linear Search Tests
```csharp
[TestMethod]
public void TestFindNumberLinear()
{
    var array = new int[] { 5, 12, 14, 20, 26, 36, 37, 43, 44, 46, 47, 50, 51, 53, 57, 78, 80, 92, 93, 95 };

    // Test first element
    int res = Search.FindNumberLinear(array, array[0]);
    Assert.AreEqual(0, res);

    // Test middle element  
    res = Search.FindNumberLinear(array, array[6]);
    Assert.AreEqual(6, res);

    // Test last element
    res = Search.FindNumberLinear(array, array[array.Length - 1]);
    Assert.AreEqual(array.Length - 1, res);

    // Test element not in array
    res = Search.FindNumberLinear(array, 91);
    Assert.AreEqual(-1, res);
}
```

### Binary Search Tests
```csharp
[TestMethod]
public void TestFindNumberBinary()
{
    var array = new int[] { 5, 12, 14, 20, 26, 36, 37, 43, 44, 46, 47, 50, 51, 53, 57, 78, 80, 92, 93, 95 };

    // Test boundary conditions
    int res = Search.FindNumberBinary(array, array[0]);      // First
    Assert.AreEqual(0, res);
    
    res = Search.FindNumberBinary(array, array[6]);          // Middle
    Assert.AreEqual(6, res);
    
    res = Search.FindNumberBinary(array, array[array.Length - 1]); // Last
    Assert.AreEqual(array.Length - 1, res);

    // Test missing element
    res = Search.FindNumberBinary(array, 91);
    Assert.AreEqual(-1, res);
}
```

### Sorted Insertion Tests
```csharp
[TestMethod]
public void TestInsertSorted()
{
    Search.InitSortedArray(
        new int[] { 2, 4, 8, 10, 15, 17, -1, -1, -1, -1 }, 
        6
    );

    // Insert in middle
    int[] newArray = Search.InsertSorted(11);
    CollectionAssert.AreEqual(
        new int[] { 2, 4, 8, 10, 11, 15, 17, -1, -1, -1 },
        newArray
    );

    // Insert at end
    newArray = Search.InsertSorted(19);
    CollectionAssert.AreEqual(
        new int[] { 2, 4, 8, 10, 11, 15, 17, 19, -1, -1 },
        newArray
    );

    // Insert at beginning
    newArray = Search.InsertSorted(1);
    CollectionAssert.AreEqual(
        new int[] { 1, 2, 4, 8, 10, 11, 15, 17, 19, -1 },
        newArray
    );
}
```

## 📊 Algorithm Comparison

| Algorithm | Time Complexity | Space Complexity | Prerequisites | Best Use Case |
|-----------|----------------|------------------|---------------|---------------|
| **Linear Search** | O(n) | O(1) | None | Small/unsorted arrays |
| **Binary Search** | O(log n) | O(1) | Sorted array | Large sorted datasets |
| **Sorted Insert** | O(n) | O(1) | Maintaining order | Dynamic sorted collections |

### Performance Analysis

**Linear Search:**
- **Best Case**: O(1) - element is first
- **Average Case**: O(n/2) - element in middle
- **Worst Case**: O(n) - element is last or not found

**Binary Search:**
- **Best Case**: O(1) - element is in middle
- **Average Case**: O(log n) - typical case
- **Worst Case**: O(log n) - consistent performance

**Example Performance:**
```
Array Size: 1,000,000 elements

Linear Search (worst case): 1,000,000 comparisons
Binary Search (worst case): ~20 comparisons (log₂ 1,000,000)

Speedup: 50,000x faster!
```

## 🚀 How to Run

### 1. Navigate to Project Directory
```bash
cd module10/search
```

### 2. Run Unit Tests
```bash
# Run all tests
dotnet test

# Run tests with detailed output
dotnet test --verbosity normal

# Run specific test method
dotnet test --filter TestFindNumberBinary
```

### 3. Build and Execute
```bash
# Build the project
dotnet build

# Run the program (if it has a Main method)
dotnet run
```

## 📦 Project Configuration

**search.csproj:**
```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net6.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
    <RootNamespace>SearchMethods.Tests</RootNamespace>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.NET.Test.Sdk" Version="16.11.0" />
    <PackageReference Include="MSTest.TestAdapter" Version="2.2.8" />
    <PackageReference Include="MSTest.TestFramework" Version="2.2.8" />
    <PackageReference Include="coverlet.collector" Version="3.1.0" />
  </ItemGroup>
</Project>
```

## 🎯 Practical Applications

### Real-World Use Cases

**Linear Search:**
- Finding a contact in an unsorted phone book
- Searching through log files
- Looking for a specific item in a shopping list
- Checking if a value exists in a small dataset

**Binary Search:**
- Dictionary/phone book lookup (sorted by name)
- Finding a page in a book (pages are numbered)
- Searching in database indexes
- Finding a file in a sorted directory listing
- Version control systems (git bisect)

**Sorted Insertion:**
- Maintaining a leaderboard
- Priority queues
- Insertion sort algorithm
- Real-time data processing with ordering

### Example Implementation

```csharp
// Real-world example: Student grade management
public class GradeManager
{
    private int[] grades = new int[100];
    private int count = 0;

    public void AddGrade(int grade)
    {
        // Use sorted insertion to maintain order
        // This allows for efficient searching later
        InsertSorted(grade);
    }

    public bool HasGrade(int grade)
    {
        // Use binary search for efficient lookup
        return FindNumberBinary(grades, grade) != -1;
    }

    public int FindGradePosition(int grade)
    {
        // Returns position of grade in sorted list
        return FindNumberBinary(grades, grade);
    }
}
```

## 🔧 Implementation Details

### Edge Cases Handled

**Array Boundary Conditions:**
```csharp
// Empty array
int[] empty = {};
Assert.AreEqual(-1, Search.FindNumberLinear(empty, 5));

// Single element array
int[] single = {42};
Assert.AreEqual(0, Search.FindNumberLinear(single, 42));
Assert.AreEqual(-1, Search.FindNumberLinear(single, 99));
```

**Insertion Edge Cases:**
```csharp
// Full array handling
Search.InitSortedArray(fullArray, fullArray.Length);
var result = Search.InsertSorted(99);  // Should return unchanged array

// Insert at boundaries
Search.InitSortedArray(array, count);
Search.InsertSorted(0);    // Insert at beginning
Search.InsertSorted(999);  // Insert at end
```

### Memory Management

**Static Array Approach:**
```csharp
private static int[] sortedArray = new int[10];  // Fixed size
private static int next = 0;  // Current size tracker
```

**Benefits:**
- ✅ Simple implementation
- ✅ Predictable memory usage
- ✅ No dynamic allocation overhead

**Limitations:**
- ❌ Fixed maximum size
- ❌ Not thread-safe
- ❌ Global state

## 🎓 Practice Exercises

### Beginner Level

1. **Modified Linear Search**: Return all indices where element appears
2. **Counting Search**: Count occurrences of element in array
3. **Range Search**: Find all elements between two values
4. **Reverse Binary Search**: Implement for descending sorted arrays

### Intermediate Level

5. **First/Last Occurrence**: Find first and last occurrence in sorted array with duplicates
6. **Insertion Point**: Return where element should be inserted in sorted array
7. **Closest Element**: Find element closest to target value
8. **Search in Rotated Array**: Binary search in rotated sorted array

### Advanced Level

9. **2D Array Search**: Search in row and column-wise sorted 2D array
10. **Exponential Search**: Combine with binary search for unknown array size
11. **Interpolation Search**: Use value estimation for uniformly distributed data
12. **Ternary Search**: Divide array into three parts instead of two

### Expert Level

13. **Parallel Binary Search**: Implement multi-threaded binary search
14. **Cache-Efficient Search**: Optimize for CPU cache performance
15. **Probabilistic Search**: Implement skip lists or bloom filters
16. **External Search**: Handle arrays too large for memory

## 🧠 Algorithm Analysis Deep Dive

### Time Complexity Visualization

```
Linear Search: O(n)
┌─────────────────────────────────────┐
│ ■ ■ ■ ■ ■ ■ ■ ■ ■ ■ (check each)  │
└─────────────────────────────────────┘
Elements checked: n (worst case)

Binary Search: O(log n)
┌─────────────────────────────────────┐
│ ■ ■ ■ ■ ■ ■ ■ ■ ■ ■                │
│         ↑                           │
│    ■ ■ ■ ■ ■                       │
│       ↑                             │
│     ■ ■                             │
│      ↑                              │
│     ■ (found)                       │
└─────────────────────────────────────┘
Elements checked: log₂(n)
```

### Space Complexity Analysis

**Linear Search**: O(1)
- Only uses index variable
- No additional arrays or recursion

**Binary Search**: O(1)
- Uses three variables (min, max, mid)
- Iterative implementation avoids recursion overhead

**Sorted Insertion**: O(1)
- In-place shifting of elements
- No additional storage required

## 🔧 Debugging and Testing Tips

### Common Mistakes

**Off-by-One Errors:**
```csharp
// ❌ Wrong: might miss last element
while (min < max)  

// ✅ Correct: includes case where min == max
while (min <= max)
```

**Integer Overflow:**
```csharp
// ❌ Potential overflow for large arrays
int mid = (min + max) / 2;

// ✅ Safer approach
int mid = min + (max - min) / 2;
```

**Array Bounds:**
```csharp
// ❌ Dangerous: no bounds checking
if (array[i] == target)

// ✅ Safe: check bounds first
if (i < array.Length && array[i] == target)
```

### Testing Strategies

**Boundary Testing:**
- Test with empty arrays
- Test with single-element arrays
- Test first and last elements
- Test elements not in array

**Performance Testing:**
```csharp
var stopwatch = System.Diagnostics.Stopwatch.StartNew();
for (int i = 0; i < 1000000; i++)
{
    Search.FindNumberBinary(largeArray, randomTarget);
}
stopwatch.Stop();
Console.WriteLine($"Time: {stopwatch.ElapsedMilliseconds}ms");
```

## 📚 Further Reading

- [Introduction to Algorithms (CLRS)](https://mitpress.mit.edu/books/introduction-algorithms-third-edition)
- [Algorithm Design Manual](https://www.algorist.com/)
- [Big O Notation Explained](https://www.bigocheatsheet.com/)
- [Binary Search Variations](https://leetcode.com/explore/learn/card/binary-search/)
- [Search Algorithms Visualization](https://www.cs.usfca.edu/~galles/visualization/Search.html)
- [Performance Analysis of Search Algorithms](https://www.geeksforgeeks.org/searching-algorithms/)