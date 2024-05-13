using Microsoft.EntityFrameworkCore;

public static class SeedData 
{
    public static void Seed(ModelBuilder builder) 
    {
        builder.Entity<RecipeEntity>().HasData(new List<RecipeEntity> {
            new RecipeEntity
            {
                Id = 1,
                Title = "Sample Title 1",
                Description = "This is the description for sample entity 1.",
                Image = ""
            },
            new RecipeEntity
            {
                Id = 2,
                Title = "Sample Title 2",
                Description = "Description for sample entity 2.",
                Image = ""
            },
            new RecipeEntity
            {
                Id = 3,
                Title = "Another Title",
                Description = "Yet another description.",
                Image = "https://example.com/image3.jpg"
            },
            new RecipeEntity
            {
                Id = 4,
                Title = "Title 4",
                Description = "Description for entity with Id 4.",
                Image = "https://example.com/image4.jpg"
            },
            new RecipeEntity
            {
                Id = 5,
                Title = "Fifth Entity",
                Description = "Description for the fifth entity.",
                Image = "https://example.com/image5.jpg"
            }
        });

        
    }
}