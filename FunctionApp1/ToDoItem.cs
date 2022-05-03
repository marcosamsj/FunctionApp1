using System;
using System.Collections.Generic;

namespace FunctionApp1
{
    public class HealthStatus
    {
        public Guid patientId { get; set; }

        public string healthStatus { get; set; }

        public IEnumerable<string> symptoms { get; set; }

        public DateTime submittedOn { get; set; }
    }
}
