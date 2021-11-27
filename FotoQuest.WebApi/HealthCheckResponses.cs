using System;
using System.Collections.Generic;

namespace FotoQuest.WebApi
{
    public class HealthCheckResponses
    {
        public string Status { get; set; }

        public IEnumerable<HealthCheckResponse> HealthChecks { get; set; }

        public TimeSpan HealthCheckDuration { get; set; }
    }
}
