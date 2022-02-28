using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi.Data;

public class AppDbContext : IdentityDbContext 
{
    public AppDbContext(DbContextOptions<AppDbContext> options):base(options) { }

    public DbSet<Todo> Todos => Set<Todo>();

}