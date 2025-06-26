using Microsoft.AspNetCore.Mvc;
using WH.SimpleMediator;
using WH.SimpleMediator.Examples.Commands;
using WH.SimpleMediator.Examples.Events;
using WH.SimpleMediator.Examples.Validations;
using WH.SimpleMediator.Examples.Loggers;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSimpleMediator(cfg =>
{
    cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly);
    cfg.AddPipelineBehavior(typeof(ValidationCommand<,>));
    cfg.AddPipelineBehavior(typeof(LoggerCommand<,>));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/examples/{message}", async (
    [FromRoute] string message,
    [FromServices] IMediator mediator,
    CancellationToken cancellationToken) =>
{
    var result = await mediator.Send(new ExampleCommand(Guid.NewGuid(), message), cancellationToken);

    if (!result.Success)
    {
        return Results.BadRequest(result.Errors);
    }

    await mediator.Publish(new ExampleEvent(Guid.NewGuid(), message), cancellationToken);
    return Results.Ok();
})
.WithTags("Examples")
.WithOpenApi();

app.Run();
