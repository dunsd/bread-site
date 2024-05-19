using Microsoft.AspNetCore.Mvc;
using webapi;
using webapi.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();
builder.Services.AddDbContext<RecipeDbContext>();
builder.Services.AddScoped<IRecipeRepository, RecipeRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(p => p.WithOrigins("http://localhost:3000")
.AllowAnyHeader().AllowAnyMethod());

app.UseHttpsRedirection();


app.MapGet("/recipes", (IRecipeRepository repo) => repo.GetAll())
    .Produces<RecipeDto[]>(StatusCodes.Status200OK);
app.MapGet("/recipe/{recipeId:int}", async (int recipeId, IRecipeRepository repo) => 
{
    var recipe = await repo.Get(recipeId);
    if (recipe == null)
    {
        return Results.Problem($"Recipe with id {recipeId} not found",
        statusCode: 404);
    }
    return Results.Ok(recipe);
}).ProducesProblem(404).Produces<RecipeDetailDto>(StatusCodes.Status200OK);

app.MapPost("/recipes", async ([FromBody]RecipeDetailDto dto, IRecipeRepository repo) => 
{
    var newRecipe = repo.Add(dto);
    return Results.Created($"/recipe/{newRecipe.Id}", newRecipe);
}).Produces<RecipeDetailDto>(StatusCodes.Status201Created);

app.MapPut("/recipes", async ([FromBody]RecipeDetailDto dto, IRecipeRepository repo) => 
{
    if (await repo.Get(dto.Id) == null) 
    {
        return Results.Problem("Recipe {recipe.Id} not found",
        statusCode: 404);
    }
    var updatedRecipe = await repo.Update(dto);
    return Results.Ok(updatedRecipe);
}).ProducesProblem(404).Produces<RecipeDetailDto>(StatusCodes.Status200OK);

app.MapDelete("/recipes/{recipeId:int}", async (int recipeId,IRecipeRepository repo) => 
{ 
    if (await repo.Get(recipeId) == null)
    {
        return Results.Problem($"Recipe {recipeId} not found", statusCode:404);
    }
    await repo.Delete(recipeId);
    return Results.Ok();
}).ProducesProblem(404).Produces(StatusCodes.Status200OK);

app.UseAuthorization();

app.MapControllers();

app.Run();
