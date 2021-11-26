using FotoQuest.WebApi.Application.Interfaces;
using System;

namespace FotoQuest.WebApi.Infrastructure.Shared.Services
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime NowUtc => DateTime.UtcNow;
    }
}
