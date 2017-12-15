using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PrisonApp.Models.Validators
{
    public class PeselValidator : ValidationAttribute
    {
        private readonly Model _db = new Model();

        public PeselValidator()
        {
            Plec = null;
        }

        public PeselValidator(string plec)
        {
            Plec = plec;
        }

        public string Plec { get; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string errorMessage;
            string pesel;

            if (validationContext.DisplayName == null)
                errorMessage = "Nieprawidłowy numer PESEL";
            else
                errorMessage = FormatErrorMessage(validationContext.DisplayName);

            if (value == null)
                return ValidationResult.Success;

            if (value is string)
                pesel = value.ToString();
            else
                return new ValidationResult("Pole PESEL wymaga tylko cyfr");

            if (pesel.Length != 11)
                return new ValidationResult(errorMessage);

            var flaga = false;
            var wiezniowie = _db.Wiezniowie.Where(x => x.Pesel != pesel);
            foreach (var z in wiezniowie)
                if (z.Pesel == value.ToString())
                    flaga = true;
            if (flaga)
                return new ValidationResult("Istnieje już taki numer PESEL w bazie");


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
            } //suma kontrolna


            if (Plec != null)
            {
                var n = Convert.ToInt32(pesel[9].ToString());

                switch (Plec)
                {
                    case "K":
                        if (n % 2 != 0)
                            return new ValidationResult("Płeć niezgodna z numerem PESEL - mężczyzna");
                        break;

                    case "M":
                        if (n % 2 != 1)
                            return new ValidationResult("Płeć niezgodna z numerem PESEL - kobieta");
                        break;

                    default:
                        var PlecInfo = validationContext.ObjectType.GetProperty(Plec);
                        var PlecValue = (string) PlecInfo.GetValue(validationContext.ObjectInstance, null);

                        switch (PlecValue)
                        {
                            case "K":
                                if (n % 2 != 0)
                                    return new ValidationResult("Płeć niezgodna z numerem PESEL - mężczyzna");
                                break;

                            case "M":
                                if (n % 2 != 1)
                                    return new ValidationResult("Płeć niezgodna z numerem PESEL - kobieta");
                                break;
                        }
                        break;
                }
            }

            return ValidationResult.Success;
        }
    }
}