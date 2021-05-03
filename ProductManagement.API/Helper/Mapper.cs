using ProductManagement.API.Dto;
using ProductManagement.Domain.Entities;

namespace ProductManagement.API.Helper
{
    public class Mapper
    {
        public static Product MapFrom(ProductDto productDto)
        {
            return new Product
            {
                Nom = productDto.Nom,
                Code = productDto.Code,
                DateDebutValidation = productDto.DateDebutValidation,
                DateFinValidation = productDto.DateFinValidation
            };
        }
    }
}
