using System;

using FotoQuest.Application.Interfaces;

namespace FotoQuest.Infrastructure.Shared.Services
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime NowUtc => DateTime.UtcNow;
    }
}
