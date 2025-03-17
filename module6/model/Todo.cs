namespace module6.model
{
    public class Todo
    {
        public Todo()
        {
        }
        public Todo(string text, string category, User user)
        {
            this.Text = text;
            this.Category = category;
            this.user = user;
        }
        public long UserId { get; set; }
        public string? Text { get; set; }
        public string? Category { get; set; }
        public User user { get; set; }
    }
};