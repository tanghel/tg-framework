using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Windows.Controls;

namespace TgFramework.Validation
{
    public static class ValidationRulesExtractor
    {
        public static IEnumerable<ValidationRule> GetValidationRules(PropertyInfo property, object dataContext)
        {
            var validationRules = new List<ValidationRule>();
            foreach (var validationAttribute in property.GetCustomAttributes(true).OfType<ValidationAttribute>())
            {
                validationRules.Add(new AttributeValidationRule(validationAttribute, dataContext));
            }

            return validationRules;
        }

    }
}
