using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace webapi.Data
{
    public interface IRecipeRepository 
    {
        Task<List<RecipeDto>> GetAll();        
        Task<RecipeDetailDto?> Get(int id);
    }
    public class RecipeRepository : IRecipeRepository
    {
        private readonly RecipeDbContext context;
        
        public RecipeRepository(RecipeDbContext context) 
        {
            this.context = context;
        }

        public async Task<List<RecipeDto>> GetAll() 
        {
            return await context.Recipes.Select(rec => new RecipeDto(rec.Id, rec.Title, rec.Description, rec.Image)).ToListAsync();
        }

        public async Task<RecipeDetailDto?> Get(int id) 
        {
            var e = await context.Recipes.SingleOrDefaultAsync(rec => rec.Id == id);

            if(e == null)
            {
                return null;
            }

            return new RecipeDetailDto(e.Id, e.Title, e.Description, e.Image);
        }
    }
}