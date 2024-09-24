using Atlassian.Jira;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using storiesbook.Services;

namespace storiesbook.Endpoints;

public static class EndpointExtensions
{
    public static void MapEndpoints(this WebApplication app)
    {
        app.MapPost("/SummarizeTicket", async Task<List<string>> (string ticketId, [FromServices] JiraService jiraService) =>
            {
                return (await jiraService.GetTicketInformation(ticketId));
            })
            .WithName("SummarizeTicket")
            .WithOpenApi();
    }
}