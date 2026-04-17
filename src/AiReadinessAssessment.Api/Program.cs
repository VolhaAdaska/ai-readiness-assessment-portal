using AiReadinessAssessment.Application;
using AiReadinessAssessment.Application.Common.Exceptions;
using AiReadinessAssessment.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Get connection string from configuration
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? "Data Source=ai_readiness_assessment.db";

// Add services to the container
builder.Services.AddApplication();
builder.Services.AddInfrastructure(connectionString);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Global exception handling middleware
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var exception = context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerFeature>()?.Error;

        context.Response.ContentType = "application/json";

        object response = new { error = "An internal server error occurred." };

        if (exception is NotFoundException notFound)
        {
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            response = new { error = notFound.Message };
        }
        else if (exception is ValidationException validation)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            response = new { error = validation.Message, errors = validation.Errors };
        }
        else if (exception is InvalidOperationException invalidOp)
        {
            context.Response.StatusCode = StatusCodes.Status409Conflict;
            response = new { error = invalidOp.Message };
        }
        else
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        }

        await context.Response.WriteAsJsonAsync(response);
    });
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
