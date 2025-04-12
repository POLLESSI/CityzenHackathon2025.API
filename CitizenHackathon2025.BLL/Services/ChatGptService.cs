using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace CitizenHackathon2025.BLL.Services
{
    public class ChatGptService
    {
    #nullable disable
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public ChatGptService(HttpClient httpClient, string apiKey, object configuration)
        {
            _httpClient = httpClient;
            //_apiKey = Configuration["OpenAI:ApiKey"]; // In appsettings.json
        }
        public async Task<string> AskChatGptAsync(string userInput)
        {
            var requestBody = new
            {
                model = "gpt-4o",
                message = new[]
                {
                    new { role = "system", content = "You are a helpful assistant in tourist orientation." },
                    new { role = "user", content = userInput }
                },
            };
            var requestJson = JsonSerializer.Serialize(requestBody);
            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.openai.com/v1/chat/completions")
            {
                Content = new StringContent(requestJson, Encoding.UTF8, "application/json")
            };
            request.Headers.Add("Authorization", $"<Bearer {_apiKey}");
            request.Content = new StringContent(requestJson, Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            using var stream = await response.Content.ReadAsStreamAsync();
            using var doc = await JsonDocument.ParseAsync(stream);
            return doc.RootElement
                .GetProperty("choices")[0]
                .GetProperty("message")
                .GetProperty("content")
                .GetString();
        }
    }
}
