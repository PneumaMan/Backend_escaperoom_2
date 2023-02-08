using Backend_Escaperoom_2.Application.Interfaces;
using System.Reflection;

namespace Backend_Escaperoom_2.WebApi.Services
{
    public class AppVersionService : IAppVersionService
    {
        public string Version => Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;
    }
}
