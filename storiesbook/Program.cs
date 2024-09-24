var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

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

app.Run();
