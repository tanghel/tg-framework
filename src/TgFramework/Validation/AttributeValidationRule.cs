using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Windows.Controls;
using ValidationResult = System.Windows.Controls.ValidationResult;

namespace TgFramework.Validation
{
    public class AttributeValidationRule : ValidationRule
    {
        private readonly ValidationAttribute _attribute;
        private readonly object _dataContext;

        public AttributeValidationRule(ValidationAttribute attribute, object dataContext)
        {
            _attribute = attribute;
            _dataContext = dataContext;
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var result = _attribute.GetValidationResult(value, new ValidationContext(_dataContext));
            if (result == System.ComponentModel.DataAnnotations.ValidationResult.Success)
            {
                return ValidationResult.ValidResult;
            }

            return new ValidationResult(false, result.ErrorMessage);
        }
    }
}
