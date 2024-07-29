using CategoriesProcessorWithOpenAi.Controllers;
using CategoriesProcessorWithOpenAi.Models;
using CategoriesProcessorWithOpenAi.Services;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;

namespace CategoriesProcessorWithOpenAi.Tests.ControllerTests
{
    public class CategoryProcessorControllerTests
    {
        private readonly IOpenAIServices _openAIServices;
        private readonly CategoryProcessorController _categoryProcessorController;

        public CategoryProcessorControllerTests()
        {
            _openAIServices = A.Fake<IOpenAIServices>();
            _categoryProcessorController = new CategoryProcessorController(_openAIServices);
        }

        [Fact]
        public async void CategoryProcessorController_GetPopularAttributes_ReturnsCategoryAttributes()
        {
            // Arrange
            var testCategories = new List<Category>
            {
                new Category
                {
                    CategoryName = "TestCategory",
                    SubCategories = new List<SubCategory>
                    {
                        new SubCategory { CategoryId = 1, CategoryName = "TestSubCategory1" },
                        new SubCategory { CategoryId = 2, CategoryName = "TestSubCategory2" }
                    }
                }
            };

            var expectedCategoryAttributes = new List<CategoryAttributes>
            {
                new CategoryAttributes { CategoryId = 1, Attributes = new List<string> { "attr1", "attr2", "attr3" } },
                new CategoryAttributes { CategoryId = 2, Attributes = new List<string> { "attr4", "attr5", "attr6" } }
            };

            A.CallTo(() => _openAIServices.GetPopularAttributesAsync(testCategories))
                .Returns(Task.FromResult(expectedCategoryAttributes));

            // Act
            var result = await _categoryProcessorController.GetPopularAttributes(testCategories) as OkObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);

            var actualCategoryAttributes = result.Value as List<CategoryAttributes>;
            actualCategoryAttributes.Should().NotBeNull();
            actualCategoryAttributes.Should().BeEquivalentTo(expectedCategoryAttributes, options => options
                .Including(x => x.CategoryId)
                .Including(x => x.Attributes));
        }
    }
}
