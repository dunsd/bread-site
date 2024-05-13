using Microsoft.EntityFrameworkCore;

public class RecipeDbContext: DbContext
{
    public RecipeDbContext(DbContextOptions <RecipeDbContext> o):
        base(o) {}
    public DbSet<RecipeEntity> Recipes => Set<RecipeEntity>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        optionsBuilder.UseSqlite($"Data Source={Path.Join(path, "recipes.db")}");
    }

    protected override void OnModelCreating(ModelBuilder builder) 
    {
        SeedData.Seed(builder);
    }
}