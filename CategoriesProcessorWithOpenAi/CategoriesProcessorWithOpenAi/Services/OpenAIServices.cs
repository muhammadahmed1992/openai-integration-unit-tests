using CategoriesProcessorWithOpenAi.Configurations;
using CategoriesProcessorWithOpenAi.Models;
using Microsoft.Extensions.Options;
using OpenAI_API;
using OpenAI_API.Completions;

namespace CategoriesProcessorWithOpenAi.Services
{
    public class OpenAIServices : IOpenAIServices
    {
        private readonly OpenAIConfig _openAIConfig;

        public OpenAIServices(IOptionsMonitor<OpenAIConfig> optionsMonitor)
        {
            _openAIConfig = optionsMonitor.CurrentValue;
        }
        public async Task<List<CategoryAttributes>> GetPopularAttributesAsync(List<Category> categories)
        {
            var response = new List<CategoryAttributes>();

            foreach (var category in categories)
            {
                foreach (var subCategory in category.SubCategories!)
                {
                    var attributes = await GetPopularAttributes(subCategory);
                    response.Add(new CategoryAttributes
                    {
                        CategoryId = subCategory.CategoryId,
                        Attributes = attributes
                    });
                }
            }

            return response;
        }

        private async Task<List<string>> GetPopularAttributes(SubCategory subCategory)
        {
            var api = new OpenAI_API.OpenAIAPI(_openAIConfig.Key);
            var request = new CompletionRequest
            {
                Prompt = $"List the 3 most popular attributes for the subcategory {subCategory.CategoryName}",
                MaxTokens = 60,
                Temperature = 0.5
            };

            var response = await api.Completions.CreateCompletionAsync(request);
            var attributes = response.ToString().Split('\n').Select(a => a.Trim()).ToList();

            return attributes;
        }
    }
}
