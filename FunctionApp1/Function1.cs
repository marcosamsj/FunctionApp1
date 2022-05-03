using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;


namespace FunctionApp1
{
    public class Function1
    {
        
        private readonly ILogger<Function1> _logger;

        public Function1(ILogger<Function1> log)
        {
            _logger = log;
        }
        
        [FunctionName("Function1")]
                    public void Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "healthstatus/{patientId}")] HttpRequest req, string patientId, [CosmosDB(
            databaseName: "patients",
            collectionName: "healthstatus",
            ConnectionStringSetting = "CosmosDBConnection")]out HealthStatus status)
            {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

                         Guid patientGuid = Guid.Empty;

            if (false == Guid.TryParse(patientId, out patientGuid)) patientGuid = Guid.NewGuid();
                        
            string json = String.Empty;
            using (StreamReader streamReader = new StreamReader(req.Body))
            {
            json = streamReader.ReadToEnd();
            }
            status = JsonConvert.DeserializeObject<HealthStatus>(json);
                        status.patientId = patientGuid;
                        status.submittedOn = DateTime.UtcNow;
            }
        
    }
}
