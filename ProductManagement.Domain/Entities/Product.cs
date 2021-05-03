using System;

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
