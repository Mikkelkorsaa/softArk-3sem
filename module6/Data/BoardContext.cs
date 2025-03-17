using Microsoft.EntityFrameworkCore;
using module6.model;

namespace Data
{
    public class BoardContext : DbContext
    {
        public DbSet<Board> Boards => Set<Board>();
        public DbSet<Todo> Todos => Set<Todo>();


        public BoardContext(DbContextOptions<BoardContext> options)
            : base(options)
        {
            // Den her er tom. Men ": base(options)" sikre at constructor
            // på DbContext super-klassen bliver kaldt.
        }
    }
}