using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

public class AppContext : DbContext {
    public DbSet<Todo> Todo {get; set;}
    // readonly contextstring = builder.Configuration.GetValue<string>("ConnectionStrings:DefaultConnection");


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySQL("server=localhost;database=todo;user=admin;password=pass");
    }
}