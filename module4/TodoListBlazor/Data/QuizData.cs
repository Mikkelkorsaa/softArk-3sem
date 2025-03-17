public class Answer
{
    public string Text { get; set; }
    public bool Correct { get; set; }
}
public class QuizData
{
    public string? Text { get; set; }
    public Answer[]? Answers { get; set; }
}