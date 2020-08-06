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

namespace AddToAccDB
{  
    public static class Function1
    {
        public static string ADO = "Server=tcp:emodiamobile.database.windows.net,1433;Initial Catalog=EmoDia;Persist Security Info=False;User ID=kasaevasab;Password=Kasab3004@;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        private static void connectToDB(string name, string surname, string mail, string password, string role, ILogger log)
        {
            using (var connection = new SqlConnection(ADO))  
			{  
				connection.Open();  
				log.LogInformation("Connection opened");
                string query = $@"insert into Accounts(userName, userSurname, userMail, userPassword, userRole) values ('{name}', '{surname}', '{mail}', '{password}', '{role}');";
                using(var cmd = new SqlCommand(query, connection))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    log.LogInformation("Query executed");
                    reader.Close();
                }
            }
        }

        [FunctionName("AddToAccounts")]        
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            
            string name = data?.name;
            string surname = data?.surname;
            string mail = data?.mail;
            string password = data?.password;
            string role = data?.role;

            connectToDB(name, surname, mail, password, role, log);
            string responseMessage = $"Data has been received. You've sent {name} {surname} {mail} {password} {role}";

            return new OkObjectResult(responseMessage);
        }
    }
}
