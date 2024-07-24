using Microsoft.EntityFrameworkCore;

public class Appcontext : DbContext {
    public DbSet<TodoApi> TodoContext {get; set;}

    protected override void OnConfiguring(DbContextOptionsBuilder options){
        options.UseSqlite("Data Source=Data.db");
    }
}