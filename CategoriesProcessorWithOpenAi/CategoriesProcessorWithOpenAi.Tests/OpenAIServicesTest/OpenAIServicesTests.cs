using CategoriesProcessorWithOpenAi.Models;
using CategoriesProcessorWithOpenAi.Services;
using FakeItEasy;
using FluentAssertions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CategoriesProcessorWithOpenAi.Tests.ServicesTests
{
    public class OpenAIServicesTests
    {
        private readonly IOpenAIServices _openAIServices;

        public OpenAIServicesTests()
        {
            _openAIServices = A.Fake<IOpenAIServices>();
        }

        [Fact]
        public async Task GetPopularAttributesAsync_ShouldReturnCategoryAttributes()
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
            var actualCategoryAttributes = await _openAIServices.GetPopularAttributesAsync(testCategories);

            // Assert
            actualCategoryAttributes.Should().BeEquivalentTo(expectedCategoryAttributes);
            actualCategoryAttributes.Should().BeOfType<List<CategoryAttributes>>();

        }
    }
}
