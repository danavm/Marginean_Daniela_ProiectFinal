using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Marginean_Daniela_ProiectFinal
{
    //validator pentru camp required
    public class StringNotEmpty : ValidationRule
    {
        //mostenim din clasa ValidationRule
        //suprascriem metoda Validate ce returneaza un
        //ValidationResult
        public override ValidationResult Validate(object value,
        System.Globalization.CultureInfo cultureinfo)
        {
            string aString = value.ToString();
            if (aString == "")
                return new ValidationResult(false, "String cannot be empty");
            return new ValidationResult(true, null);
        }
    }
    //validator pentru lungime minima a string-ului
    public class StringMinLengthValidator : ValidationRule
    {
        public override ValidationResult Validate(object value,
        System.Globalization.CultureInfo cultureinfo)
        {
            string aString = value.ToString();
            if (aString.Length < 3)
                return new ValidationResult(false, "String must have at least 3 characters!");
            return new ValidationResult(true, null);
        }
    }

    //validator pentru valorile numerice
    public class NumericValidator : ValidationRule
    {
        public override ValidationResult Validate(object value,
        System.Globalization.CultureInfo cultureinfo)
        {
            try
            {
                Convert.ToInt32(value.ToString());
            } 
            catch(Exception e)
            {
                return new ValidationResult(false, "Input must be a numeric value");
            }
            return new ValidationResult(true, null);
        }
    }

    public class TimeValidator : ValidationRule
    {
        public override ValidationResult Validate(object value,
        System.Globalization.CultureInfo cultureinfo)
        {
            try
            {
                TimeSpan.Parse(value.ToString().Trim());
            }
            catch (Exception e)
            {
                return new ValidationResult(false, "Input must be a time value with format HH:MM:SS");
            }
            return new ValidationResult(true, null);
        }
    }


}
