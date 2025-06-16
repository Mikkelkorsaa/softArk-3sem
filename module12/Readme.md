# Module 12: Sorting Algorithms

## 📋 Overview

This module explores fundamental sorting algorithms essential for computer science and software development. You'll learn to implement various sorting strategies, analyze their performance characteristics, and understand when to use each algorithm based on data size, memory constraints, and stability requirements.

## 🎯 Learning Objectives

- Implement five major sorting algorithms with different approaches
- Understand time and space complexity trade-offs
- Analyze algorithm performance through practical benchmarking
- Compare stable vs unstable sorting algorithms
- Master divide-and-conquer and iterative sorting strategies
- Write comprehensive unit tests for algorithms
- Apply sorting algorithms to solve real-world problems

## 📁 Project Structure

```
module12/Sortering/
├── Program.cs              # Main demonstration program
├── BubbleSort.cs          # O(n²) comparison-based sorting
├── InsertionSort.cs       # O(n²) adaptive sorting
├── SelectionSort.cs       # O(n²) minimal swaps sorting
├── QuickSort.cs           # O(n log n) divide-and-conquer
├── MergeSort.cs           # O(n log n) stable divide-and-conquer
├── SortTester.cs          # Performance benchmarking tool
├── Sortering.Tests.cs     # Comprehensive unit tests
└── sortering.csproj       # Project configuration
```

## 📊 Algorithms Overview

| Algorithm | Time Complexity | Space Complexity | Stable | In-Place | Best Use Case |
|-----------|----------------|------------------|--------|----------|---------------|
| **Bubble Sort** | O(n²) | O(1) | ✅ | ✅ | Educational, small datasets |
| **Insertion Sort** | O(n²) | O(1) | ✅ | ✅ | Small/nearly sorted data |
| **Selection Sort** | O(n²) | O(1) | ❌ | ✅ | Memory-constrained environments |
| **Quick Sort** | O(n log n) | O(log n) | ❌ | ✅ | General purpose, average case |
| **Merge Sort** | O(n log n) | O(n) | ✅ | ❌ | Guaranteed performance, external sorting |

## 🫧 Bubble Sort Implementation

**Algorithm:** Repeatedly steps through the list, compares adjacent elements and swaps them if they're in wrong order.

**BubbleSort.cs:**
```csharp
namespace Sortering;

public class BubbleSort
{
    private static void Swap(int[] array, int i, int j)
    {
        int temp = array[i];
        array[i] = array[j];
        array[j] = temp;
    }

    public static void Sort(int[] array)
    {
        for (int i = array.Length - 1; i >= 0; i--)
        {
            for (int j = 0; j <= i - 1; j++)
            {
                if (array[j] > array[j + 1])
                {
                    Swap(array, j, j + 1);
                }
            }
        }
    }
}
```

**How it works:**
1. **Outer Loop**: Reduces the range of unsorted elements
2. **Inner Loop**: Bubbles largest element to its correct position
3. **Comparison**: Adjacent elements compared and swapped if needed
4. **Optimization**: Each pass places one element in final position

**Example Trace:**
```
Initial: [64, 34, 25, 12, 22, 11, 90]

Pass 1: [34, 25, 12, 22, 11, 64, 90]  // 90 bubbles to end
Pass 2: [25, 12, 22, 11, 34, 64, 90]  // 64 bubbles to position
Pass 3: [12, 22, 11, 25, 34, 64, 90]  // 34 bubbles to position
Pass 4: [12, 11, 22, 25, 34, 64, 90]  // 25 bubbles to position
Pass 5: [11, 12, 22, 25, 34, 64, 90]  // 22 bubbles to position
Pass 6: [11, 12, 22, 25, 34, 64, 90]  // 12 in correct position

Final:  [11, 12, 22, 25, 34, 64, 90]
```

**Characteristics:**
- **Time Complexity**: O(n²) in all cases
- **Space Complexity**: O(1) - sorts in place
- **Stable**: Yes - equal elements maintain relative order
- **Adaptive**: No - always performs O(n²) comparisons

## 📥 Insertion Sort Implementation

**Algorithm:** Builds the final sorted array one item at a time, inserting each element into its correct position.

**InsertionSort.cs:**
```csharp
namespace Sortering;

public class InsertionSort
{
    public static void Sort(int[] array)
    {
        for (int i = 1; i < array.Length; i++)
        {
            int next = array[i];      // Element to insert
            int j = i;
            bool found = false;

            while (!found && j > 0)
            {
                if (next >= array[j - 1])
                {
                    found = true;     // Found correct position
                }
                else
                {
                    array[j] = array[j - 1];  // Shift element right
                    j--;
                }
            }
            array[j] = next;          // Insert element
        }
    }
}
```

**How it works:**
1. **Iterate**: Through array starting from second element
2. **Store**: Current element to be inserted
3. **Shift**: Larger elements to the right
4. **Insert**: Element at correct position in sorted portion

**Example Trace:**
```
Initial: [64, 34, 25, 12, 22]

Step 1: [34, 64, 25, 12, 22]  // Insert 34
Step 2: [25, 34, 64, 12, 22]  // Insert 25
Step 3: [12, 25, 34, 64, 22]  // Insert 12
Step 4: [12, 22, 25, 34, 64]  // Insert 22

Final:  [12, 22, 25, 34, 64]
```

**Characteristics:**
- **Time Complexity**: O(n²) worst case, O(n) best case (already sorted)
- **Space Complexity**: O(1) - sorts in place
- **Stable**: Yes - maintains relative order of equal elements
- **Adaptive**: Yes - performs better on nearly sorted data

## 🎯 Selection Sort Implementation

**Algorithm:** Finds the minimum element and places it at the beginning, then repeats for remaining elements.

**SelectionSort.cs:**
```csharp
namespace Sortering;

public class SelectionSort
{
    private static void Swap(int[] array, int i, int j)
    {
        int temp = array[i];
        array[i] = array[j];
        array[j] = temp;
    }

    public static void Sort(int[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            int min = i;                    // Assume current is minimum
            for (int j = i + 1; j < array.Length; j++)
            {
                if (array[j] < array[min])
                {
                    min = j;                // Found new minimum
                }
            }
            Swap(array, i, min);           // Place minimum at position i
        }
    }
}
```

**How it works:**
1. **Find Minimum**: Search for smallest element in unsorted portion
2. **Swap**: Place minimum at beginning of unsorted portion
3. **Repeat**: Continue with remaining unsorted elements
4. **Invariant**: Sorted portion grows from left to right

**Example Trace:**
```
Initial: [64, 25, 12, 22, 11]

Pass 1: [11, 25, 12, 22, 64]  // Swap 11 with 64
Pass 2: [11, 12, 25, 22, 64]  // Swap 12 with 25
Pass 3: [11, 12, 22, 25, 64]  // Swap 22 with 25
Pass 4: [11, 12, 22, 25, 64]  // 25 already in position

Final:  [11, 12, 22, 25, 64]
```

**Characteristics:**
- **Time Complexity**: O(n²) in all cases
- **Space Complexity**: O(1) - sorts in place
- **Stable**: No - may change relative order of equal elements
- **Minimal Swaps**: Performs only O(n) swaps

## ⚡ Quick Sort Implementation

**Algorithm:** Divide-and-conquer approach that partitions array around a pivot element.

**QuickSort.cs:**
```csharp
namespace Sortering;

public static class QuickSort
{
    private static void Swap(int[] array, int k, int j)
    {
        int tmp = array[k];
        array[k] = array[j];
        array[j] = tmp;
    }

    private static void _quickSort(int[] array, int low, int high)
    {
        if (low < high)
        {
            int pivot = Partition(array, low, high);
            _quickSort(array, low, pivot - 1);      // Sort left partition
            _quickSort(array, pivot + 1, high);     // Sort right partition
        }
    }

    private static int Partition(int[] array, int low, int high)
    {
        int pivot = array[low];     // Choose first element as pivot
        int i = low + 1;
        int j = high;
        
        while (i <= j)
        {
            if (array[i] <= pivot)
            {
                i++;                // Element in correct partition
            }
            else if (array[j] > pivot)
            {
                j--;                // Element in correct partition
            }
            else
            {
                Swap(array, i, j);  // Swap misplaced elements
                i++;
                j--;
            }
        }
        Swap(array, low, j);        // Place pivot in final position
        return j;
    }

    public static void Sort(int[] array)
    {
        _quickSort(array, 0, array.Length - 1);
    }
}
```

**How it works:**
1. **Choose Pivot**: Select pivot element (first element in this implementation)
2. **Partition**: Rearrange array so elements < pivot are left, > pivot are right
3. **Recursion**: Recursively sort left and right partitions
4. **Base Case**: Single element or empty array is already sorted

**Partitioning Example:**
```
Array: [33, 15, 46, 27, 89, 12, 75]
Pivot: 33

After Partition: [15, 27, 12, 33, 89, 46, 75]
                              ↑
                           pivot position

Left partition: [15, 27, 12]    // Elements ≤ 33
Right partition: [89, 46, 75]   // Elements > 33
```

**Characteristics:**
- **Time Complexity**: O(n log n) average, O(n²) worst case
- **Space Complexity**: O(log n) due to recursion stack
- **Stable**: No - may change relative order of equal elements
- **In-Place**: Yes - sorts with minimal extra memory

## 🔗 Merge Sort Implementation

**Algorithm:** Divide-and-conquer that recursively divides array into halves, sorts them, and merges results.

**MergeSort.cs:**
```csharp
using System;
using System.Collections.Generic;

namespace Sortering;

public static class MergeSort
{
    public static void Sort(List<int> list)
    {
        if (list == null || list.Count <= 1)
            return;
            
        _mergeSort(list, 0, list.Count - 1);
    }

    private static void _mergeSort(List<int> list, int left, int right)
    {
        if (left < right)
        {
            int middle = (left + right) / 2;
            _mergeSort(list, left, middle);         // Sort left half
            _mergeSort(list, middle + 1, right);    // Sort right half
            Merge(list, left, middle, right);       // Merge sorted halves
        }
    }

    private static void Merge(List<int> list, int left, int middle, int right)
    {
        // Create temporary lists for left and right partitions
        List<int> leftList = new List<int>();
        List<int> rightList = new List<int>();
        
        // Copy data to temporary lists
        for (int i = left; i <= middle; i++)
            leftList.Add(list[i]);
            
        for (int i = middle + 1; i <= right; i++)
            rightList.Add(list[i]);
            
        int leftIndex = 0;
        int rightIndex = 0;
        int currentIndex = left;
        
        // Merge the two lists back into original
        while (leftIndex < leftList.Count && rightIndex < rightList.Count)
        {
            if (leftList[leftIndex] <= rightList[rightIndex])
            {
                list[currentIndex] = leftList[leftIndex];
                leftIndex++;
            }
            else
            {
                list[currentIndex] = rightList[rightIndex];
                rightIndex++;
            }
            currentIndex++;
        }
        
        // Copy remaining elements from leftList
        while (leftIndex < leftList.Count)
        {
            list[currentIndex] = leftList[leftIndex];
            leftIndex++;
            currentIndex++;
        }
        
        // Copy remaining elements from rightList
        while (rightIndex < rightList.Count)
        {
            list[currentIndex] = rightList[rightIndex];
            rightIndex++;
            currentIndex++;
        }
    }
}
```

**How it works:**
1. **Divide**: Split array into two halves
2. **Conquer**: Recursively sort both halves
3. **Merge**: Combine sorted halves into single sorted array
4. **Base Case**: Array of size 1 or 0 is already sorted

**Merge Process Example:**
```
Left:  [12, 25, 34]
Right: [11, 22, 64]

Merge Process:
Step 1: Compare 12 vs 11 → take 11 → [11]
Step 2: Compare 12 vs 22 → take 12 → [11, 12]
Step 3: Compare 25 vs 22 → take 22 → [11, 12, 22]
Step 4: Compare 25 vs 64 → take 25 → [11, 12, 22, 25]
Step 5: Compare 34 vs 64 → take 34 → [11, 12, 22, 25, 34]
Step 6: Only 64 remains → take 64 → [11, 12, 22, 25, 34, 64]
```

**Characteristics:**
- **Time Complexity**: O(n log n) in all cases
- **Space Complexity**: O(n) - requires additional storage
- **Stable**: Yes - maintains relative order of equal elements
- **Predictable**: Guaranteed O(n log n) performance

## 🏃‍♂️ Performance Testing (`SortTester.cs`)

Comprehensive benchmarking tool that tests all algorithms:

```csharp
using System.Diagnostics;
using System.Collections.Generic;

namespace Sortering;

public class SortTester
{
    public static void Run()
    {
        int testSize = 100000;          // Large dataset for meaningful results
        int Min = 0;
        int Max = 10000;
        Random randNum = new Random();

        // Generate random test data
        int[] bigArray = Enumerable
            .Repeat(0, testSize)
            .Select(i => randNum.Next(Min, Max))
            .ToArray();

        // Create copies for each algorithm
        int[] bigArray1 = (int[])bigArray.Clone();     // Bubble Sort (commented out)
        int[] bigArray2 = (int[])bigArray.Clone();     // Insertion Sort
        int[] bigArray3 = (int[])bigArray.Clone();     // Selection Sort
        List<int> bigArray4 = new List<int>(bigArray); // Merge Sort
        int[] bigArray5 = (int[])bigArray.Clone();     // Quick Sort

        Stopwatch stopWatch = new Stopwatch();

        // Bubble Sort Test (commented out due to poor performance)
        stopWatch.Start();
        // BubbleSort.Sort(bigArray1);  // Would take too long!
        stopWatch.Stop();
        TimeSpan ts = stopWatch.Elapsed;
        string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
        Console.WriteLine("BubbleSort " + elapsedTime);

        // Insertion Sort Test
        stopWatch.Reset();
        stopWatch.Start();
        InsertionSort.Sort(bigArray2);
        stopWatch.Stop();
        ts = stopWatch.Elapsed;
        elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
        Console.WriteLine("InsertionSort " + elapsedTime);

        // Selection Sort Test
        stopWatch.Reset();
        stopWatch.Start();
        SelectionSort.Sort(bigArray3);
        stopWatch.Stop();
        ts = stopWatch.Elapsed;
        elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
        Console.WriteLine("SelectionSort " + elapsedTime);

        // Merge Sort Test
        stopWatch.Reset();
        stopWatch.Start();
        MergeSort.Sort(bigArray4);
        stopWatch.Stop();
        ts = stopWatch.Elapsed;
        elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
        Console.WriteLine("MergeSort " + elapsedTime);

        // Quick Sort Test
        stopWatch.Reset();
        stopWatch.Start();
        QuickSort.Sort(bigArray5);
        stopWatch.Stop();
        ts = stopWatch.Elapsed;
        elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
        Console.WriteLine("QuickSort " + elapsedTime);
    }
}
```

**Sample Performance Results** (100,000 random integers):
```
BubbleSort 00:00:00.00     // Skipped - would take too long
InsertionSort 00:00:12.50  // ~12.5 seconds
SelectionSort 00:00:08.75  // ~8.75 seconds  
MergeSort 00:00:00.15      // ~0.15 seconds
QuickSort 00:00:00.12      // ~0.12 seconds
```

## 🧪 Comprehensive Unit Tests

**Sortering.Tests.cs:**
```csharp
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sortering.Tests
{
    [TestClass]
    public class Sortering_Tests
    {
        private readonly int[] testArray = { 34, 18, 15, 45, 67, 11 };
        private readonly int[] expectedResult = { 11, 15, 18, 34, 45, 67 };

        [TestMethod]
        public void TestBubbleSort()
        {
            int[] array = (int[])testArray.Clone();
            BubbleSort.Sort(array);
            CollectionAssert.AreEqual(expectedResult, array);
        }

        [TestMethod]
        public void TestInsertionSort()
        {
            int[] array = (int[])testArray.Clone();
            InsertionSort.Sort(array);
            CollectionAssert.AreEqual(expectedResult, array);
        }

        [TestMethod]
        public void TestSelectionSort()
        {
            int[] array = (int[])testArray.Clone();
            SelectionSort.Sort(array);
            CollectionAssert.AreEqual(expectedResult, array);
        }

        [TestMethod]
        public void TestQuickSort()
        {
            int[] array = (int[])testArray.Clone();
            QuickSort.Sort(array);
            CollectionAssert.AreEqual(expectedResult, array);
        }

        [TestMethod]
        public void TestMergeSort()
        {
            List<int> list = new List<int>(testArray);
            MergeSort.Sort(list);
            CollectionAssert.AreEqual(new List<int>(expectedResult), list);
        }
    }
}
```

**Testing Edge Cases:**
```csharp
[TestMethod]
public void TestEmptyArray()
{
    int[] empty = { };
    BubbleSort.Sort(empty);
    Assert.AreEqual(0, empty.Length);
}

[TestMethod]
public void TestSingleElement()
{
    int[] single = { 42 };
    BubbleSort.Sort(single);
    CollectionAssert.AreEqual(new int[] { 42 }, single);
}

[TestMethod]
public void TestAlreadySorted()
{
    int[] sorted = { 1, 2, 3, 4, 5 };
    int[] expected = { 1, 2, 3, 4, 5 };
    InsertionSort.Sort(sorted);
    CollectionAssert.AreEqual(expected, sorted);
}

[TestMethod]
public void TestReverseSorted()
{
    int[] reverse = { 5, 4, 3, 2, 1 };
    int[] expected = { 1, 2, 3, 4, 5 };
    QuickSort.Sort(reverse);
    CollectionAssert.AreEqual(expected, reverse);
}
```

## 🚀 How to Run

### 1. Navigate to Project Directory
```bash
cd module12/Sortering
```

### 2. Run Performance Tests
```bash
# Build and run performance benchmark
dotnet run

# Expected output shows timing for each algorithm
```

### 3. Run Unit Tests
```bash
# Run all sorting tests
dotnet test

# Run specific algorithm tests
dotnet test --filter TestQuickSort

# Run with detailed output
dotnet test --verbosity normal
```

## 📊 Algorithm Selection Guide

### When to Use Each Algorithm

**Bubble Sort:**
- ✅ Educational purposes
- ✅ Very small datasets (< 20 elements)
- ✅ Code simplicity is paramount
- ❌ Never use for production with large data

**Insertion Sort:**
- ✅ Small datasets (< 50 elements)
- ✅ Nearly sorted data
- ✅ Online algorithm (can sort data as it arrives)
- ✅ Stable sorting required

**Selection Sort:**
- ✅ Memory-constrained environments
- ✅ Minimal number of swaps required
- ✅ Simple implementation needed
- ❌ Poor performance on large datasets

**Quick Sort:**
- ✅ General purpose sorting (most common choice)
- ✅ Large datasets with average case performance
- ✅ In-place sorting required
- ❌ Worst-case O(n²) performance unacceptable

**Merge Sort:**
- ✅ Guaranteed O(n log n) performance needed
- ✅ Stable sorting required
- ✅ External sorting (data larger than memory)
- ✅ Parallel processing (divide-and-conquer parallelizes well)

### Performance Characteristics Summary

```
Dataset Size Recommendations:

Small (< 50):      Insertion Sort
Medium (50-1000):  Quick Sort or Merge Sort  
Large (> 1000):    Quick Sort (average case) or Merge Sort (worst case)

Memory Constraints:
Limited Memory:    Quick Sort, Insertion Sort, Selection Sort
Abundant Memory:   Merge Sort

Stability Required:
Yes:              Merge Sort, Insertion Sort, Bubble Sort
No:               Quick Sort, Selection Sort

Worst-Case Guarantees:
Required:         Merge Sort
Not Critical:     Quick Sort
```

## 🎓 Practice Exercises

### Beginner Level

1. **Implement Cocktail Sort**: Bidirectional bubble sort
2. **Add Early Termination**: Optimize bubble sort to stop when array is sorted
3. **Count Operations**: Modify algorithms to count comparisons and swaps
4. **Visualize Sorting**: Add print statements to show each step

### Intermediate Level

5. **Hybrid Sort**: Combine algorithms (e.g., QuickSort + InsertionSort for small subarrays)
6. **Iterative QuickSort**: Implement QuickSort without recursion using stack
7. **Three-Way QuickSort**: Handle duplicate elements efficiently
8. **Bottom-Up MergeSort**: Implement iterative version of merge sort

### Advanced Level

9. **Heap Sort**: Implement O(n log n) in-place sorting algorithm
10. **Radix Sort**: Non-comparison based sorting for integers
11. **Tim Sort**: Python's hybrid stable sorting algorithm
12. **Parallel Merge Sort**: Multi-threaded implementation

### Expert Level

13. **External Sorting**: Sort data larger than available memory
14. **Cache-Optimized Sorting**: Optimize for CPU cache performance
15. **Adaptive Sorting**: Algorithm that adapts to data characteristics
16. **Stable QuickSort**: Modify QuickSort to be stable

## 🔧 Optimization Techniques

### Quick Sort Improvements

**Better Pivot Selection:**
```csharp
// Median-of-three pivot selection
private static int MedianOfThree(int[] array, int low, int high)
{
    int mid = (low + high) / 2;
    if (array[mid] < array[low]) Swap(array, low, mid);
    if (array[high] < array[low]) Swap(array, low, high);
    if (array[high] < array[mid]) Swap(array, mid, high);
    return mid;
}
```

**Hybrid Approach:**
```csharp
private static void HybridQuickSort(int[] array, int low, int high)
{
    const int INSERTION_THRESHOLD = 10;
    
    if (high - low < INSERTION_THRESHOLD)
    {
        InsertionSort.Sort(array, low, high);  // Use insertion sort for small subarrays
    }
    else if (low < high)
    {
        int pivot = Partition(array, low, high);
        HybridQuickSort(array, low, pivot - 1);
        HybridQuickSort(array, pivot + 1, high);
    }
}
```

### Memory Optimization

**In-Place Merge Sort:**
```csharp
// Advanced technique - merge without extra space
private static void InPlaceMerge(int[] array, int left, int mid, int right)
{
    // Complex implementation that merges without additional arrays
    // Trades space complexity O(n) for time complexity (slightly slower)
}
```

## 🧠 Algorithm Analysis Deep Dive

### Recurrence Relations

**Merge Sort:**
```
T(n) = 2T(n/2) + O(n)  // Two recursive calls + merge time
     = O(n log n)       // By Master Theorem
```

**Quick Sort:**
```
Best/Average: T(n) = 2T(n/2) + O(n) = O(n log n)
Worst:        T(n) = T(n-1) + O(n) = O(n²)
```

### Stability Analysis

**Stable Algorithm Example:**
```
Input:  [3a, 1, 3b, 2]  // 3a and 3b are equal but distinguishable
Output: [1, 2, 3a, 3b]  // Original order of equal elements preserved
```

**Unstable Algorithm Example:**
```
Input:  [3a, 1, 3b, 2]
Output: [1, 2, 3b, 3a]  // Order of equal elements may change
```

## 📚 Further Reading

- [Introduction to Algorithms (CLRS)](https://mitpress.mit.edu/books/introduction-algorithms-third-edition)
- [Algorithms 4th Edition by Sedgewick](https://algs4.cs.princeton.edu/home/)
- [Sorting Algorithm Animations](https://www.toptal.com/developers/sorting-algorithms)
- [Analysis of Algorithms](https://aofa.cs.princeton.edu/)
- [TimSort: The Fastest Sorting Algorithm](https://bugs.python.org/file4451/timsort.txt)
- [Parallel Sorting Algorithms](https://en.wikipedia.org/wiki/Parallel_sorting_algorithm)