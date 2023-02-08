using System;

namespace Backend_Escaperoom_2.Application.Helpers
{
    public static class NumberFormatHelper
    {

        public static decimal ToDecimalNumberFormat(string number, string numberDecimalSeparator)
        {
            if (numberDecimalSeparator.Equals("."))
            {
                return Decimal.Parse(number.Replace(',', '.'));
            }
            else
            {
                return Decimal.Parse(number.Replace('.', ','));
            }
        }

        public static string ToReplaceNumberFormat(string number, string numberDecimalSeparator)
        {
            if (numberDecimalSeparator.Equals("."))
            {
                return number.Replace(',', '.');
            }
            else
            {
                return number.Replace('.', ',');
            }
        }

    }
}
