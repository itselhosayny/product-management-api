using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagement.API.Dto
{
    public class ProductDto
    {
        public string Nom { get; set; }
        public string Code { get; set; }
        public DateTime DateDebutValidation { get; set; }
        public DateTime DateFinValidation { get; set; }
    }
}
