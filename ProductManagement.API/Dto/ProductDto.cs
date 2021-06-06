using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProductManagement.API.Dto
{
    public class ProductDto : IValidatableObject
    {
        [Required]
        public string Nom { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public DateTime DateDebutValidation { get; set; }
        [Required]
        public DateTime DateFinValidation { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(DateFinValidation < DateDebutValidation)
            {
                yield return new ValidationResult($"{nameof(DateFinValidation)} must be greater than {nameof(DateDebutValidation)}");
            }

        }
    }
}
