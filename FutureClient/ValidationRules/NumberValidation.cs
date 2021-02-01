using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace FutureClient.ValidationRules
{
    public class NumberValidation : ValidationRule
    {
        public  string ValidType { get; set; }
        public decimal Ticks { get; set; }
        public decimal Min { get; set; }
        public decimal Max { get; set; }

        public NumberValidation()
        {
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            Global.ValidateDict[ValidType] = false;
            decimal num = 0m;

            try
            {
                if (((string)value).Length > 0)
                    num = decimal.Parse((String)value);
            }
            catch (Exception e)
            {
                return new ValidationResult(false, $"Illegal characters or {e.Message}");
            }

            if ((num < Min) || (num > Max))
            {
                return new ValidationResult(false,
                  $"数值范围: {Min}-{Max}.");
            }
            if (num % Ticks != 0)
            {
                return new ValidationResult(false,
                  $"最小变动单位: {Ticks}");
            }
            Global.ValidateDict[ValidType] = true;
            return ValidationResult.ValidResult;
        }
    }
}
