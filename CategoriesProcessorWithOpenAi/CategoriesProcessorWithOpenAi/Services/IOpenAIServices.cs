using CategoriesProcessorWithOpenAi.Models;

namespace CategoriesProcessorWithOpenAi.Services
{
    public interface IOpenAIServices
    { 
        Task<List<CategoryAttributes>> GetPopularAttributesAsync(List<Category> categories);
    }
}
