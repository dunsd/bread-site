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


app.UseAuthorization();

app.MapControllers();

app.Run();
