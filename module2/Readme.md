# Module 2: LINQ & Functional Programming

## 📋 Overview

This module explores LINQ (Language Integrated Query) operations and functional programming patterns in C#. You'll learn to work with collections using lambda expressions, delegates, and functional programming techniques for data manipulation and transformation.

## 🎯 Learning Objectives

- Master LINQ operations for data querying and manipulation
- Understand lambda expressions and delegates
- Implement functional programming patterns
- Create custom sorting and filtering functions
- Work with higher-order functions

## 📁 Project Structure

```
module2/
├── Program.cs          # Main implementation file
└── module2.csproj     # Project configuration
```

## 🏗️ Core Classes

### Person Class

Represents a person with basic information.

```csharp
public class Person
{
    public string Name { get; set; } = "";
    public int Age { get; set; } = 0;
    public string Phone { get; set; } = "";
}
```

### Sample Data

```csharp
Person[] people = new Person[]
{
    new Person { Name = "Jens Hansen", Age = 45, Phone = "+4512345678" },
    new Person { Name = "Jane Olsen", Age = 22, Phone = "+4543215687" },
    new Person { Name = "Tor Iversen", Age = 35, Phone = "+4587654322" },
    new Person { Name = "Sigurd Nielsen", Age = 31, Phone = "+4512345673" },
    new Person { Name = "Viggo Nielsen", Age = 28, Phone = "+4543217846" },
    new Person { Name = "Rosa Jensen", Age = 23, Phone = "+4543217846" }
};
```

## 🔧 LINQ Operations (Opgave1 Class)

### 1. Total Age Calculation (`GetTotalAge`)

Calculates the sum of all ages in the collection.

**Signature:** `public int GetTotalAge()`

**Example:**
```csharp
Opgave1 opgave = new Opgave1();
int totalAge = opgave.GetTotalAge();
Console.WriteLine($"Total Age: {totalAge}"); // Output: Total Age: 184
```

**Implementation:**
```csharp
return people.Sum(person => person.Age);
```

### 2. Name Search (`CountNames`)

Counts how many people have a specific substring in their name.

**Signature:** `public int CountNames(string name)`

**Example:**
```csharp
int nielsenCount = opgave.CountNames("Nielsen");
Console.WriteLine($"Nielsen count: {nielsenCount}"); // Output: Nielsen count: 2

int jensenCount = opgave.CountNames("Jensen");
Console.WriteLine($"Jensen count: {jensenCount}"); // Output: Jensen count: 2
```

**Implementation:**
```csharp
return people.Count(person => person.Name.Contains(name));
```

### 3. Oldest Person (`OldestPerson`)

Finds the age of the oldest person.

**Signature:** `public int OldestPerson()`

**Example:**
```csharp
int oldestAge = opgave.OldestPerson();
Console.WriteLine($"Oldest person age: {oldestAge}"); // Output: Oldest person age: 45
```

**Implementation:**
```csharp
return people.Max(person => person.Age);
```

### 4. Phone Number Validation (`HasPhoneNumber`)

Checks if a specific phone number exists in the collection.

**Signature:** `public bool HasPhoneNumber(string phoneNumber)`

**Example:**
```csharp
bool hasPhone = opgave.HasPhoneNumber("+4543217846");
Console.WriteLine($"Has phone: {hasPhone}"); // Output: Has phone: True

bool hasOtherPhone = opgave.HasPhoneNumber("+4500000000");
Console.WriteLine($"Has other phone: {hasOtherPhone}"); // Output: Has other phone: False
```

**Implementation:**
```csharp
return people.Where(person => person.Phone == phoneNumber).Any();
```

### 5. Age Filter (`OlderThan`)

Returns names of people older than a specified age as a comma-separated string.

**Signature:** `public string OlderThan(int age)`

**Example:**
```csharp
string olderThan30 = opgave.OlderThan(30);
Console.WriteLine($"Older than 30: {olderThan30}");
// Output: Older than 30: Jens Hansen, Tor Iversen, Sigurd Nielsen
```

**Implementation:**
```csharp
IEnumerable<Person> people1 = people.Where(person => person.Age > age);
return people1.Select(person => person.Name).Aggregate((a, b) => a + ", " + b);
```

### 6. Phone Number Formatting (`BetterPhone`)

Removes country code prefix from phone numbers.

**Signature:** `public static Person[] BetterPhone(Person[] people)`

**Example:**
```csharp
Person[] formattedPeople = Opgave1.BetterPhone(Opgave1.people);
// Transforms "+4512345678" to "12345678"
```

**Implementation:**
```csharp
return people.Select(p => new Person { 
    Name = p.Name, 
    Phone = p.Phone.Substring(2) 
}).ToArray();
```

### 7. Youth Filter (`YoungerThan30`)

Returns formatted information for people under 30.

**Signature:** `public string YoungerThan30(Person[] person)`

**Example:**
```csharp
string youngPeople = opgave.YoungerThan30(Opgave1.people);
Console.WriteLine($"Younger than 30: {youngPeople}");
// Output: Jane Olsen, +4543215687: Viggo Nielsen, +4543217846: Rosa Jensen, +4543217846
```

**Implementation:**
```csharp
var people1 = people.Where(person => person.Age < 30);
return string.Join(": ", people1.Select(person => $"{person.Name}, {person.Phone}"));
```

## 🔄 Custom Sorting (BubbleSort Class)

### Comparison Functions

Pre-defined comparison delegates for different sorting criteria:

```csharp
// Sort by age
public static Func<Person, Person, int> ComparePersonAge = (p1, p2) => p1.Age - p2.Age;

// Sort by name alphabetically
public static Func<Person, Person, int> ComparePersonName = (p1, p2) => p1.Name.CompareTo(p2.Name);

// Sort by phone number
public static Func<Person, Person, int> ComparePersonPhone = (p1, p2) => p1.Phone.CompareTo(p2.Phone);
```

### Generic Bubble Sort

**Signature:** `public static Person[] Sort(Person[] array, Func<Person, Person, int> compareFn)`

**Examples:**
```csharp
// Sort by age
Person[] sortedByAge = BubbleSort.Sort(people, BubbleSort.ComparePersonAge);

// Sort by name
Person[] sortedByName = BubbleSort.Sort(people, BubbleSort.ComparePersonName);

// Sort by phone
Person[] sortedByPhone = BubbleSort.Sort(people, BubbleSort.ComparePersonPhone);
```

## 🎛️ Functional Programming Patterns

### Text Filtering Functions

Higher-order functions that create customized text processors:

#### Word Filter Function

Creates a function that removes specified bad words:

```csharp
var CreateWordFilterFn = (string[] badWords) =>
{
    return (string text) =>
    {
        return text.Split(' ')
        .Where(word => !badWords.Contains(word))
        .Aggregate((a, b) => a + " " + b);
    };
};
```

**Example:**
```csharp
var badWords = new string[] { "luder", "svin", "fuck" };
var filterBadWords = CreateWordFilterFn(badWords);
string cleanText = filterBadWords("Du er en luder og et svin fuck dig");
// Output: "Du er en og et dig"
```

#### Word Replacer Function

Creates a function that replaces bad words with a replacement string:

```csharp
var CreateWordReplacerFn = (string[] badWords, string replacement) =>
{
    return (string text) =>
    {
        return text.Split(' ')
        .Select(word => badWords.Contains(word) ? replacement : word)
        .Aggregate((a, b) => a + " " + b);
    };
};
```

**Example:**
```csharp
var badWords = new string[] { "luder", "svin", "fuck" };
var replaceBadWords = CreateWordReplacerFn(badWords, "***");
string cleanText = replaceBadWords("Du er en luder og et svin fuck dig");
// Output: "Du er en *** og et *** *** dig"
```

## 🚀 How to Run

1. Navigate to the module2 directory
2. Run the project:
```bash
dotnet run
```

3. The program will demonstrate all LINQ operations, sorting functions, and text filtering.

## 🔑 Key Concepts

### Lambda Expressions
Anonymous functions that can be used inline:
```csharp
person => person.Age > 30  // Lambda expression
```

### Delegates and Func Types
Type-safe function pointers:
```csharp
Func<Person, Person, int> comparer = (p1, p2) => p1.Age - p2.Age;
```

### Higher-Order Functions
Functions that take other functions as parameters or return functions:
```csharp
var filterCreator = (string[] words) => (string text) => /* filter logic */;
```

### Method Chaining
Chaining LINQ operations for fluent data processing:
```csharp
people.Where(p => p.Age > 30).Select(p => p.Name).ToArray();
```

## 📊 LINQ Operations Overview

| Operation | Purpose | Example |
|-----------|---------|---------|
| `Where()` | Filter elements | `people.Where(p => p.Age > 30)` |
| `Select()` | Transform elements | `people.Select(p => p.Name)` |
| `Sum()` | Calculate sum | `people.Sum(p => p.Age)` |
| `Count()` | Count elements | `people.Count(p => p.Name.Contains("Nielsen"))` |
| `Max()` | Find maximum | `people.Max(p => p.Age)` |
| `Any()` | Check if any match | `people.Any(p => p.Phone == number)` |
| `Aggregate()` | Combine elements | `names.Aggregate((a, b) => a + ", " + b)` |

## 💡 Functional Programming Benefits

1. **Immutability**: Original data remains unchanged
2. **Composability**: Functions can be easily combined
3. **Readability**: Declarative style is easier to understand
4. **Reusability**: Generic functions work with different data types
5. **Testability**: Pure functions are easier to test

## 🎓 Practice Exercises

Try implementing these LINQ operations:

1. **Group people by age ranges** (20-29, 30-39, 40+)
2. **Find the most common phone area code**
3. **Create a phonebook dictionary** from the person array
4. **Calculate average age by name suffix** (Hansen, Nielsen, etc.)
5. **Implement custom text processors** using functional patterns

## 📚 Further Reading

- [LINQ Documentation](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/linq/)
- [Lambda Expressions](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/lambda-expressions)
- [Functional Programming in C#](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/linq/functional-programming-vs-imperative-programming)
- [Delegates and Events](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/delegates/)