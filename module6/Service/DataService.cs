using Microsoft.EntityFrameworkCore;
using System.Text.Json;

using Data;
using module6.model;
using System.Security.Cryptography.X509Certificates;

namespace Service;

public class DataService
{
    private BoardContext db { get; }

    public DataService(BoardContext db)
    {
        this.db = db;
    }
    /// <summary>
    /// Seeder noget nyt data i databasen hvis det er nødvendigt.
    /// </summary>
    public void SeedData()
    {

        Board board = db.Boards.FirstOrDefault()!;
        if (board == null)
        {
            board = new Board(new List<Todo> { new Todo("Test1", "Test", new User { Username = "Asger" }) });
            db.Boards.Add(board);
            db.Boards.Add(new Board(new List<Todo> { new Todo("Test2", "Test", new User { Username = "Asger" }) }));
            db.Boards.Add(new Board(new List<Todo> { new Todo("Test3", "Test", new User { Username = "Asger" }) }));
        }
        db.SaveChanges();
    }
    public List<Board> GetBoards()
    {
        return db.Boards.Include(b => b.todos).ToList();
    }
};