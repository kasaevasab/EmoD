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

namespace EmoDia
{
    public static class AddToThankingDiary 
    {
        public static string ADO = "Server=tcp:emodiamobile.database.windows.net,1433;Initial Catalog=EmoDia;Persist Security Info=False;User ID=kasaevasab;Password=Kasab3004@;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        private static void connectToDB(string userId, string date, string text, ILogger log)
        {
            using (var connection = new SqlConnection(ADO))
            {
                connection.Open();
                log.LogInformation("Connection opened");
                string query = $@"insert into ThankDiary(userId, recDate, rectext) values ('{userId}', '{date}', '{text}');";
                using (var cmd = new SqlCommand(query, connection))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    log.LogInformation("Query executed");
                    reader.Close();
                }
            }
        }

        [FunctionName("Function1")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            string userId = data?.userId;
            string date = data?.date;
            string text = data?.text;
            

            connectToDB(userId, date, text, log);
            string responseMessage = $"Data has been received. You've sent {userId} {date} {text}";

            return new OkObjectResult(responseMessage);
        }
    }
}
