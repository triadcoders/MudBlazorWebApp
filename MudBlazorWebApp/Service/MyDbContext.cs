using Microsoft.EntityFrameworkCore;

namespace MudBlazorWebApp.Service;

public class MyDbContext : DbContext
{
    //CH202308210
    //BlazorDemo
    
    // protected override void OnConfiguring(DbContextOptionsBuilder options)
    //     => options.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=MyDatabase");
    //

    // protected override void OnConfiguring(DbContextOptionsBuilder options)
    //     => options.UseSqlServer(@"Data Source=SERVER\MSSQLLocalDB;Initial Catalog=MyDatabase;Trusted_Connection=false");
    //
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = "Server=SABRECHAD;Database=BlazorDemo;User Id=theadmin;Password=password;;TrustServerCertificate=true";
        optionsBuilder.UseSqlServer(connectionString, b => b.MigrationsAssembly("MudBlazorWebApp")); // Adjust assembly name
    }
    
    
    public DbSet<Customer> Customers { get; set; }

    
    public static string Report()
    {
        return "DB SERVER";
    }
}

public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; }
}

