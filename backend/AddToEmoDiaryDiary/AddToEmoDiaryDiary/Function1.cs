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

namespace AddToEmo
{
    public static class Function1
    {
        public static string ADO = "Server=tcp:emodiamobile.database.windows.net,1433;Initial Catalog=EmoDia;Persist Security Info=False;User ID=kasaevasab;Password=Kasab3004@;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        private static void connectToDB(string id, string time, string situation, string reaction, string emotion, string trigger, string curEmo, string rethinking, string conclusion, string date, ILogger log)
        {
            using (var connection = new SqlConnection(ADO))
            {
                connection.Open();
                log.LogInformation("Connection opened");
                string query = $@"insert into EmotionalDiary(userId, recTime, situation, reaction, emotion, emoTrigger, curEmotions, rethinking, conclusion, recDate) values ('{id}', '{time}', '{situation}', '{reaction}', '{emotion}', '{trigger}', '{curEmo}', '{rethinking}', '{conclusion}', '{date}');";
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
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            string id = data?.userId;
            string time = data?.recTime;
            string situation = data?.situation;
            string reaction = data?.reaction;
            string emotion = data?.emotion;
            string trigger = data?.emoTrigger;
            string curEmo = data?.curEmotions;
            string rethinking = data?.rethinking;
            string conclusion = data?.conclusion;
            string date = data?.date;

            connectToDB(id, time, situation, reaction, emotion, trigger, curEmo, rethinking, conclusion, date, log);
            string responseMessage = $"Data has been received. You've sent {id} {time} {situation} {emotion} {trigger} {curEmo} {rethinking} {conclusion} {date}";

            return new OkObjectResult(responseMessage);
        }
    }
}
