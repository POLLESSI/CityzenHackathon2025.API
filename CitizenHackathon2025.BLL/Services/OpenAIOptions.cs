namespace CitizenHackathon2025.BLL.Services
{
    public class OpenAIOptions
    {
    #nullable disable
        public string ApiKey { get; set; }
        public string ApiUrl { get; set; } = "https://api.openai.com/v1/chat/completions";
    }
}