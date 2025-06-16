# Module 1: Recursion Fundamentals

## 📋 Overview

This module introduces fundamental recursive programming patterns in C#. Recursion is a programming technique where a function calls itself to solve smaller instances of the same problem until it reaches a base case.

## 🎯 Learning Objectives

- Understand the concept of recursion
- Implement basic recursive algorithms
- Recognize base cases and recursive cases
- Apply recursion to mathematical operations and string manipulation

## 📁 Project Structure

```
module1/
├── Program.cs          # Main implementation file
└── module1.csproj     # Project configuration
```

## 🔧 Functions Implemented

### 1. Factorial (`fakultet`)

Calculates the factorial of a number (n! = n × (n-1) × ... × 1).

**Signature:** `static int fakultet(int n)`

**Example:**
```csharp
Console.WriteLine(fakultet(5)); // Output: 120
Console.WriteLine(fakultet(0)); // Output: 1
Console.WriteLine(fakultet(4)); // Output: 24
```

**How it works:**
- Base case: `n == 0` returns 1
- Recursive case: `n * fakultet(n - 1)`

### 2. Euclidean Algorithm (`euclids`)

Finds the Greatest Common Divisor (GCD) of two numbers using Euclid's algorithm.

**Signature:** `static int euclids(int a, int b)`

**Example:**
```csharp
Console.WriteLine(euclids(48, 18)); // Output: 6
Console.WriteLine(euclids(56, 42)); // Output: 14
Console.WriteLine(euclids(17, 13)); // Output: 1
```

**How it works:**
- Base case: `b <= a && a % b == 0` returns b
- Recursive cases:
  - If `a < b`: swap with `euclids(b, a)`
  - Otherwise: `euclids(b, a % b)`

### 3. Power Calculation (`potens`)

Calculates n raised to the power of p (n^p).

**Signature:** `static int potens(int n, int p)`

**Example:**
```csharp
Console.WriteLine(potens(2, 3)); // Output: 8
Console.WriteLine(potens(5, 2)); // Output: 25
Console.WriteLine(potens(7, 0)); // Output: 1
```

**How it works:**
- Base case: `p == 0` returns 1
- Recursive case: `n * potens(n, p - 1)`

### 4. Multiplication via Addition (`times`)

Multiplies two numbers using repeated addition.

**Signature:** `static int times(int a, int b)`

**Example:**
```csharp
Console.WriteLine(times(4, 5)); // Output: 20
Console.WriteLine(times(3, 7)); // Output: 21
Console.WriteLine(times(0, 10)); // Output: 0
```

**How it works:**
- Base cases:
  - `a == 1` returns b
  - `a == 0` returns 0
- Recursive case: `b + times(a - 1, b)`

### 5. String Reversal (`vendStreng`)

Reverses a string using recursion.

**Signature:** `static string vendStreng(string s)`

**Example:**
```csharp
Console.WriteLine(vendStreng("Hello")); // Output: "olleH"
Console.WriteLine(vendStreng("Recursion")); // Output: "noisruceR"
Console.WriteLine(vendStreng("A")); // Output: "A"
```

**How it works:**
- Base case: `s.Length == 0 || s.Length <= 1` returns s
- Recursive case: `vendStreng(s.Substring(1)) + s[0]`

### 6. Area Calculation (`areal`)

Calculates triangular numbers (sum of integers from 1 to n).

**Signature:** `static int areal(int bredde)`

**Example:**
```csharp
Console.WriteLine(areal(4)); // Output: 10 (1+2+3+4)
Console.WriteLine(areal(5)); // Output: 15 (1+2+3+4+5)
Console.WriteLine(areal(1)); // Output: 1
```

**How it works:**
- Base case: `bredde == 1` returns 1
- Recursive case: `bredde + areal(bredde - 1)`

## 🚀 How to Run

1. Navigate to the module1 directory
2. Run the project:
```bash
dotnet run
```

3. The program will execute `vendStreng("Hej")` and display the result.

## 🔑 Key Concepts

### Base Case
Every recursive function must have a **base case** - a condition where the function stops calling itself and returns a direct result.

### Recursive Case
The **recursive case** is where the function calls itself with modified parameters, gradually working toward the base case.

### Call Stack
Each recursive call is added to the call stack. When the base case is reached, the stack unwinds, combining results from each level.

## 💡 Common Patterns

1. **Mathematical Operations**: Factorial, power, GCD
2. **String Manipulation**: Reversal, parsing
3. **Accumulation**: Sum of series, area calculations

## ⚠️ Important Notes

- Ensure base cases are properly defined to avoid infinite recursion
- Be mindful of stack overflow with deep recursion
- Consider iterative alternatives for performance-critical applications
- Test edge cases (empty strings, zero values, etc.)

## 🎓 Practice Exercises

Try implementing these recursive functions:

1. **Fibonacci sequence**: `fib(n) = fib(n-1) + fib(n-2)`
2. **Sum of digits**: Sum all digits in a number
3. **Palindrome checker**: Check if a string reads the same forwards and backwards
4. **Binary search**: Search in a sorted array

## 📚 Further Reading

- [Recursion in Computer Science](https://en.wikipedia.org/wiki/Recursion_(computer_science))
- [C# Recursion Best Practices](https://docs.microsoft.com/en-us/dotnet/csharp/)
- [Call Stack and Memory Management](https://docs.microsoft.com/en-us/dotnet/standard/automatic-memory-management)