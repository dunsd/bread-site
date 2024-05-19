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
        Task Delete(int id);
        Task<RecipeDetailDto> Add(RecipeDetailDto dto);
        Task<RecipeDetailDto> Update(RecipeDetailDto dto);
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
            var entity = await context.Recipes.SingleOrDefaultAsync(rec => rec.Id == id);

            if(entity == null)
            {
                return null;
            }
            return EntityToDetailDto(entity);
        }
        

        public async Task<RecipeDetailDto?> Add(RecipeDetailDto dto)
        {
            var recipeEntity = new RecipeEntity 
            {
                Title = dto.Title,
                Description = dto.Description,
                Image = dto.Image
            };        
            DtoToEntity(dto, recipeEntity);

            context.Recipes.Add(recipeEntity);
            await context.SaveChangesAsync();
            return EntityToDetailDto(recipeEntity);
        }

        public async Task<RecipeDetailDto> Update(RecipeDetailDto dto)
        {
            var entity = await context.Recipes.FindAsync(dto.Id);
            if(entity == null)
            {
                throw new ArgumentException($"Error updating recipe {dto.Id}");
            }
            DtoToEntity(dto, entity);
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return EntityToDetailDto(entity);
        }

        public async Task Delete(int id)
        {
            var entity = await context.Recipes.FindAsync(id);
            if(entity == null)
            {
                throw new ArgumentException($"Error deleting recipe {id}");
            }
            context.Recipes.Remove(entity);
            await context.SaveChangesAsync();
        }

        #region Helpers
        private static void DtoToEntity(RecipeDetailDto dto, RecipeEntity entity)
        {
            entity.Title = dto.Title;
            entity.Description = dto.Description;
            entity.Image = dto.Image;
        }

        private static RecipeDetailDto EntityToDetailDto(RecipeEntity entity)
        {
            return new RecipeDetailDto(entity.Id, entity.Title, entity.Description, entity.Image);
        }
        #endregion
    }
}