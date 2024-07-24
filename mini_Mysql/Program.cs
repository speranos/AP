using Microsoft.EntityFrameworkCore;
using NSwag.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument(config =>
{
    config.DocumentName = "TodoAPI";
    config.Title = "TodoAPI v1";
    config.Version = "v1";
});

// var contextstring = builder.Configuration.GetValue<string>("ConnectionStrings:DefaultConnection");
// DotNetEnv.Env.Load();
builder.Services.AddDbContext<AppContext>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi(config =>
    {
        config.DocumentTitle = "TodoAPI";
        config.Path = "/swagger";
        config.DocumentPath = "/swagger/{documentName}/swagger.json";
        config.DocExpansion = "list";
    });
}


app.MapGet("/all", async (AppContext db) => {
    var all = await db.Todo.ToListAsync();
    Console.WriteLine(all);
    return Results.Ok(all);
});


app.MapPost("/create", async (AppContext db, string content) => {
    var dto = new Todo();
    dto.content = content;

    if(content == null)
        return Results.NotFound();
    
    db.Todo.Add(dto);
    await db.SaveChangesAsync();
    return Results.Created();
});

// app.MapPost();


app.Run();