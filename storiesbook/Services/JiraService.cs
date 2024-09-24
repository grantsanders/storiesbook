using Atlassian.Jira;
using Newtonsoft.Json;

namespace storiesbook.Services;

public class JiraService
{
    private readonly IConfiguration _config;

    public JiraService(IConfiguration config)
    {
        _config = config;
    }

    public async Task<List<string>> GetTicketInformation(string ticketId)
    {
        var jira = Jira.CreateRestClient(_config["JIRA_URL"], _config["JIRA_USERNAME"], _config["JIRA_API_KEY"]);

        var issue = await jira.Issues.GetIssueAsync(ticketId);
        var comments = await issue.GetCommentsAsync();

        var ticketInformation = new List<string>();
        
        ticketInformation.Add(issue.Summary);
        foreach (var comment in comments)
        {
            ticketInformation.Add(comment.Body);
        }

        return ticketInformation;
    }
}