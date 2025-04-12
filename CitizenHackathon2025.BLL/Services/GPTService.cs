using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CitizenHackathon2025.BLL.Interfaces;

namespace CitizenHackathon2025.BLL.Services
{
    public class GptService : IGPTService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey = "sk-xxxxxxx";

        public GptService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetSuggestionsAsync(string prompt)
        {
            var request = new
            {
                model = "gpt-4o", // Ou "gpt-4-turbo" selon ton abonnement
                messages = new[] {
                new { role = "user", content = prompt }
            },
                temperature = 0.2
            };

            var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);

            var response = await _httpClient.PostAsync("https://api.openai.com/v1/chat/completions", content);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(result);
            return doc.RootElement
                      .GetProperty("choices")[0]
                      .GetProperty("message")
                      .GetProperty("content")
                      .GetString();
        }
    }
}
