using FotoQuest.Application.Interfaces;
using System;

namespace FotoQuest.Infrastructure.Shared.Services
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime NowUtc => DateTime.UtcNow;
    }
}
