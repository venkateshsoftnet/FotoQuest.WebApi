using System;
using System.Collections.Generic;
using System.Text;

namespace FotoQuest.WebApi.Application.Interfaces
{
    public interface IDateTimeService
    {
        DateTime NowUtc { get; }
    }
}
