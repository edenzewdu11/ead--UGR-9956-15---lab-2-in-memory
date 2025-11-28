# PizzaStore (InMemory) - Explanation (line-by-line)

Files:
- Program.cs: Minimal API using EF Core InMemory
- Models/Pizza.cs: Entity class
- Data/PizzaDb.cs: DbContext

Program.cs (high-level)
1. using Microsoft.EntityFrameworkCore;
   - Import EF Core types (DbContext, UseInMemoryDatabase, ToListAsync, etc.)
2. using PizzaStore.Data; using PizzaStore.Models;
   - Import our project namespaces for DbContext and Pizza entity.

Builder and services:
3. var builder = WebApplication.CreateBuilder(args);
   - Create the web application builder.
4. builder.Services.AddEndpointsApiExplorer();
   - Adds minimal API endpoint metadata used by Swagger.
5. builder.Services.AddSwaggerGen();
   - Registers Swagger generator for API docs.
6. builder.Services.AddDbContext<PizzaDb>(options => options.UseInMemoryDatabase("PizzaDb"));
   - Registers the PizzaDb DbContext and configures it to use the in-memory provider.
   - "PizzaDb" is the in-memory database name (scoped to this app run).

Build and middleware:
7. var app = builder.Build();
   - Build the app pipeline.
8. if (app.Environment.IsDevelopment()) { app.UseSwagger(); app.UseSwaggerUI(); }
   - Enable Swagger only in development.

Endpoints:
9. app.MapGet("/", () => "PizzaStore - InMemory");
   - Root endpoint to verify the app is running.
10. app.MapGet("/pizzas", async (PizzaDb db) => await db.Pizzas.ToListAsync());
   - Query: return all pizzas. EF Core executes a SQL-equivalent against the provider.
11. app.MapPost("/pizzas", async (PizzaDb db, Pizza pizza) => { await db.Pizzas.AddAsync(pizza); await db.SaveChangesAsync(); return Results.Created($"/pizzas/{pizza.Id}", pizza); });
   - Insert: Adds a pizza entity and saves changes. In-memory generates Ids automatically.
12. app.MapPut("/pizzas/{id}", ...)
   - Update: Find by id, update properties, SaveChangesAsync.
13. app.MapDelete("/pizzas/{id}", ...)
   - Delete: Find by id, remove, SaveChangesAsync.
14. app.Run();
   - Run the web application.

Notes:
- InMemory provider is great for tests because data is ephemeral (lost when app stops).
- No file or external DB server is required.
