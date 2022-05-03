namespace FunctionApp1
{
    public class ToDoItem
    {
        public string ToDoItemId { get; set; }

        public string ToDoItemPartitionKeyValue { get; set; }

        public string HealthStatus { get; set; }
    }
}