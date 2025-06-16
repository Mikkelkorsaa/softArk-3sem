# Module 4: Blazor WebAssembly Frontend

## 📋 Overview

This module introduces frontend development using Blazor WebAssembly, a framework that allows you to build interactive web UIs using C# instead of JavaScript. You'll learn to create components, handle user interactions, consume APIs, and manage application state.

## 🎯 Learning Objectives

- Build interactive web applications with Blazor WebAssembly
- Create reusable Blazor components
- Consume REST APIs from a Blazor frontend
- Handle user input and form interactions
- Manage application state and component lifecycle
- Implement reactive UI updates
- Style components with CSS

## 📁 Project Structure

```
module4/TodoListBlazor/
├── Program.cs                  # Application entry point
├── App.razor                   # Root application component
├── _Imports.razor             # Global using statements
├── TodoListBlazor.csproj      # Project configuration
├── Pages/
│   ├── Index.razor            # Home page
│   ├── TodoList.razor         # Todo list page
│   └── Quiz.razor             # Quiz page
├── Shared/
│   ├── MainLayout.razor       # Application layout
│   ├── TodoTask.razor         # Individual task component
│   └── PostTask.razor         # Add task component
├── Data/
│   ├── TaskData.cs           # Task data model
│   ├── TodoListService.cs    # Todo API service
│   ├── QuizData.cs           # Quiz data model
│   └── QuizService.cs        # Quiz API service
├── Properties/
│   └── launchSettings.json   # Development settings
└── wwwroot/
    ├── index.html            # HTML host page
    ├── css/                  # Stylesheets
    ├── appsettings.json      # Configuration
    └── appsettings.Development.json
```

## 🧩 Core Components

### 1. App Component (`App.razor`)

The root component that handles routing and layout:

```razor
<Router AppAssembly="@typeof(App).Assembly">
    <Found Context="routeData">
        <RouteView RouteData="@routeData" DefaultLayout="@typeof(MainLayout)" />
        <FocusOnNavigate RouteData="@routeData" Selector="h1" />
    </Found>
    <NotFound>
        <PageTitle>Not found</PageTitle>
        <LayoutView Layout="@typeof(MainLayout)">
            <p role="alert">Sorry, there's nothing at this address.</p>
        </LayoutView>
    </NotFound>
</Router>
```

### 2. Main Layout (`MainLayout.razor`)

Provides the overall page structure:

```razor
@inherits LayoutComponentBase

<div class="page">
    <main>
        <article class="content px-4">
            @Body
        </article>
    </main>
</div>
```

### 3. Index Page (`Index.razor`)

The home page that displays the todo list:

```razor
@page "/"

<PageTitle>Todo List</PageTitle>

<TodoList></TodoList>
```

## 📝 Todo List Components

### TodoList Component (`TodoList.razor`)

Main component for displaying and managing tasks:

**Features:**
- Displays all tasks in an ordered list
- Handles data loading and refresh
- Integrates with API service

**Key Code:**
```razor
@page "/fetchdata"
@using TodoListBlazor.Data;
@inject TodoListService todoService

<h1>The Todo List</h1>

@if (tasks == null)
{
    <p><em>Loading...</em></p>
}
else
{
    <ol>
        @foreach (var task in tasks)
        {
            <li>
                <TodoTask Task=@task></TodoTask>
            </li>
        }
    </ol>
}

<PostTask />

@code {
    private TaskData[]? tasks;

    protected override async Task OnInitializedAsync()
    {
        tasks = await todoService.GetTaskData();
        todoService.RefreshRequired += this.RefreshMe;
    }
    
    private async void RefreshMe()
    {
        tasks = await todoService.GetTaskData();
        StateHasChanged();
    }
}
```

### TodoTask Component (`TodoTask.razor`)

Individual task display and interaction:

**Features:**
- Checkbox for marking tasks complete
- Updates task state via API
- Reactive UI updates

**Code:**
```razor
@using TodoListBlazor.Data;
@inject TodoListService todoService

<input id=@Task.Id type="checkbox" checked=@Task.Done @onchange="HandleDone"/>
<label htmlFor=@Task.Id>@Task.Text</label>

@code {
    [Parameter]
    public TaskData Task { get; set; }

    private void HandleDone(ChangeEventArgs e)
    {
        if (e.Value != null && e.Value is bool) {
            bool newValue = (bool) e.Value;
            Task.Done = newValue;
            todoService.PutTaskData(Task);
        }
    }
}
```

### PostTask Component (`PostTask.razor`)

Form for adding new tasks:

**Features:**
- Input field for task text
- Button to submit new tasks
- Clears input after submission

**Code:**
```razor
@using TodoListBlazor.Data;
@inject TodoListService todoService

<input type="input" id="taskText" @bind-value="taskText"/>
<button id="addTaskButton" type="button" @onclick="PostNewTask">Add Task</button>

@code {
    private string? taskText;
    
    private void PostNewTask()
    {
        if (taskText is null)
        {
            return;
        }
        todoService.PostTask(new TaskData() { Text = taskText, Done = false });
    }
}
```

## 🧠 Quiz Components

### Quiz Component (`Quiz.razor`)

Interactive quiz with multiple-choice questions:

**Features:**
- Displays questions one at a time
- Multiple choice answers with buttons
- Immediate feedback on answers
- Progress through questions
- Styled with custom CSS

**Key Features:**
```razor
@page "/quiz"
@using TodoListBlazor.Data
@inject QuizService quizService

<h3>Quiz</h3>

@if (quizData == null)
{
    <p>Loading...</p>
}
else if (currentQuestionIndex < quizData.Length)
{
    <div>
        <strong>@quizData[currentQuestionIndex].Text</strong>
        <ul>
            @foreach (var answer in quizData[currentQuestionIndex].Answers.Select((value, index) => new { value, index }))
            {
                <li>
                    <button @onclick="() => SelectAnswer(answer.index)">
                        @answer.value.Text
                    </button>
                </li>
            }
        </ul>
        @if (selectedAnswer != null)
        {
            <p class="@(isAnswerCorrect ? "correct" : "incorrect")">
                @(isAnswerCorrect ? "Correct" : "Incorrect")
            </p>
            <button @onclick="NextQuestion">Next Question</button>
        }
    </div>
}
else
{
    <p>Quiz completed!</p>
}
```

**Component Logic:**
```csharp
@code {
    private QuizData[]? quizData;
    private int currentQuestionIndex = 0;
    private int? selectedAnswer;
    private bool isAnswerCorrect;

    protected override async Task OnInitializedAsync()
    {
        quizData = await quizService.GetQuizData();
        quizService.RefreshRequired += StateHasChanged;
    }

    private async Task SelectAnswer(int answerIndex)
    {
        selectedAnswer = answerIndex;
        isAnswerCorrect = await answerQuestion(answerIndex);
    }

    private void NextQuestion()
    {
        if (currentQuestionIndex < quizData.Length - 1)
        {
            currentQuestionIndex++;
            selectedAnswer = null;
        }
    }

    private async Task<bool> answerQuestion(int answerIndex)
    {
        var answerStatus = await quizService.GetAnswer(currentQuestionIndex, answerIndex);
        return answerStatus.Correct;
    }
}
```

## 🔧 Data Models

### TaskData Class
```csharp
public class TaskData {
    public long Id { get; set; }
    public string Text { get; set; }
    public bool Done { get; set; }
}
```

### QuizData Class
```csharp
public class QuizData
{
    public string? Text { get; set; }
    public Answer[]? Answers { get; set; }
}

public class Answer
{
    public string Text { get; set; }
    public bool Correct { get; set; }
}
```

## 🌐 API Services

### TodoListService

Handles all todo-related API communications:

**Key Methods:**
```csharp
public class TodoListService
{
    private readonly HttpClient http;
    private readonly string baseAPI;

    public async Task<TaskData[]> GetTaskData()
    {
        string url = $"{baseAPI}tasks/";
        return await http.GetFromJsonAsync<TaskData[]>(url);
    }

    public async void PutTaskData(TaskData data)
    {
        TaskDataAPI newData = new TaskDataAPI(data.Text, data.Done);
        string url = $"{baseAPI}tasks/{data.Id}";
        var res = await http.PutAsJsonAsync(url, newData);
        CallRequestRefresh();
    }

    public async void PostTask(TaskData data)
    {
        TaskDataAPI newData = new TaskDataAPI(data.Text, data.Done);
        string url = $"{baseAPI}tasks/";
        var res = await http.PostAsJsonAsync(url, newData);
        CallRequestRefresh();
    }
}
```

### QuizService

Manages quiz data and answer validation:

**Key Methods:**
```csharp
public class QuizService
{
    public async Task<QuizData[]> GetQuizData()
    {
        string url = $"{baseAPI}quiz/";
        var result = await http.GetFromJsonAsync<QuizData[]>(url);
        return result ?? Array.Empty<QuizData>();
    }

    public async Task<Answer> GetAnswer(int id, int questionIndex)
    {
        string url = $"{baseAPI}quiz/{id}/answers/{questionIndex}";
        var result = await http.GetFromJsonAsync<Answer>(url);
        return result ?? new Answer(string.Empty, false);
    }
}
```

## ⚙️ Configuration & Setup

### Program.cs

Application configuration and dependency injection:

```csharp
var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddSingleton(sp => new HttpClient
{
  BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});
builder.Services.AddSingleton<TodoListService>();
builder.Services.AddSingleton<QuizService>();

await builder.Build().RunAsync();
```

### Configuration Files

**appsettings.json** (Production):
```json
{
  "base_api": "/api/"
}
```

**appsettings.Development.json** (Development):
```json
{
  "base_api": "http://localhost:5121/api/"
}
```

## 🎨 Styling

### Quiz Component Styles

Custom CSS for the quiz interface:

```css
h3 {
    text-align: center;
    font-size: 2em;
    color: #333;
}

div {
    margin: 20px auto;
    padding: 20px;
    max-width: 600px;
    background-color: #f9f9f9;
    border-radius: 10px;
    box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
}

button {
    padding: 10px 20px;
    font-size: 1em;
    color: #fff;
    background-color: #007bff;
    border: none;
    border-radius: 5px;
    cursor: pointer;
    transition: background-color 0.3s ease;
}

.correct {
    color: green;
}

.incorrect {
    color: red;
}
```

## 🚀 How to Run

1. **Ensure Module 3 API is running** on `http://localhost:5121`

2. Navigate to the module4/TodoListBlazor directory:
```bash
cd module4/TodoListBlazor
```

3. Start the Blazor application:
```bash
dotnet run
```

4. Open your browser and navigate to:
    - **HTTPS:** `https://localhost:7082`
    - **HTTP:** `http://localhost:5098`

## 🔑 Key Blazor Concepts

### Component Lifecycle

**OnInitializedAsync**: Called when component is first created
```csharp
protected override async Task OnInitializedAsync()
{
    // Initialize data, subscribe to events
}
```

### Data Binding

**One-way binding**: Display data
```razor
<p>@task.Text</p>
```

**Two-way binding**: Form inputs
```razor
<input @bind-value="taskText" />
```

**Event binding**: Handle user interactions
```razor
<button @onclick="PostNewTask">Add Task</button>
```

### Parameters

Pass data between components:
```csharp
[Parameter]
public TaskData Task { get; set; }
```

### Dependency Injection

Inject services into components:
```razor
@inject TodoListService todoService
```

### State Management

**StateHasChanged()**: Trigger UI refresh
```csharp
private async void RefreshMe()
{
    tasks = await todoService.GetTaskData();
    StateHasChanged();
}
```

## 🔄 Component Communication

### Event-Driven Updates

Services use events to notify components of changes:

```csharp
// In Service
public event Action RefreshRequired;

public void CallRequestRefresh()
{
    RefreshRequired?.Invoke();
}

// In Component
protected override async Task OnInitializedAsync()
{
    todoService.RefreshRequired += this.RefreshMe;
}
```

## 🧪 Features Demonstrated

### Todo List Features
- ✅ Display all tasks
- ✅ Add new tasks
- ✅ Mark tasks as complete/incomplete
- ✅ Real-time UI updates
- ✅ API integration

### Quiz Features
- ✅ Multiple choice questions
- ✅ Immediate feedback
- ✅ Progress tracking
- ✅ Styled interface
- ✅ Question navigation

## 🎓 Practice Exercises

Try extending the application with these features:

1. **Task Editing**: Add ability to edit task text
2. **Task Deletion**: Implement delete functionality
3. **Task Categories**: Add categories or tags to tasks
4. **User Authentication**: Add login/logout functionality
5. **Local Storage**: Save tasks locally when offline
6. **Task Filtering**: Filter tasks by completion status
7. **Task Search**: Add search functionality
8. **Quiz Scoring**: Implement scoring system for quiz
9. **Quiz Timer**: Add time limits to questions
10. **Responsive Design**: Improve mobile experience

## 📱 Development Features

### Hot Reload
Changes to Razor components automatically refresh in the browser.

### Browser Developer Tools
Use F12 to inspect the generated DOM and debug client-side issues.

### WebAssembly Debugging
Debug C# code directly in the browser using browser dev tools.

## 📚 Further Reading

- [Blazor WebAssembly Documentation](https://docs.microsoft.com/en-us/aspnet/core/blazor/webassembly/)
- [Blazor Components](https://docs.microsoft.com/en-us/aspnet/core/blazor/components/)
- [Blazor Data Binding](https://docs.microsoft.com/en-us/aspnet/core/blazor/components/data-binding)
- [Blazor Forms and Validation](https://docs.microsoft.com/en-us/aspnet/core/blazor/forms-validation)
- [Blazor Dependency Injection](https://docs.microsoft.com/en-us/aspnet/core/blazor/fundamentals/dependency-injection)
- [Blazor Routing](https://docs.microsoft.com/en-us/aspnet/core/blazor/fundamentals/routing)