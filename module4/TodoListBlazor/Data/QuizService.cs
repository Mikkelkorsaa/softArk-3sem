using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace TodoListBlazor.Data;

public class QuizService
{
    private readonly HttpClient http;
    private readonly IConfiguration configuration;
    private readonly string baseAPI = "";

    public event Action RefreshRequired;

    public QuizService(HttpClient http, IConfiguration configuration)
    {
        this.http = http;
        this.configuration = configuration;
        this.baseAPI = configuration["base_api"];
    }

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

    public record Answer(string Text, bool Correct);
    public record Question(string Text, Answer[] Answers);

}
