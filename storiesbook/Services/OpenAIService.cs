namespace storiesbook.Services
{
    using OpenAI.Chat;
    using System.Text;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.Extensions.Configuration;

    public class OpenAIService
    {
        private readonly IConfiguration _config;

        public OpenAIService(IConfiguration config)
        {
            _config = config;
        }

        public async Task<string> GetDescriptionOfTicket(List<string> JiraInformation)
        {
            ChatClient client = new(model: "gpt-4o-mini", apiKey: _config["OPENAI_API_KEY"]);

            // Initialize variables to hold the issue creator, description, and comments
            string issueCreator = string.Empty;
            string issueDescription = string.Empty;
            List<string> commenters = new List<string>();
            List<string> comments = new List<string>();

            // Parse the Jira information
            foreach (var line in JiraInformation)
            {
                var parts = line.Split(':', 2); // Split each line at the first colon
                if (parts.Length < 2) continue; // Skip if it doesn't follow the expected format

                var key = parts[0].Trim();   // Get the part before the colon (the key)
                var value = parts[1].Trim(); // Get the part after the colon (the value)

                if (key.Equals("IssueCreator", StringComparison.OrdinalIgnoreCase))
                {
                    issueCreator = value; // Store the issue creator
                }
                else if (key.Equals("Description", StringComparison.OrdinalIgnoreCase))
                {
                    issueDescription = value; // Store the description
                }
                else
                {
                    commenters.Add(key);   // Add to the list of commenters
                    comments.Add(value);   // Add to the list of comments
                }
            }

            // Construct the prompt based on the parsed JiraInformation
            StringBuilder promptBuilder = new StringBuilder();
            promptBuilder.AppendLine("You will be provided with an issue's details, including its creator, description, and comments.");
            promptBuilder.AppendLine("Please format your response with the following sections:");
            promptBuilder.AppendLine("1. **Issue Summary**: Provide a brief summary of the issue.");
            promptBuilder.AppendLine("2. **People Involved**: List all people involved, including the issue creator and anyone who commented.");
            promptBuilder.AppendLine("3. **Comments Summary**: Summarize the key points of the comments.");
            promptBuilder.AppendLine();
            promptBuilder.AppendLine("Here are the details:");

            // Add the issue creator and description to the prompt
            promptBuilder.AppendLine($"- Issue Creator: {issueCreator}");
            promptBuilder.AppendLine($"- Description: {issueDescription}");

            // Add the commenters and their comments
            promptBuilder.AppendLine("Comments:");
            for (int i = 0; i < commenters.Count; i++)
            {
                promptBuilder.AppendLine($"- {commenters[i]}: {comments[i]}");
            }

            string prompt = promptBuilder.ToString();

            // Make the request to OpenAI with the constructed prompt
            ChatCompletion completion = await client.CompleteChatAsync(prompt);

            // Return the completion result as a string
            return completion.ToString();
        }
    }
}
