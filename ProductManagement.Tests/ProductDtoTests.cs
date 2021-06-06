using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ProductManagement.API.Models;
using Xunit;

namespace ProductManagement.Tests
{
    public class ProductDtoTests
    {
        public static readonly object[][] InvalidStartAndEndDates =
        {
            new object[] {new DateTime(2021, 05, 03), new DateTime(2021, 05, 03)},
            new object[] {new DateTime(2021, 05, 03), new DateTime(2021, 04, 03)},
        };

        [Theory, MemberData(nameof(InvalidStartAndEndDates))]
        public async Task Validate_ReturnEmptyListOfErrors_WhenProductIsValid(DateTime startDateTime,DateTime endDateTime)
        {
            // Arrange
            var model = new ProductDto
            {
                Code = "code1",
                Nom = "product1",
                DateDebutValidation = startDateTime,
                DateFinValidation = endDateTime,
            };
            var validationContext = new ValidationContext(model);

            // Act
            var result = model.Validate(validationContext);

            // Assert
            Assert.Single(result);
            Assert.Equal(result.First().ErrorMessage, $"{nameof(ProductDto.DateFinValidation)} must be greater than {nameof(ProductDto.DateDebutValidation)}");
        }

        public static readonly object[][] ValidStartAndEndDates =
        {
            new object[] {new DateTime(2021, 05, 03), new DateTime(2021, 06, 03)},
            new object[] {new DateTime(2021, 05, 03), new DateTime(2022, 05, 03)},
            new object[] {new DateTime(2021, 05, 03), new DateTime(2022, 05, 04)},
        };

        [Theory,MemberData(nameof(ValidStartAndEndDates))]
        public async Task Validate_ReturnListOfErrors_WhenProductIsNotValid(DateTime startDateTime, DateTime endDateTime)
        {
            // Arrange
            var model = new ProductDto
            {
                Code = "code1",
                Nom = "product1",
                DateDebutValidation = startDateTime,
                DateFinValidation = endDateTime,
            };
            var validationContext = new ValidationContext(model);

            // Act
            var result = model.Validate(validationContext);

            // Assert
            Assert.Empty(result);
        }
    }
}
