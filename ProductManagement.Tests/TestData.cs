using ProductManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Tests
{
    public class TestData
    {
        public static List<Product> GetTestProducts()
        {
            return new List<Product>
            {
                new Product
                {
                    Id = 1,
                    Code = "code1",
                    Nom = "product1",
                    DateDebutValidation = new DateTime(2021,05,03),
                    DateFinValidation = new DateTime(2021,05,05),
                },
                 new Product
                {
                    Id = 2,
                    Code = "code2",
                    Nom = "product2",
                    DateDebutValidation = new DateTime(2021,05,03),
                    DateFinValidation = new DateTime(2021,05,05),
                },
            };
        }
    }
}
