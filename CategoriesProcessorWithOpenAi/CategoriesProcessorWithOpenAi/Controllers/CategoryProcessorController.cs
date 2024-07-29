using CategoriesProcessorWithOpenAi.Models;
using CategoriesProcessorWithOpenAi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CategoriesProcessorWithOpenAi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryProcessorController : ControllerBase
    {
        private readonly IOpenAIServices _openAIServices;
        public CategoryProcessorController(IOpenAIServices openAIServices)
        {
            _openAIServices = openAIServices;
        }

        [HttpPost()]
        [Route("GetPopularAttributes")]
        public async Task<IActionResult> GetPopularAttributes([FromBody] List<Category> categories)
        {
            List<CategoryAttributes> categoryAttributes = await _openAIServices.GetPopularAttributesAsync(categories);
            return Ok(categoryAttributes);
        }
    }
}
