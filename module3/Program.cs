var builder = WebApplication.CreateBuilder(args);

// Åben op for "CORS" i din API.
// Læs om baggrunden her: https://docs.microsoft.com/en-us/aspnet/core/security/cors?view=aspnetcore-6.0

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

var app = builder.Build();
app.UseCors(AllowCors);

app.MapGet("/api/hello", () => new { Message = "Hello World!" });

app.MapGet("/api/hello/{name}", (string name) => new { Message = $"Hello {name}!" });


app.MapGet("/api/hello/{name}/{age}", (string name, string age) => new { Message = $"Hello {name}! You are {age} years old." });

String[] frugter = new String[]
{
    "æble", "banan", "pære", "ananas"
};

app.MapGet("/api/frugter", () => frugter);

app.MapGet("/api/frugter/{index}", (int index) => frugter[index]);

app.MapGet("/api/frugter/random", () => frugter[new Random().Next(0, frugter.Length)]);

app.MapPost("/api/frugter/", (Fruit fruit) =>
{
    if (fruit.name != string.Empty)
    {
        frugter = frugter.Append(fruit.name).ToArray();

        Console.WriteLine($"Tilføjet frugt: {fruit.name}");

        return Results.Ok(frugter);
    }


    return Results.StatusCode(400);
});
//OPGAVE 7
Task[] toDoList = new Task[]
{
    new Task("Gå tur med hunden", false),
    new Task("Køb ind", false),
    new Task("Lav lektier", false),
    new Task("Træn", false)
};

app.MapGet("/api/tasks", () => toDoList);
app.MapGet("/api/tasks/{index}", (int index) => toDoList[index]);
app.MapPut("/api/tasks/{index}", (int index, Task task) =>
{
    toDoList[index] = task;
    return Results.Ok(toDoList);
});
app.MapDelete("/api/tasks/{index}", (int index) =>
{
    toDoList = toDoList.Where((task, i) => i != index).ToArray();
    return Results.Ok(toDoList);
});
app.MapPost("/api/tasks", (Task task) =>
{
    toDoList = toDoList.Append(task).ToArray();
    return Results.Ok(toDoList);
});

//OPGAVE 8 - quiz api
Question[] quiz = new Question[]
{
    new Question("Hvad er hovedstaden i Danmark?", new Answer[]
    {
        new Answer("København", true),
        new Answer("Aarhus", false),
        new Answer("Odense", false),
        new Answer("Århus", false)}
        ),
    new Question("Hvad er hovedstaden i Sverige?", new Answer[]
    {
        new Answer("København", false),
        new Answer("Stockholm", true),
        new Answer("Oslo", false),
        new Answer("Helsinki", false)}
        ),
    new Question("Hvad er hovedstaden i Norge?", new Answer[]
    {
        new Answer("København", false),
        new Answer("Stockholm", false),
        new Answer("Oslo", true),
        new Answer("Helsinki", false)}
        ),
};

app.MapGet("/api/quiz", () => quiz);
app.MapGet("/api/quiz/{index}", (int index) => quiz[index]);
app.MapGet("/api/quiz/{index}/answers/{questionIndex}", (int index, int questionIndex) => quiz[index].Answers[questionIndex]);
app.Run();
record Fruit(string name);
record Answer(string Text, bool Correct);
record Question(string Text, Answer[] Answers);
record Task(string Text, bool Done);

