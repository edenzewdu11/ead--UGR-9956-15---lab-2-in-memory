using System;
namespace PizzaStore.Models
{
    public class Pizza
    {
        // Primary key - EF Core will treat 'Id' as the primary key by convention
        public int Id { get; set; }
        // Nullable name and description to keep the example simple
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
