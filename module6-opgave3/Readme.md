# Module 6 Opgave 3: Book API with Entity Framework

## 📋 Overview

This module demonstrates a complete book management system built with ASP.NET Core Web API and Entity Framework Core. It showcases advanced ORM patterns, proper API design, database migrations, and comprehensive CRUD operations with related entities.

## 🎯 Learning Objectives

- Build a complete REST API with Entity Framework Core
- Implement one-to-many relationships between entities
- Master database migrations and schema evolution
- Use dependency injection with DbContext and services
- Create projection-based API responses
- Implement proper data seeding strategies
- Handle related entity operations efficiently

## 📁 Project Structure

```
module6-opgave3/book-api-ef/
├── Program.cs                          # API application entry point
├── TodoApi.csproj                     # Project configuration and packages
├── Data/
│   └── BookContext.cs                 # Entity Framework DbContext
├── Model/
│   ├── Author.cs                      # Author entity model
│   └── Book.cs                        # Book entity model
├── Service/
│   └── DataService.cs                 # Business logic and data operations
├── Migrations/                        # Entity Framework migrations
│   ├── 20220915094732_InitialCreate.cs
│   ├── 20220915103944_RenameTitle.cs
│   └── BookContextModelSnapshot.cs
├── Properties/
│   └── launchSettings.json           # Development server configuration
├── appsettings.json                  # Production configuration
└── appsettings.Development.json      # Development configuration
```

## 🏗️ Entity Models

### Author Entity (`Author.cs`)

Represents book authors with a collection of their books:

```csharp
namespace Model
{
    public class Author
    {
        public int AuthorId { get; set; }      // Primary key
        public string Fullname { get; set; }   // Author's full name
        public List<Book> Books { get; set; }  // Navigation property (one-to-many)
    }
}
```

**Key Features:**
- **Primary Key**: `AuthorId` with auto-increment
- **Required Field**: `Fullname` cannot be null
- **Navigation Property**: Collection of books by this author
- **Relationship**: One author can have many books

### Book Entity (`Book.cs`)

Represents individual books with author reference:

```csharp
namespace Model
{
    public class Book
    {
        public int BookId { get; set; }    // Primary key
        public string Title { get; set; }  // Book title
        public Author Author { get; set; } // Navigation property (many-to-one)
    }
}
```

**Key Features:**
- **Primary Key**: `BookId` with auto-increment
- **Required Field**: `Title` cannot be null
- **Navigation Property**: Reference to the book's author
- **Foreign Key**: Implicitly created for Author relationship

## 🗄️ Database Context (`BookContext.cs`)

Manages database connections and entity configurations:

```csharp
using Microsoft.EntityFrameworkCore;
using Model;

namespace Data
{
    public class BookContext : DbContext
    {
        // DbSet properties define database tables
        public DbSet<Book> Books => Set<Book>();
        public DbSet<Author> Authors => Set<Author>();

        public BookContext(DbContextOptions<BookContext> options)
            : base(options)
        {
            // Constructor for dependency injection
        }
    }
}
```

**Key Features:**
- **Modern DbSet Syntax**: Uses `Set<T>()` method
- **Dependency Injection**: Constructor accepts options
- **Automatic Configuration**: EF Core infers relationships from navigation properties
- **Multiple Entities**: Manages both books and authors

## 📊 Database Schema

### Authors Table
| Column | Type | Constraints |
|--------|------|-------------|
| AuthorId | INTEGER | PRIMARY KEY, AUTOINCREMENT |
| Fullname | TEXT | NOT NULL |

### Books Table
| Column | Type | Constraints |
|--------|------|-------------|
| BookId | INTEGER | PRIMARY KEY, AUTOINCREMENT |
| Title | TEXT | NOT NULL |
| AuthorId | INTEGER | FOREIGN KEY REFERENCES Authors(AuthorId) |

### Relationships
- **Books.AuthorId** → **Authors.AuthorId** (Many-to-One)
- **Cascade Delete**: When author is deleted, their books are also deleted

## 🔄 Database Migrations

### Migration History

#### 1. InitialCreate (`20220915094732`)

Creates the initial database schema:

```csharp
protected override void Up(MigrationBuilder migrationBuilder)
{
    migrationBuilder.CreateTable(
        name: "Authors",
        columns: table => new
        {
            AuthorId = table.Column<int>(type: "INTEGER", nullable: false)
                .Annotation("Sqlite:Autoincrement", true),
            Fullname = table.Column<string>(type: "TEXT", nullable: false)
        },
        constraints: table =>
        {
            table.PrimaryKey("PK_Authors", x => x.AuthorId);
        });

    migrationBuilder.CreateTable(
        name: "Books",
        columns: table => new
        {
            BookId = table.Column<int>(type: "INTEGER", nullable: false)
                .Annotation("Sqlite:Autoincrement", true),
            Titel = table.Column<string>(type: "TEXT", nullable: false),
            AuthorId = table.Column<int>(type: "INTEGER", nullable: false)
        },
        constraints: table =>
        {
            table.PrimaryKey("PK_Books", x => x.BookId);
            table.ForeignKey(
                name: "FK_Books_Authors_AuthorId",
                column: x => x.AuthorId,
                principalTable: "Authors",
                principalColumn: "AuthorId",
                onDelete: ReferentialAction.Cascade);
        });

    migrationBuilder.CreateIndex(
        name: "IX_Books_AuthorId",
        table: "Books",
        column: "AuthorId");
}
```

#### 2. RenameTitle (`20220915103944`)

Renames the Title column from Danish to English:

```csharp
protected override void Up(MigrationBuilder migrationBuilder)
{
    migrationBuilder.RenameColumn(
        name: "Titel",
        table: "Books",
        newName: "Title");
}

protected override void Down(MigrationBuilder migrationBuilder)
{
    migrationBuilder.RenameColumn(
        name: "Title",
        table: "Books",
        newName: "Titel");
}
```

## 🔧 Data Service Layer (`DataService.cs`)

Comprehensive business logic for book operations:

```csharp
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Data;
using Model;

namespace Service
{
    public class DataService
    {
        private BookContext db { get; }

        public DataService(BookContext db)
        {
            this.db = db;
        }

        /// <summary>
        /// Seeds sample data if database is empty
        /// </summary>
        public void SeedData()
        {
            Author author = db.Authors.FirstOrDefault()!;
            if (author == null)
            {
                author = new Author { Fullname = "Kristian" };
                db.Authors.Add(author);
                db.Authors.Add(new Author { Fullname = "Søren" });
                db.Authors.Add(new Author { Fullname = "Mette" });
            }

            Book book = db.Books.FirstOrDefault()!;
            if (book == null)
            {
                db.Books.Add(new Book { Title = "Harry Potter", Author = author });
                db.Books.Add(new Book { Title = "Ringenes Herre", Author = author });
                db.Books.Add(new Book { Title = "Entity Framework for Dummies", Author = author });
            }

            db.SaveChanges();
        }

        // READ OPERATIONS
        public List<Book> GetBooks()
        {
            return db.Books.Include(b => b.Author).ToList();
        }

        public Book GetBook(int id)
        {
            return db.Books.Include(b => b.Author).FirstOrDefault(b => b.BookId == id);
        }

        public List<Author> GetAuthors()
        {
            return db.Authors.ToList();
        }

        public Author GetAuthor(int id)
        {
            return db.Authors.Include(a => a.Books).FirstOrDefault(a => a.AuthorId == id);
        }

        // CREATE OPERATIONS
        public string CreateBook(string title, int authorId)
        {
            Author author = db.Authors.FirstOrDefault(a => a.AuthorId == authorId);
            db.Books.Add(new Book { Title = title, Author = author });
            db.SaveChanges();
            return "Book created";
        }
    }
}
```

**Key Features:**
- **Dependency Injection**: Receives DbContext via constructor
- **Conditional Seeding**: Only adds data if database is empty
- **Eager Loading**: Uses `.Include()` to load related entities
- **CRUD Operations**: Complete set of data operations
- **Error Handling**: Graceful handling of missing entities

## 🌐 API Endpoints

### Books API

#### Get All Books
- **Endpoint:** `GET /api/books`
- **Response:** Array of books with embedded author information

**Example Request:**
```bash
curl https://localhost:8080/api/books
```

**Example Response:**
```json
[
  {
    "bookId": 1,
    "title": "Harry Potter",
    "author": {
      "authorId": 1,
      "fullname": "Kristian"
    }
  },
  {
    "bookId": 2,
    "title": "Ringenes Herre",
    "author": {
      "authorId": 1,
      "fullname": "Kristian"
    }
  }
]
```

**Implementation:**
```csharp
app.MapGet("/api/books", (DataService service) =>
{
    return service.GetBooks().Select(b => new
    {
        bookId = b.BookId,
        title = b.Title,
        author = new
        {
            b.Author.AuthorId,
            b.Author.Fullname
        }
    });
});
```

#### Create New Book
- **Endpoint:** `POST /api/books`
- **Body:** JSON object with title and author ID
- **Response:** Success message

**Example Request:**
```bash
curl -X POST https://localhost:8080/api/books \
  -H "Content-Type: application/json" \
  -d '{"titel": "The Hobbit", "authorId": 1}'
```

**Example Response:**
```json
{
  "message": "Book created"
}
```

**Implementation:**
```csharp
app.MapPost("/api/books", (DataService service, NewBookData data) =>
{
    string result = service.CreateBook(data.Titel, data.AuthorId);
    return new { message = result };
});

record NewBookData(string Titel, int AuthorId);
```

### Authors API

#### Get All Authors
- **Endpoint:** `GET /api/authors`
- **Response:** Array of authors (basic information only)

**Example Request:**
```bash
curl https://localhost:8080/api/authors
```

**Example Response:**
```json
[
  {
    "authorId": 1,
    "fullname": "Kristian"
  },
  {
    "authorId": 2,
    "fullname": "Søren"
  }
]
```

#### Get Author with Books
- **Endpoint:** `GET /api/authors/{id}`
- **Parameters:** `id` (int) - Author ID
- **Response:** Author object with all their books

**Example Request:**
```bash
curl https://localhost:8080/api/authors/1
```

**Example Response:**
```json
{
  "authorId": 1,
  "fullname": "Kristian",
  "books": [
    {
      "bookId": 1,
      "title": "Harry Potter"
    },
    {
      "bookId": 2,
      "title": "Ringenes Herre"
    }
  ]
}
```

## ⚙️ Application Configuration

### Program.cs Setup

Complete application configuration with dependency injection:

```csharp
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.Json;
using Data;
using Service;

var builder = WebApplication.CreateBuilder(args);

// CORS Configuration
var AllowSomeStuff = "_AllowSomeStuff";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AllowSomeStuff, builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

// Database Configuration
builder.Services.AddDbContext<BookContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("ContextSQLite")));

// Business Service Registration
builder.Services.AddScoped<DataService>();

var app = builder.Build();

// Automatic Data Seeding
using (var scope = app.Services.CreateScope())
{
    var dataService = scope.ServiceProvider.GetRequiredService<DataService>();
    dataService.SeedData();
}

app.UseHttpsRedirection();
app.UseCors(AllowSomeStuff);

// Content-Type Middleware
app.Use(async (context, next) =>
{
    context.Response.ContentType = "application/json; charset=utf-8";
    await next(context);
});

// API Endpoints (defined above)
app.Run();
```

### Configuration Files

**appsettings.json:**
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "ContextSQLite": "Data Source=bin/database.db"
  }
}
```

**appsettings.Development.json:**
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "ConnectionStrings": {
    "ContextSQLite": "Data Source=bin/database.db"
  }
}
```

### Launch Settings

**launchSettings.json:**
```json
{
  "profiles": {
    "TodoApi": {
      "commandName": "Project",
      "dotnetRunMessages": true,
      "launchBrowser": true,
      "launchUrl": "api/tasks",
      "applicationUrl": "https://localhost:8080;http://localhost:8081",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    }
  }
}
```

## 📦 Project Dependencies

**TodoApi.csproj:**
```xml
<Project Sdk="Microsoft.NET.Sdk.Web">
  <PropertyGroup>
    <TargetFramework>net9.0</TargetFramework>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore" Version="7.0.4" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.Design" Version="7.0.4">
      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
      <PrivateAssets>all</PrivateAssets>
    </PackageReference>
    <PackageReference Include="Microsoft.EntityFrameworkCore.SQLite" Version="7.0.4" />
    <PackageReference Include="Microsoft.VisualStudio.Web.CodeGeneration.Design" Version="7.0.4" />
    <PackageReference Include="Swashbuckle.AspNetCore" Version="6.2.3" />
  </ItemGroup>
</Project>
```

**Package Purposes:**
- **EntityFrameworkCore.SQLite**: SQLite database provider
- **EntityFrameworkCore.Design**: Migration and design-time tools
- **Diagnostics.EntityFrameworkCore**: Enhanced error pages for EF
- **Swashbuckle.AspNetCore**: API documentation (Swagger)
- **Web.CodeGeneration.Design**: Scaffolding tools

## 🚀 How to Run

### 1. Prerequisites
```bash
# Install EF Core tools globally
dotnet tool install --global dotnet-ef
```

### 2. Setup Database
```bash
# Navigate to project directory
cd module6-opgave3/book-api-ef

# Create database from migrations
dotnet ef database update
```

### 3. Start Application
```bash
# Run the API
dotnet run
```

### 4. Test Endpoints
```bash
# Test basic endpoint
curl https://localhost:8080/

# Get all books
curl https://localhost:8080/api/books

# Get all authors
curl https://localhost:8080/api/authors

# Get specific author with books
curl https://localhost:8080/api/authors/1

# Create new book
curl -X POST https://localhost:8080/api/books \
  -H "Content-Type: application/json" \
  -d '{"titel": "New Book Title", "authorId": 1}'
```

## 🔑 Key Design Patterns

### Repository Pattern (Service Layer)

The `DataService` acts as a repository, abstracting database operations:

```csharp
// Instead of using DbContext directly in controllers
public class DataService
{
    private BookContext db { get; }
    
    // Encapsulated business logic
    public List<Book> GetBooks() => db.Books.Include(b => b.Author).ToList();
}
```

### Projection Pattern

API responses use projection to control data shape:

```csharp
// Only return needed fields, avoiding circular references
return service.GetBooks().Select(b => new
{
    bookId = b.BookId,
    title = b.Title,
    author = new { b.Author.AuthorId, b.Author.Fullname }
});
```

### Dependency Injection Pattern

Clean separation of concerns through DI:

```csharp
// Service registration
builder.Services.AddScoped<DataService>();

// Constructor injection
public DataService(BookContext db) => this.db = db;
```

## 🧪 Advanced Testing

### Integration Testing Setup

```csharp
// Test data creation
public void CreateTestData()
{
    var author = new Author { Fullname = "Test Author" };
    var book = new Book { Title = "Test Book", Author = author };
    
    db.Authors.Add(author);
    db.Books.Add(book);
    db.SaveChanges();
}

// Query testing
public void TestBookRetrieval()
{
    var books = dataService.GetBooks();
    Assert.IsTrue(books.Any());
    Assert.IsNotNull(books.First().Author);
}
```

### API Testing with Different Tools

**Using PowerShell:**
```powershell
# Get all books
Invoke-RestMethod -Uri "https://localhost:8080/api/books" -Method Get

# Create new book
$body = @{ titel = "PowerShell Book"; authorId = 1 } | ConvertTo-Json
Invoke-RestMethod -Uri "https://localhost:8080/api/books" -Method Post -Body $body -ContentType "application/json"
```

**Using JavaScript/Fetch:**
```javascript
// Get all books
fetch('https://localhost:8080/api/books')
  .then(response => response.json())
  .then(data => console.log(data));

// Create new book
fetch('https://localhost:8080/api/books', {
  method: 'POST',
  headers: { 'Content-Type': 'application/json' },
  body: JSON.stringify({ titel: 'JavaScript Book', authorId: 1 })
});
```

## 🎓 Practice Exercises

### Beginner Level

1. **Add Book Description**: Extend the Book model with a `Description` property
2. **ISBN Support**: Add ISBN field with validation
3. **Publication Year**: Add year of publication to books
4. **Author Bio**: Add biography field to authors

### Intermediate Level

5. **Book Categories**: Create a Category entity with many-to-many relationship
6. **Publishers**: Add Publisher entity with one-to-many to books
7. **Soft Delete**: Implement logical deletion for books and authors
8. **Search API**: Add endpoints for searching books by title/author

### Advanced Level

9. **Pagination**: Implement paginated responses for large datasets
10. **Caching**: Add response caching for frequently accessed data
11. **Validation**: Implement comprehensive input validation
12. **Audit Logging**: Track all changes to entities with timestamps

### Expert Level

13. **CQRS Pattern**: Separate read and write operations
14. **Event Sourcing**: Track all changes as events
15. **Multi-tenancy**: Support multiple book collections per tenant
16. **Performance Optimization**: Implement query optimization and N+1 prevention

## 🔧 Troubleshooting

### Common Issues and Solutions

**Migration Errors:**
```bash
# Reset database (development only)
dotnet ef database drop --force
dotnet ef migrations remove
dotnet ef migrations add InitialCreate
dotnet ef database update
```

**Circular Reference in JSON:**
```csharp
// Add to Program.cs
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = 
        System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});
```

**Foreign Key Violations:**
```csharp
// Ensure author exists before creating book
var author = db.Authors.Find(authorId);
if (author == null) throw new ArgumentException("Author not found");
```

**Performance Issues:**
```csharp
// Use projection instead of Include for large datasets
var books = db.Books
    .Select(b => new { b.BookId, b.Title, AuthorName = b.Author.Fullname })
    .ToList();
```

## 📚 Further Reading

- [Entity Framework Core Documentation](https://docs.microsoft.com/en-us/ef/core/)
- [ASP.NET Core Web API](https://docs.microsoft.com/en-us/aspnet/core/web-api/)
- [Dependency Injection in .NET](https://docs.microsoft.com/en-us/dotnet/core/extensions/dependency-injection)
- [Repository Pattern](https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/infrastructure-persistence-layer-design)
- [API Versioning](https://docs.microsoft.com/en-us/aspnet/core/web-api/advanced/versioning)
- [OpenAPI/Swagger](https://docs.microsoft.com/en-us/aspnet/core/tutorials/web-api-help-pages-using-swagger)