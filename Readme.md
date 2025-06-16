# Software Architecture - 3rd Semester Course

This repository contains implementations of fundamental computer science concepts including algorithms, data structures, web development, and security patterns using C# .NET.

## 📋 Table of Contents

- [Module 1: Recursion Fundamentals](#module-1-recursion-fundamentals)
- [Module 2: LINQ & Functional Programming](#module-2-linq--functional-programming)
- [Module 3: Web API Development](#module-3-web-api-development)
- [Module 4: Blazor WebAssembly Frontend](#module-4-blazor-webassembly-frontend)
- [Module 5: Entity Framework & Database](#module-5-entity-framework--database)
- [Module 6: Advanced EF & API Integration](#module-6-advanced-ef--api-integration)
- [Module 10: Search Algorithms](#module-10-search-algorithms)
- [Module 11: Linked Lists](#module-11-linked-lists)
- [Module 12: Sorting Algorithms](#module-12-sorting-algorithms)
- [Module 13: Hash Tables](#module-13-hash-tables)
- [Module 16: Authentication & Security](#module-16-authentication--security)
- [Quick Reference](#quick-reference)

---

## Module 1: Recursion Fundamentals
**📁 Location:** `module1/Program.cs`

### Key Functions
- `fakultet(n)` - Factorial calculation
- `euclids(a, b)` - Greatest Common Divisor
- `potens(n, p)` - Power calculation
- `vendStreng(s)` - String reversal
- `times(a, b)` - Multiplication via addition

**🎯 Purpose:** Introduction to recursive programming patterns

---

## Module 2: LINQ & Functional Programming
**📁 Location:** `module2/Program.cs`

### Key Concepts
- LINQ operations (Sum, Count, Max, Where, Select)
- Lambda expressions and delegates
- Custom sorting with `Func<Person, Person, int>`
- Text filtering functions

### Classes
- `Person` - Data model with Name, Age, Phone
- `BubbleSort` - Generic sorting with comparison functions

**🎯 Use Case:** Data manipulation and functional programming patterns

---

## Module 3: Web API Development
**📁 Location:** `module3/Program.cs`

### API Endpoints
- GET  /api/hello           - Basic greeting
- GET  /api/hello/{name}    - Personalized greeting
- GET  /api/frugter         - Get all fruits
- POST /api/frugter         - Add new fruit
- GET  /api/tasks           - Get all tasks
- PUT  /api/tasks/{index}   - Update task
- DELETE /api/tasks/{index} - Delete task
- GET  /api/quiz            - Quiz questions

### Features
- CORS configuration
- REST API design
- Record types: `Fruit`, `Answer`, `Question`, `Task`

---

## Module 4: Blazor WebAssembly Frontend
**📁 Location:** `module4/TodoListBlazor/`

### Components
- `TodoList.razor` - Main todo interface
- `Quiz.razor` - Interactive quiz component
- `TodoTask.razor` - Individual task component
- `PostTask.razor` - Add new task form

### Services
- `TodoListService` - API communication for tasks
- `QuizService` - Quiz data management

**🎯 Purpose:** Frontend consuming Module 3 API

---

## Module 5: Entity Framework & Database
**📁 Location:** `module5/`

### Models
- `TodoTask` - Task entity with category and user
- `User` - User entity
- `TaskContext` - EF DbContext

### Features
- SQLite database with EF Core
- Database migrations
- One-to-many relationships
- CRUD operations

---

## Module 6: Advanced EF & API Integration
**📁 Locations:** `module6/` and `module6-opgave3/`

### Module 6 Models
- `Board` - Contains multiple todos
- `Todo` - Task with user assignment
- `User` - User entity

### Module 6 Opgave 3 Models
- `Book` - Book entity with author relationship
- `Author` - Author with multiple books

### Features
- Complex entity relationships
- Data seeding strategies
- Dependency injection patterns

---

## Module 10: Search Algorithms
**📁 Location:** `module10/search/Search.cs`

### Implemented Algorithms
| Algorithm | Time Complexity | Description |
|-----------|----------------|-------------|
| Linear Search | O(n) | Sequential search through array |
| Binary Search | O(log n) | Divide-and-conquer on sorted array |
| Sorted Insert | O(n) | Maintain sorted order during insertion |

### Key Methods
- `FindNumberLinear(array, target)`
- `FindNumberBinary(array, target)`
- `InsertSorted(value)`

**✅ Complete unit test coverage included**

---

## Module 11: Linked Lists
**📁 Location:** `module11/LinkedList/`

### Implementations
- **`UserLinkedList`** - Basic singly linked list
- **`DoubleUserLinkedList`** - Doubly linked list with previous pointers
- **`SortedUserLinkedList`** - Auto-sorting linked list

### Common Operations
- `AddFirst()` / `AddLast()`
- `RemoveFirst()` / `RemoveLast()`
- `Contains()` / `CountUsers()`
- `GetUser(index)`

---

## Module 12: Sorting Algorithms
**📁 Location:** `module12/Sortering/`

### Implemented Algorithms
| Algorithm | Time Complexity | Space Complexity | Characteristics |
|-----------|----------------|------------------|-----------------|
| Bubble Sort | O(n²) | O(1) | Simple, stable |
| Insertion Sort | O(n²) | O(1) | Efficient for small datasets |
| Selection Sort | O(n²) | O(1) | Minimal swaps |
| Quick Sort | O(n log n) | O(log n) | Divide-and-conquer |
| Merge Sort | O(n log n) | O(n) | Stable, guaranteed performance |

### Performance Testing
- `SortTester.cs` - Benchmarks with 100,000 random integers
- Performance comparison across all algorithms

---

## Module 13: Hash Tables
**📁 Location:** `module13/Hashing/`

### Implementations
- **`HashSetChaining`** - Collision resolution via linked lists
- **`HashSetLinearProbing`** - Open addressing with linear probing

### Features
- Dynamic resizing (load factor threshold: 0.75)
- Custom hash functions
- Standard operations: `Add()`, `Remove()`, `Contains()`, `Size()`

### Example Usage
```csharp
// Custom hash implementation in User class
public override int GetHashCode() {
    return Id + Username.GetHashCode();
}
```
---

### Module 16: Authentication & Security
**📁 Location:** modul16/

### Security Features
- Password Hashing: PBKDF2 with 128-bit salt (100,000 iterations)
- Custom Authentication Handler: Role-based authorization
- API Security: Protected endpoints with custom schemes

### Roles & Policies
- Admin - Full access to admin endpoints
- CakeLover - Access to cake-related endpoints
- Anonymous access for public endpoints

### Implementation Files
- password-hashing/Program.cs - Secure password hashing
- unsecure-api/DummyAuthenticationHandler.cs - Custom auth logic

### Quick Reference
- 📊 Algorithm Complexities

Search Algorithms:
├── Linear Search: O(n)
└── Binary Search: O(log n)

Sorting Algorithms:
├── O(n²): Bubble, Insertion, Selection
└── O(n log n): Quick Sort, Merge Sort

### 🏗️ Design Patterns Used
- Repository Pattern - DataService classes across modules
- Dependency Injection - Throughout web projects
- MVC/Component Architecture - Blazor frontend
- Factory Pattern - DbContext creation

### 🔒 Security Best Practices
- Password hashing with salt
- Role-based authorization
- CORS configuration
- Custom authentication handlers

### 🚀 Technologies Stack
- Backend: ASP.NET Core, Entity Framework Core
- Fontend: Blazor WebAssembly
- Database: SQLite
- Testing: MSTest Framework
- Authentication: Custom JWT-like implementation

### 🎓 Exam Preparation Notes
#### Critical Concepts to Remember

- Time Complexity Analysis - Big O notation for all algorithms
- Data Structure Trade-offs - When to use arrays vs linked lists vs hash tables
- Entity Framework Relationships - One-to-many, many-to-many configurations
- Web API Design - RESTful principles and HTTP methods
- Security Implementation - Proper password storage and authentication flows

#### Common Patterns
- CRUD Operations - Consistent across all data modules
- Error Handling - Try-catch patterns and null checking
- Async Programming - Task-based asynchronous operations in web modules