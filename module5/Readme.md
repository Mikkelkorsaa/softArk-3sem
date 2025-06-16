# Module 5: Entity Framework & Database

## 📋 Overview

This module introduces Entity Framework Core, Microsoft's modern object-relational mapping (ORM) framework. You'll learn to work with databases using code-first approach, implement database migrations, create entity relationships, and perform CRUD operations using LINQ and Entity Framework.

## 🎯 Learning Objectives

- Understand Entity Framework Core and ORM concepts
- Implement code-first database development
- Create and manage database migrations
- Design entity relationships (one-to-many)
- Perform CRUD operations with Entity Framework
- Work with SQLite databases
- Handle database context and dependency injection

## 📁 Project Structure

```
module5/
├── Program.cs                  # Main application entry point
├── module5.csproj             # Project configuration with EF packages
├── Model/
│   ├── TaskContext.cs         # DbContext configuration
│   ├── TodoTask.cs           # Task entity model
│   └── User.cs               # User entity model
└── Migrations/
    ├── 20250217090323_InitialCreate.cs
    ├── 20250217094629_Category added to TodoTask.cs
    ├── 20250217101129_User added to TodoTask.cs
    ├── 20250217102451_InitialMigration.cs
    └── TaskContextModelSnapshot.cs
```

## 🏗️ Entity Models

### TodoTask Entity (`TodoTask.cs`)

Represents a task with category and user assignment:

```csharp
namespace Model
{
    public class TodoTask
    {
        // Constructors
        public TodoTask(string text, bool done, string category)
        {
            this.Text = text;
            this.Done = done;
            this.Category = category;
        }

        public TodoTask(string text, bool done, string category, User user)
        {
            this.Text = text;
            this.Done = done;
            this.Category = category;
            this.User = user;
        }

        // Properties
        public long TodoTaskId { get; set; }        // Primary key
        public string? Text { get; set; }           // Task description
        public bool Done { get; set; }              // Completion status
        public string Category { get; set; }        // Task category
        public User? User { get; set; }             // Assigned user (navigation property)
    }
}
```

**Key Features:**
- **Primary Key**: `TodoTaskId` (auto-generated)
- **Required Fields**: `Category` is required
- **Navigation Property**: `User` for one-to-many relationship
- **Multiple Constructors**: With and without user assignment

### User Entity (`User.cs`)

Represents a user in the system:

```csharp
using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class User
    {
        public long UserId { get; set; }       // Primary key
        public string? Username { get; set; }  // User's name
    }
}
```

**Key Features:**
- **Primary Key**: `UserId` (auto-generated)
- **Simple Structure**: Contains only essential user information
- **Nullable Username**: Allows for flexible user creation

## 🗄️ Database Context (`TaskContext.cs`)

The DbContext manages database connections and entity configurations:

```csharp
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Model
{
    public class TaskContext : DbContext
    {
        // DbSet represents tables in the database
        public DbSet<TodoTask> Tasks { get; set; }
        public string DbPath { get; }

        public TaskContext()
        {
            DbPath = "bin/TodoTask.db";  // SQLite database file path
        }

        // Configure database connection
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");

        // Configure entity mappings
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TodoTask>().ToTable("Tasks");
        }
    }
}
```

**Key Features:**
- **SQLite Database**: Uses local file-based database
- **DbSet Properties**: Define database tables
- **Connection Configuration**: Automatic SQLite setup
- **Table Mapping**: Custom table names via `ToTable()`

## 🔄 Database Migrations

Entity Framework tracks database schema changes through migrations:

### Migration History

1. **InitialCreate** (`20250217090323`)
    - Created initial `Tasks` table
    - Basic TodoTask properties (Id, Text, Done)

2. **Category Added** (`20250217094629`)
    - Added `Category` column to Tasks table
    - Made Category a required field

3. **User Added** (`20250217101129`)
    - Added `User` table
    - Added `UserId` foreign key to Tasks table
    - Created relationship between User and TodoTask

4. **InitialMigration** (`20250217102451`)
    - Empty migration (no changes)

### Sample Migration Code

**Adding Category Column:**
```csharp
protected override void Up(MigrationBuilder migrationBuilder)
{
    migrationBuilder.AddColumn<string>(
        name: "Category",
        table: "Tasks",
        type: "TEXT",
        nullable: false,
        defaultValue: "");
}
```

**Creating User Table and Relationship:**
```csharp
protected override void Up(MigrationBuilder migrationBuilder)
{
    migrationBuilder.AddColumn<long>(
        name: "UserId",
        table: "Tasks",
        type: "INTEGER",
        nullable: true);

    migrationBuilder.CreateTable(
        name: "User",
        columns: table => new
        {
            UserId = table.Column<long>(type: "INTEGER", nullable: false)
                .Annotation("Sqlite:Autoincrement", true),
            Username = table.Column<string>(type: "TEXT", nullable: true)
        },
        constraints: table =>
        {
            table.PrimaryKey("PK_User", x => x.UserId);
        });

    migrationBuilder.CreateIndex(
        name: "IX_Tasks_UserId",
        table: "Tasks",
        column: "UserId");

    migrationBuilder.AddForeignKey(
        name: "FK_Tasks_User_UserId",
        table: "Tasks",
        column: "UserId",
        principalTable: "User",
        principalColumn: "UserId");
}
```

## 💾 CRUD Operations (`Program.cs`)

### Create Operation

Adding new tasks and users to the database:

```csharp
using (var db = new TaskContext())
{
    Console.WriteLine($"Database path: {db.DbPath}.");

    // Create a new user
    User asger = new User { Username = "Asger" };
    
    // Create a new task with user assignment
    db.Add(new TodoTask("En opgave der skal løses med user", false, "Test", asger));
    
    // Save changes to database
    db.SaveChanges();
}
```

**Key Points:**
- **Using Statement**: Ensures proper database connection disposal
- **Add Method**: Adds entities to change tracker
- **SaveChanges**: Commits all pending changes to database
- **Automatic IDs**: Entity Framework generates primary keys automatically

### Read Operation

Querying data from the database:

```csharp
// Read the latest task
Console.WriteLine("Find det sidste task");
var lastTask = db.Tasks
    .OrderBy(b => b.TodoTaskId)
    .Last();
Console.WriteLine($"Text: {lastTask.Text}");
```

**Query Features:**
- **LINQ Integration**: Use familiar LINQ syntax
- **Method Chaining**: Combine multiple operations
- **Deferred Execution**: Queries execute when data is accessed

### Update Operation (Commented Example)

```csharp
// Update example (currently commented out)
// Console.WriteLine("Opdater det sidste task");
// var Task = await db.Tasks.SingleAsync(b => b.TodoTaskId == 1);
// Task.Text = "DET VIRKER PÅ INDEX 1";
// await db.SaveChangesAsync();
// System.Console.WriteLine($"Text: {Task.Text}");
```

### Delete Operation (Commented Example)

```csharp
// Delete example (currently commented out)
// Console.WriteLine("Slet task med id 13");
// var task = await db.Tasks.SingleAsync(b => b.TodoTaskId == 14);
// db.Tasks.Remove(task);
// await db.SaveChangesAsync();
// Console.WriteLine("Task slettet");
```

## ⚙️ Project Configuration (`module5.csproj`)

Required NuGet packages for Entity Framework:

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net9.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore" Version="7.0.4" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.Design" Version="7.0.4">
      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
      <PrivateAssets>all</PrivateAssets>
    </PackageReference>
    <PackageReference Include="Microsoft.EntityFrameworkCore.SQLite" Version="7.0.4" />
  </ItemGroup>
</Project>
```

**Package Purposes:**
- **EntityFrameworkCore.SQLite**: SQLite database provider
- **EntityFrameworkCore.Design**: Design-time tools for migrations
- **Diagnostics.EntityFrameworkCore**: Enhanced error reporting

## 🚀 How to Run

### 1. Install Entity Framework Tools

```bash
dotnet tool install --global dotnet-ef
```

### 2. Create Database Migration

```bash
dotnet ef migrations add "Description of changes"
```

### 3. Update Database

```bash
dotnet ef database update
```

### 4. Run Application

```bash
dotnet run
```

The application will:
1. Display the database path
2. Create a new task with user
3. Save to database
4. Query and display the latest task

## 🔑 Key Entity Framework Concepts

### Code-First Development

1. **Define Models**: Create C# classes for entities
2. **Configure Context**: Set up DbContext with DbSets
3. **Generate Migrations**: Use `dotnet ef migrations add`
4. **Update Database**: Apply migrations with `dotnet ef database update`

### Entity Relationships

**One-to-Many Relationship** (User → TodoTasks):
```csharp
public class User
{
    public long UserId { get; set; }
    // Navigation property (collection)
    public List<TodoTask> Tasks { get; set; }
}

public class TodoTask
{
    public long TodoTaskId { get; set; }
    public long? UserId { get; set; }  // Foreign key
    public User? User { get; set; }    // Navigation property
}
```

### DbContext Lifecycle

```csharp
using (var db = new TaskContext())
{
    // Context is created and connection opened
    // Perform database operations
    // Context is disposed and connection closed
}
```

### Change Tracking

Entity Framework automatically tracks changes to entities:
```csharp
var task = db.Tasks.First();
task.Text = "Updated text";  // Change is tracked
db.SaveChanges();           // Change is persisted
```

## 🗃️ Database Schema

### Tasks Table
| Column | Type | Constraints |
|--------|------|-------------|
| TodoTaskId | INTEGER | PRIMARY KEY, AUTOINCREMENT |
| Text | TEXT | NULLABLE |
| Done | INTEGER | NOT NULL (boolean) |
| Category | TEXT | NOT NULL |
| UserId | INTEGER | FOREIGN KEY, NULLABLE |

### User Table
| Column | Type | Constraints |
|--------|------|-------------|
| UserId | INTEGER | PRIMARY KEY, AUTOINCREMENT |
| Username | TEXT | NULLABLE |

### Relationships
- **Tasks.UserId** → **User.UserId** (Many-to-One)

## 🛠️ Common EF Commands

### Migration Commands
```bash
# Add new migration
dotnet ef migrations add MigrationName

# Update database to latest migration
dotnet ef database update

# Update to specific migration
dotnet ef database update MigrationName

# Remove last migration (if not applied)
dotnet ef migrations remove

# List all migrations
dotnet ef migrations list
```

### Database Commands
```bash
# Drop database
dotnet ef database drop

# Generate SQL script for migrations
dotnet ef migrations script

# Get database info
dotnet ef dbcontext info
```

## 🧪 Testing Database Operations

### Sample Data Creation

```csharp
// Create multiple users
var users = new List<User>
{
    new User { Username = "Alice" },
    new User { Username = "Bob" },
    new User { Username = "Charlie" }
};

// Create tasks for users
var tasks = new List<TodoTask>
{
    new TodoTask("Learn Entity Framework", false, "Education", users[0]),
    new TodoTask("Build web application", false, "Development", users[1]),
    new TodoTask("Write documentation", false, "Documentation", users[2])
};

db.Users.AddRange(users);
db.Tasks.AddRange(tasks);
db.SaveChanges();
```

### Advanced Queries

```csharp
// Find all incomplete tasks
var incompleteTasks = db.Tasks
    .Where(t => !t.Done)
    .ToList();

// Find tasks by category
var educationTasks = db.Tasks
    .Where(t => t.Category == "Education")
    .Include(t => t.User)  // Load related user data
    .ToList();

// Count tasks per user
var taskCounts = db.Tasks
    .GroupBy(t => t.User.Username)
    .Select(g => new { Username = g.Key, Count = g.Count() })
    .ToList();
```

## 🎓 Practice Exercises

Try extending the application with these features:

1. **Categories Management**: Create a separate Category entity
2. **Task Priority**: Add priority levels (High, Medium, Low)
3. **Due Dates**: Add deadline tracking for tasks
4. **Task Comments**: Add one-to-many relationship for comments
5. **User Roles**: Implement different user types
6. **Soft Delete**: Implement logical deletion instead of physical
7. **Audit Trail**: Track creation and modification dates
8. **Data Seeding**: Create sample data automatically
9. **Validation**: Add data annotations for validation
10. **Repository Pattern**: Abstract database operations

## 🔧 Troubleshooting

### Common Issues

**Migration Errors:**
```bash
# Reset migrations (development only)
dotnet ef database drop
dotnet ef migrations remove
dotnet ef migrations add InitialCreate
dotnet ef database update
```

**Database Locked:**
- Ensure all database connections are properly disposed
- Check for lingering database connections

**Migration Conflicts:**
- Resolve conflicts in migration files
- Consider creating new migration for fixes

## 📚 Further Reading

- [Entity Framework Core Documentation](https://docs.microsoft.com/en-us/ef/core/)
- [Code First Migrations](https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/)
- [Entity Relationships](https://docs.microsoft.com/en-us/ef/core/modeling/relationships)
- [SQLite Provider](https://docs.microsoft.com/en-us/ef/core/providers/sqlite/)
- [LINQ and Entity Framework](https://docs.microsoft.com/en-us/ef/core/querying/)
- [DbContext Configuration](https://docs.microsoft.com/en-us/ef/core/dbcontext-configuration/)