using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Code { get; set; }
        public DateTime DateDebutValidation { get; set; }
        public DateTime DateFinValidation { get; set; }
    }
}
