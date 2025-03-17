namespace module6.model
{
    public class Board
    {
        public Board()
        {
        }
        public Board(List<Todo> todos)
        {
            this.todos = todos;
        }

        public long BoardId { get; set; }
        public List<Todo> todos { get; set; }
    }
};