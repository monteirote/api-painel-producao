using System;
using System.ComponentModel.DataAnnotations;

namespace api_painel_producao.Validators
{

    public class EnumValidationAttribute : ValidationAttribute {
        private readonly Type _enumType;

        public EnumValidationAttribute (Type enumType) {
            if (!enumType.IsEnum)
                throw new ArgumentException("Action failed: the type must be an enum.");

            _enumType = enumType;
        }

        protected override ValidationResult IsValid (object value, ValidationContext validationContext) {

            if (value == null)
                return ValidationResult.Success;

            string stringValue = value.ToString();

            if (Enum.IsDefined(_enumType, stringValue))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult($"The value '{stringValue}' is not valid for the enum {_enumType.Name}.");
        }
    }
}
