using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using System.Globalization;

namespace Backend_Escaperoom_2.Application.Wrappers
{
    public class LocalizationUtils<TEntity>
    {

        private static readonly IStringLocalizer _localizer;

        static LocalizationUtils()
        {
            var options = Options.Create(new LocalizationOptions());
            var factory = new ResourceManagerStringLocalizerFactory(options, NullLoggerFactory.Instance);
            var type = typeof(TEntity);

            _localizer = factory.Create(type);
        }

        public static string GetValue(string field, CultureInfo cultureinfo)
        {
            using (new CultureScope(cultureinfo))
            {
                return _localizer[field];
            }
        }
    }
}
