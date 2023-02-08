using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Backend_Escaperoom_2.Domain.Settings;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Backend_Escaperoom_2.Application.Extension
{
    public static class HttpContextAccessorExtension
    {
        public static CultureInfo GetCultureInfo(this IHeaderDictionary header, string defaultLanguage)
        {

            using (new CultureScope(new CultureInfo("en")))
            {
                var languages = new List<(string, decimal)>();
                string acceptedLanguage = header["Accept-Language"];
                if (acceptedLanguage == null || acceptedLanguage.Length == 0) 
                {
                    return new CultureInfo(defaultLanguage);
                }
                string[] acceptedLanguages = acceptedLanguage.Split(',');
                foreach (string accLang in acceptedLanguages)
                {
                    var languageDetails = accLang.Split(';');
                    if (languageDetails.Length == 1)
                    {
                        languages.Add((languageDetails[0], 1));
                    }
                    else
                    {
                        languages.Add((languageDetails[0], Convert.ToDecimal(languageDetails[1].Replace("q=", ""))));
                    }
                }
                string languageToSet = languages.OrderByDescending(a => a.Item2).First().Item1;
                return new CultureInfo(languageToSet);
            }
        }
    }
}
