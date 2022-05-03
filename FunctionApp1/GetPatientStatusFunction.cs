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

namespace HealthStatusFunctionApp
{
    public class GetPatientStatusFunction
    {
        private readonly ILogger<GetPatientStatusFunction> _logger;

        public GetPatientStatusFunction(ILogger<GetPatientStatusFunction> log)
        {
            _logger = log;
        }

        [FunctionName("GetPatientStatus")]
        public async Task<IActionResult> Run(
           [HttpTrigger(AuthorizationLevel.Function, "get", Route = "healthstatus/{patientId}")] HttpRequest req, [CosmosDB(
                databaseName: "patients",
                collectionName: "healthstatus",
                ConnectionStringSetting = "CosmosDBConnection",
                SqlQuery = "select * from ToDoItems r where r.id = {patientId} and DateTimeDiff(\"day\", r.submittedOn, GetCurrentDateTime ()) >= 14",
                PartitionKey = "{Query.pid}")]IEnumerable<HealthStatus> sumbissions)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            return new OkObjectResult(sumbissions);
        }



    }
}
