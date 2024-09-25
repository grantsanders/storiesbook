namespace storiesbook.Services
{
    using OpenAI.Chat;

    public class OpenAIService
    {
        public OpenAIService()
        {

        }

        public async Task<string> GetDescriptionOfTicket()
        {
            ChatClient client = new(model: "gpt-4o", apiKey: Environment.GetEnvironmentVariable("OPENAI_API_KEY"));

            ChatCompletion completion = await client.CompleteChatAsync("Say 'this is a test.'");

            return completion.ToString();
        }
    }
}
