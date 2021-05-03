using ProductManagement.API.Dto;
using ProductManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
