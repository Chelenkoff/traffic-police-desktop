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
               message = "Не сте въвели име!";
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
        public static string validateEGN(string egn)
        {
            if (String.IsNullOrWhiteSpace(egn))
                message = "Не сте въвели 'ЕГН'!";
            else if (egn.Contains(" "))
                message = "Не е възможно да имате интервали в полето 'ЕГН'!";
            else if (specialCharactersRegex.IsMatch(egn) || !IsDigitsOnly(egn))
                message = "'ЕГН' се състои само от цифри!";
            else if (egn.Length != 10)
                message = "'ЕГН' се състои от точно 10 цифри!";

            else message = null;
            return message;
        }

        //Password validator
        public static string validatePass(string pass)
       {


           if (String.IsNullOrWhiteSpace(pass))
               message = "Паролата не може да съдържа само интервали!";
           else if (pass.Contains(" "))
               message = "Не е възможно да имате интервали в паролата!";
           else
               message = null;
           return message;
       }

        public static string validateId(string id)
       {
           if (String.IsNullOrWhiteSpace(id))
               message = "Не сте въвели 'Служебен номер'!";
           else if (id.Contains(" "))
               message = "Не е възможно да имате интервали в полето 'Служебен номер'!";
           else if (specialCharactersRegex.IsMatch(id) || !IsDigitsOnly(id))
               message = "'Служебен номер' се състои само от цифри!";
           else   message = null;
           return message;
       }
        public static string validateDate(string date)
        {
            if (String.IsNullOrWhiteSpace(date))
                message = "Не сте въвели дата!";
            else message = null;
            return message;
        }

        public static string validateBirthPlace(string place)
        {
            if (String.IsNullOrWhiteSpace(place))
                message = "Не сте въвели месторождение!";
            else message = null;
            return message;
        }
        public static string validateResidence(string res)
        {
            if (String.IsNullOrWhiteSpace(res))
                message = "Не сте въвели постоянен адрес!";
            else message = null;
            return message;
        }
        public static string validateIssuer(string iss)
        {
            if (String.IsNullOrWhiteSpace(iss))
                message = "Не сте въвели органа издал документа!";
            else message = null;
            return message;
        }
        public static string validateIssuedDate(string iss)
        {
            if (String.IsNullOrWhiteSpace(iss))
                message = "Не сте въвели дата на издаване на документа!";
            else message = null;
            return message;
        }
        public static string validateExpirydDate(string exp)
        {
            if (String.IsNullOrWhiteSpace(exp))
                message = "Не сте въвели дата на валидност на документа!";
            else message = null;
            return message;
        }

       static bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }
    }
}
