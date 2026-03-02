using System.Net.Http.Json;
using System.Text.Json;

namespace Fitness_Tracker.Agents;

public interface IOpenAiService
{
    Task<string> GetChatCompletionAsync(string prompt, string model = "gpt-4o");
}

public class OpenAiService : IOpenAiService
{
    private readonly HttpClient _http;

    public OpenAiService(HttpClient http)
    {
        _http = http;
    }

    public async Task<string> GetChatCompletionAsync(string prompt, string model = "gpt-4o")
    {
        var request = new
        {
            model,
            messages = new[] { new { role = "user", content = prompt } }
        };

        using var resp = await _http.PostAsJsonAsync("v1/chat/completions", request);
        resp.EnsureSuccessStatusCode();

        using var stream = await resp.Content.ReadAsStreamAsync();
        using var doc = await JsonDocument.ParseAsync(stream);

        if (doc.RootElement.TryGetProperty("choices", out var choices) &&
            choices.ValueKind == JsonValueKind.Array && choices.GetArrayLength() > 0)
        {
            var first = choices[0];
            if (first.TryGetProperty("message", out var message) &&
                message.TryGetProperty("content", out var content))
            {
                return content.GetString() ?? string.Empty;
            }
        }

        return string.Empty;
    }
}
