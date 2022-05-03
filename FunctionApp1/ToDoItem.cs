namespace FunctionApp1
{
    public class HealthStatus
    {
        public Guid PatientId { get; set; }

        public string HeatlhStatus { get; set; }

        public IEnumerable<string> Symptoms { get; set; }

        public DateTime SubmittedOn { get; set; }
    }
}
