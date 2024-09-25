using Atlassian.Jira;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using storiesbook.Services;

namespace storiesbook.Endpoints;

public static class EndpointExtensions
{
    public static void MapEndpoints(this WebApplication app)
    {
        app.MapPost("/SummarizeTicket", async Task<string> (string ticketId, [FromServices] JiraService jiraService, [FromServices] OpenAIService openAIService) =>
            {
                var ticketInformation = await jiraService.GetTicketInformation(ticketId);
                return (await openAIService.GetDescriptionOfTicket(ticketInformation));
            })
            .WithName("SummarizeTicket")    
            .WithOpenApi();

    }
}