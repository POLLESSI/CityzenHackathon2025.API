using System;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.Owin;
using Newtonsoft.Json;
using Owin;

[assembly: OwinStartup(typeof(CitizenHackathon2025.BLL.Services.AIService))]

namespace CitizenHackathon2025.BLL.Services
{
    public class AIService : IAIService
    {
        private readonly HttpClient _httpClient;
        private readonly OpenAIOptions _options;

        public AIService(HttpClient httpClient, IOptions<OpenAIOptions> options)
        {
            _httpClient = httpClient;
            _options = options.Value;
        }
        public async Task<string> GetTouristicSuggestionsAsync(string prompt)
        {
            var request = new
            {
                model = "gpt-4o", // or gpt-4o-mini if ​​you have access to it
                messages = new[]
                {
                new { role = "system", content = "You are a smart tourist assistant." },
                new { role = "user", content = prompt }
            },
                temperature = 0.2
            };

            var req = new HttpRequestMessage(HttpMethod.Post, _options.ApiUrl);
            req.Headers.Add("Authorization", $"Bearer {_options.ApiKey}");
            req.Content = JsonContent.Create(request);

            var response = await _httpClient.SendAsync(req);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            dynamic result = JsonConvert.DeserializeObject(json);

            return result.choices[0].message.content.ToString();
        }
        public Task GetSuggestionsAsync(object content)
        {
            throw new NotImplementedException();
        }

        public Task<string> SummarizeTextAsync(string input)
        {
            throw new NotImplementedException();
        }

        public Task<string> TranslateToFrenchAsync(string englishText)
        {
            throw new NotImplementedException();
        }
    }
}
