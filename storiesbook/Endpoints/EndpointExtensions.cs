namespace storiesbook.Endpoints;

public static class EndpointExtensions
{
    public static void MapEndpoints(this WebApplication app)
    {
        app.MapPost("/SummarizeTicket", (string ticketId) =>
            {
                /* logic will go here:
                 • grab jira ticket info
                 • pass that info to ai summarization tool
                 • pass summarized text back to GitHub
                 */
            })
            .WithName("SummarizeTicket")
            .WithOpenApi();
    }
}