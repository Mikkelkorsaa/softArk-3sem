using Microsoft.EntityFrameworkCore;
using Model;

using (var db = new TaskContext())
{
    Console.WriteLine($"Database path: {db.DbPath}.");

    // Create
    Console.WriteLine("Indsæt et nyt task");
    User asger = new User { Username = "Asger" };
    db.Add(new TodoTask("En opgave der skal løses med user", false, "Test", asger));
    db.SaveChanges();

    // Read
    Console.WriteLine("Find det sidste task");
    var lastTask = db.Tasks
        .OrderBy(b => b.TodoTaskId)
        .Last();
    Console.WriteLine($"Text: {lastTask.Text}");

    // // Update
    // Console.WriteLine("Opdater det sidste task");
    // var Task = await db.Tasks.SingleAsync(b => b.TodoTaskId == 1);
    // Task.Text = "DET VIRKER PÅ INDEX 1";
    // await db.SaveChangesAsync();
    // System.Console.WriteLine($"Text: {Task.Text}");

    // // Delete
    // Console.WriteLine("Slet task med id 13");
    // var task = await db.Tasks.SingleAsync(b => b.TodoTaskId == 14);
    // db.Tasks.Remove(task);
    // await db.SaveChangesAsync();
    // Console.WriteLine("Task slettet");

}