using Backend_Escaperoom_2.Application.Interfaces.Services;
using System;

namespace Backend_Escaperoom_2.Infrastruture.Shared.Services
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime Now => DateTime.Now;
    }
}
