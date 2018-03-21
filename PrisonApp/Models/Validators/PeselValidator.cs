using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PrisonApplication.Models.Validators
{
  public class PeselValidator : ValidationAttribute
    {
        private readonly PrisonDatabase _db = new PrisonDatabase();
        public string Sex { get; }

        public PeselValidator()
        {
            Sex = null;
        }

        public PeselValidator(string sex)
        {
            Sex = sex;
        }

       

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string errorMessage;
            string pesel;

            if (validationContext.DisplayName == null)
                errorMessage = "Invalid pesel";
            else
                errorMessage = FormatErrorMessage(validationContext.DisplayName);

            if (value == null)
                return ValidationResult.Success;

            if (value is string)
                pesel = value.ToString();
            else
                return new ValidationResult("Pesel needs number");

            if (pesel.Length != 11)
                return new ValidationResult(errorMessage);

            var flag = false;
            var prisoners = _db.Prisoners.Where(x => x.Pesel != pesel);
            foreach (var z in prisoners)
                if (z.Pesel == value.ToString())
                    flag = true;
            if (flag)
                return new ValidationResult("Pesel exist in database");


            int[] weight = {1, 3, 7, 9, 1, 3, 7, 9, 1, 3};
            var k = 0;

            for (var i = 0; i < pesel.Length; i++)
            {
                int temp;

                if (!int.TryParse(pesel[i].ToString(), out temp))
                    return new ValidationResult(errorMessage);


                if (i + 1 == pesel.Length)
                {
                    if ((10 - k % 10) % 10 != temp)
                        return new ValidationResult(errorMessage);
                }
                else
                {
                    k += temp * weight[i];
                }
            } //Check sum


            if (Sex != null)
            {
                var n = Convert.ToInt32(pesel[9].ToString());

                switch (Sex)
                {
                    case "F":
                        if (n % 2 != 0)
                            return new ValidationResult("Invalid sex - male");
                        break;

                    case "M":
                        if (n % 2 != 1)
                            return new ValidationResult("Invalid sex - female");
                        break;

                    default:
                        var SexInfo = validationContext.ObjectType.GetProperty(Sex);
                        var SexValue = (string) SexInfo.GetValue(validationContext.ObjectInstance, null);

                        switch (SexValue)
                        {
                            case "F":
                                if (n % 2 != 0)
                                    return new ValidationResult("Invalid sex - male");
                                break;

                            case "M":
                                if (n % 2 != 1)
                                    return new ValidationResult("Invalid sex - female");
                                break;
                        }
                        break;
                }
            }

            return ValidationResult.Success;
        }
    }
}
