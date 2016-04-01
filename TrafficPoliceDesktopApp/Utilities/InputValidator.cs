using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TrafficPoliceDesktopApp.Utilities
{
    public static class InputValidator
    {
        static string message;
        static Regex specialCharactersRegex = new Regex(@"[~`!@#$%^&*()+=|\{}':;.,<>/?[\]""_-]");

        //Name Validator
       public static string validateName(string name)
        {


            if (String.IsNullOrWhiteSpace(name))
               message = "Невалиден формат на име!";
            else if (name.Any(char.IsDigit))
               message = "Не е възможно в името да се съдържат цифри!";
            else if (name.Contains(" "))
               message = "Не е възможно да имате интервали в името!";
            else if (specialCharactersRegex.IsMatch(name))
               message = "Не са позволени специални символи в името!";
           else
               message = null;
           return message;
        }

        //Password validator
       public static string validatePass(string pass)
       {


           if (String.IsNullOrWhiteSpace(pass))
               message = "Въведете парола!";
           else if (pass.Contains(" "))
               message = "Не е възможно да имате интервали в паролата!";
           else
               message = null;
           return message;
       }
    }
}
