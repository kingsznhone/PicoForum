using System.ComponentModel.DataAnnotations;

namespace PicoForum.Data.Validation
{
    public class IntValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (int.TryParse((string)value, out int buffer))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("Input is not integer");
        }
    }
}