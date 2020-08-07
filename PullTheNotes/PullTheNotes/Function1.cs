using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Data.SqlClient;

namespace PullTheNotes
{
    public static class Function1
    {
        public static string connectToDB(string query)
        {
            string ADO = "Server=tcp:emodiamobile.database.windows.net,1433;Initial Catalog=EmoDia;Persist Security Info=False;User ID=kasaevasab;Password=Kasab3004@;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            using (var connection = new SqlConnection(ADO))
            {
                connection.Open();
                using (var cmd = new SqlCommand(query, connection))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Read();
                    try
                    {
                        var s = reader.ToString();
                        return s;
                    }
                    catch
                    {
                        return "";
                    }
                }
            }
        }

        [FunctionName("Function1")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            string query = data?.query;

            return new OkObjectResult(connectToDB(query));
        }

    }
}
