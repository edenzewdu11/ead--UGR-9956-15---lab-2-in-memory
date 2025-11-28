using Microsoft.EntityFrameworkCore;
using PizzaStore.Data;
using PizzaStore.Models;

var builder = WebApplication.CreateBuilder(args);

// Enable Swagger for minimal APIs
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// In-memory DB
builder.Services.AddDbContext<PizzaDb>(options =>
    options.UseInMemoryDatabase("PizzaDb"));

var app = builder.Build();

// Enable Swagger ALWAYS (Development + Production)
app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", () => "PizzaStore - InMemory");

// Get all pizzas
app.MapGet("/pizzas", async (PizzaDb db) =>
    await db.Pizzas.ToListAsync());

// Create pizza
app.MapPost("/pizzas", async (PizzaDb db, Pizza pizza) =>
{
    await db.Pizzas.AddAsync(pizza);
    await db.SaveChangesAsync();
    return Results.Created($"/pizzas/{pizza.Id}", pizza);
});

// Update pizza
app.MapPut("/pizzas/{id}", async (PizzaDb db, int id, Pizza updated) =>
{
    var pizza = await db.Pizzas.FindAsync(id);
    if (pizza is null) return Results.NotFound();
    pizza.Name = updated.Name;
    pizza.Description = updated.Description;
    await db.SaveChangesAsync();
    return Results.NoContent();
});

// Delete pizza
app.MapDelete("/pizzas/{id}", async (PizzaDb db, int id) =>
{
    var pizza = await db.Pizzas.FindAsync(id);
    if (pizza is null) return Results.NotFound();
    db.Pizzas.Remove(pizza);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.Run();
