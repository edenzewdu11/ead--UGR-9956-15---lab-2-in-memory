using Microsoft.EntityFrameworkCore;
using PizzaStore.Models;

namespace PizzaStore.Data
{
    // DbContext represents a session with the database and gives access to DbSet<T> properties
    public class PizzaDb : DbContext
    {
        public PizzaDb(DbContextOptions<PizzaDb> options) : base(options)
        {
        }

        // Each DbSet<T> corresponds to a table in the database
        public DbSet<Pizza> Pizzas { get; set; } = default!;
    }
}
