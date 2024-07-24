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

builder.Services.AddDbContext<Appcontext>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi(config =>
    {
        config.DocumentTitle = "TodoAPI";
        config.Path = "";
        config.DocumentPath = "/swagger/{documentName}/swagger.json";
        config.DocExpansion = "list";
    });
}


app.MapPost("/create", async (Appcontext db, string content) => {
try{
    var add = new TodoApi();
    add.content = content;
    await db.TodoContext.AddAsync(add);
    await db.SaveChangesAsync();
    return Results.Created();
}
catch(Exception e){
        return Results.Problem("Error task creation failed " + e);
}
});

app.MapGet("/all", async (Appcontext db) => {
try {
    var all = await db.TodoContext.ToListAsync();
    return Results.Ok(all);
}
catch(Exception e){
    return Results.Problem("Error task Not Found " + e);
}
});

app.MapDelete("/delete/{taskid}", async (Appcontext db, Guid taskid) => {
try{
    var task = await db.TodoContext.FindAsync(taskid);
    if(task == null)
        return Results.NotFound();
    db.TodoContext.Remove(task);
    await db.SaveChangesAsync();
    return Results.Ok("delete success");
}
catch(Exception e){
    return Results.Problem("Error task Not Found " + e);
}
});

app.Run();