# Module 3: Web API Development

## 📋 Overview

This module introduces REST API development using ASP.NET Core. You'll learn to create HTTP endpoints, handle different HTTP methods, work with JSON data, configure CORS, and implement basic CRUD operations for web services.

## 🎯 Learning Objectives

- Build RESTful APIs with ASP.NET Core
- Handle HTTP methods (GET, POST, PUT, DELETE)
- Work with JSON serialization and deserialization
- Configure CORS for cross-origin requests
- Implement route parameters and query handling
- Create record types for data transfer objects

## 📁 Project Structure

```
module3/
├── Program.cs                      # Main API implementation
├── module3.csproj                 # Project configuration
├── Properties/
│   └── launchSettings.json       # Development server settings
├── appsettings.json              # Application configuration
└── appsettings.Development.json  # Development-specific settings
```

## 🌐 API Endpoints

### 1. Basic Greeting Endpoints

#### Simple Hello World
- **Endpoint:** `GET /api/hello`
- **Response:** JSON object with greeting message

**Example:**
```bash
curl http://localhost:5121/api/hello
```
**Response:**
```json
{
  "message": "Hello World!"
}
```

#### Personalized Greeting
- **Endpoint:** `GET /api/hello/{name}`
- **Parameters:** `name` (string) - Person's name
- **Response:** Personalized greeting message

**Example:**
```bash
curl http://localhost:5121/api/hello/John
```
**Response:**
```json
{
  "message": "Hello John!"
}
```

#### Greeting with Age
- **Endpoint:** `GET /api/hello/{name}/{age}`
- **Parameters:**
    - `name` (string) - Person's name
    - `age` (string) - Person's age
- **Response:** Greeting with age information

**Example:**
```bash
curl http://localhost:5121/api/hello/Alice/25
```
**Response:**
```json
{
  "message": "Hello Alice! You are 25 years old."
}
```

### 2. Fruit Management API

#### Get All Fruits
- **Endpoint:** `GET /api/frugter`
- **Response:** Array of fruit names

**Example:**
```bash
curl http://localhost:5121/api/frugter
```
**Response:**
```json
["æble", "banan", "pære", "ananas"]
```

#### Get Fruit by Index
- **Endpoint:** `GET /api/frugter/{index}`
- **Parameters:** `index` (int) - Array index
- **Response:** Single fruit name

**Example:**
```bash
curl http://localhost:5121/api/frugter/0
```
**Response:**
```json
"æble"
```

#### Get Random Fruit
- **Endpoint:** `GET /api/frugter/random`
- **Response:** Randomly selected fruit

**Example:**
```bash
curl http://localhost:5121/api/frugter/random
```
**Response:**
```json
"banan"
```

#### Add New Fruit
- **Endpoint:** `POST /api/frugter`
- **Body:** JSON object with fruit data
- **Response:** Updated fruit array or error status

**Example:**
```bash
curl -X POST http://localhost:5121/api/frugter \
  -H "Content-Type: application/json" \
  -d '{"name": "appelsin"}'
```
**Response:**
```json
["æble", "banan", "pære", "ananas", "appelsin"]
```

### 3. Todo Task Management API

#### Get All Tasks
- **Endpoint:** `GET /api/tasks`
- **Response:** Array of todo tasks

**Example:**
```bash
curl http://localhost:5121/api/tasks
```
**Response:**
```json
[
  {"text": "Gå tur med hunden", "done": false},
  {"text": "Køb ind", "done": false},
  {"text": "Lav lektier", "done": false},
  {"text": "Træn", "done": false}
]
```

#### Get Task by Index
- **Endpoint:** `GET /api/tasks/{index}`
- **Parameters:** `index` (int) - Task index
- **Response:** Single task object

**Example:**
```bash
curl http://localhost:5121/api/tasks/0
```
**Response:**
```json
{"text": "Gå tur med hunden", "done": false}
```

#### Update Task
- **Endpoint:** `PUT /api/tasks/{index}`
- **Parameters:** `index` (int) - Task index
- **Body:** Updated task object
- **Response:** Updated task array

**Example:**
```bash
curl -X PUT http://localhost:5121/api/tasks/0 \
  -H "Content-Type: application/json" \
  -d '{"text": "Gå tur med hunden", "done": true}'
```

#### Delete Task
- **Endpoint:** `DELETE /api/tasks/{index}`
- **Parameters:** `index` (int) - Task index
- **Response:** Updated task array

**Example:**
```bash
curl -X DELETE http://localhost:5121/api/tasks/0
```

#### Add New Task
- **Endpoint:** `POST /api/tasks`
- **Body:** New task object
- **Response:** Updated task array

**Example:**
```bash
curl -X POST http://localhost:5121/api/tasks \
  -H "Content-Type: application/json" \
  -d '{"text": "Læse bog", "done": false}'
```

### 4. Quiz API

#### Get All Quiz Questions
- **Endpoint:** `GET /api/quiz`
- **Response:** Array of quiz questions with answers

**Example:**
```bash
curl http://localhost:5121/api/quiz
```
**Response:**
```json
[
  {
    "text": "Hvad er hovedstaden i Danmark?",
    "answers": [
      {"text": "København", "correct": true},
      {"text": "Aarhus", "correct": false},
      {"text": "Odense", "correct": false},
      {"text": "Århus", "correct": false}
    ]
  }
]
```

#### Get Specific Question
- **Endpoint:** `GET /api/quiz/{index}`
- **Parameters:** `index` (int) - Question index
- **Response:** Single quiz question

#### Get Specific Answer
- **Endpoint:** `GET /api/quiz/{index}/answers/{questionIndex}`
- **Parameters:**
    - `index` (int) - Question index
    - `questionIndex` (int) - Answer index
- **Response:** Single answer object

**Example:**
```bash
curl http://localhost:5121/api/quiz/0/answers/0
```
**Response:**
```json
{"text": "København", "correct": true}
```

## 📝 Data Models (Record Types)

### Fruit Record
```csharp
record Fruit(string name);
```

### Answer Record
```csharp
record Answer(string Text, bool Correct);
```

### Question Record
```csharp
record Question(string Text, Answer[] Answers);
```

### Task Record
```csharp
record Task(string Text, bool Done);
```

## ⚙️ Configuration

### CORS Setup

The API is configured to allow cross-origin requests from any domain:

```csharp
var AllowCors = "_AllowCors";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AllowCors, builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});
```

### Launch Settings

Development server runs on:
- **HTTP:** `http://localhost:5121`
- **HTTPS:** `https://localhost:7006`

## 🚀 How to Run

1. Navigate to the module3 directory
2. Start the development server:
```bash
dotnet run
```

3. The API will be available at `http://localhost:5121`

4. Test endpoints using curl, Postman, or a web browser

## 🔧 Sample Data

### Initial Fruits Array
```csharp
String[] frugter = new String[]
{
    "æble", "banan", "pære", "ananas"
};
```

### Initial Todo Tasks
```csharp
Task[] toDoList = new Task[]
{
    new Task("Gå tur med hunden", false),
    new Task("Køb ind", false),
    new Task("Lav lektier", false),
    new Task("Træn", false)
};
```

### Quiz Questions
The API includes sample quiz questions about Scandinavian capitals with multiple-choice answers.

## 🔑 Key Concepts

### HTTP Methods
- **GET**: Retrieve data
- **POST**: Create new resources
- **PUT**: Update existing resources
- **DELETE**: Remove resources

### Route Parameters
Extract values from URL paths:
```csharp
app.MapGet("/api/hello/{name}", (string name) => 
    new { Message = $"Hello {name}!" });
```

### Request Body Handling
Process JSON data from request body:
```csharp
app.MapPost("/api/frugter/", (Fruit fruit) =>
{
    // Process fruit object
});
```

### Response Types
- **JSON Objects**: `new { Message = "Hello" }`
- **Arrays**: Return collections directly
- **Status Codes**: `Results.Ok()`, `Results.StatusCode(400)`

### CORS (Cross-Origin Resource Sharing)
Enables web applications from different domains to access the API.

## 🛠️ Development Features

### Hot Reload
Changes to the code automatically restart the development server.

### Environment-Specific Configuration
Different settings for development and production environments.

### Logging
Built-in logging for debugging and monitoring.

## 🧪 Testing the API

### Using curl

**Get all tasks:**
```bash
curl http://localhost:5121/api/tasks
```

**Add a new task:**
```bash
curl -X POST http://localhost:5121/api/tasks \
  -H "Content-Type: application/json" \
  -d '{"text": "New task", "done": false}'
```

**Update a task:**
```bash
curl -X PUT http://localhost:5121/api/tasks/0 \
  -H "Content-Type: application/json" \
  -d '{"text": "Updated task", "done": true}'
```

### Using Browser

Navigate to these URLs in your browser:
- `http://localhost:5121/api/hello`
- `http://localhost:5121/api/frugter`
- `http://localhost:5121/api/tasks`
- `http://localhost:5121/api/quiz`

## 🎓 Practice Exercises

Try extending the API with these features:

1. **User Management**: Add endpoints for user registration and authentication
2. **Data Validation**: Implement input validation for POST/PUT requests
3. **Pagination**: Add pagination support for large datasets
4. **Filtering**: Add query parameters for filtering results
5. **Error Handling**: Implement proper error responses and status codes
6. **Logging**: Add request/response logging
7. **Documentation**: Generate OpenAPI/Swagger documentation

## 📚 Further Reading

- [ASP.NET Core Web API](https://docs.microsoft.com/en-us/aspnet/core/web-api/)
- [Routing in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/routing)
- [Model Binding](https://docs.microsoft.com/en-us/aspnet/core/mvc/models/model-binding)
- [HTTP Status Codes](https://developer.mozilla.org/en-US/docs/Web/HTTP/Status)
- [RESTful API Design](https://restfulapi.net/)
- [CORS in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/security/cors)