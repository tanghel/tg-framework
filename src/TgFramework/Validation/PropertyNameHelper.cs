using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace TgFramework.Validation
{
    public static class PropertyNameHelper
    {
        public static string GetLabelText(PropertyInfo property)
        {
            string labelText;
            var displayAttribute = property.GetCustomAttributes(true)
                .FirstOrDefault(x => x.GetType() == typeof(DisplayAttribute))
                as DisplayAttribute;
            if (displayAttribute != null)
            {
                labelText = displayAttribute.Name;
            }
            else
            {
                labelText = SplitCamelCaseString(property.Name);
            }

            return labelText + ":";
        }

        private static string SplitCamelCaseString(string camelCaseString)
        {
            return Regex.Replace(camelCaseString, @"(\B[A-Z]+?(?=[A-Z][^A-Z])|\B[A-Z]+?(?=[^A-Z]))", " $1");
        }
    }
}