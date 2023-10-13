using System.Collections;
using System.Text;
using System.Text.Json.Nodes;
using Newtonsoft.Json;

namespace XiaoLi
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Task[] tasks = new Task[1];
            for (var i = 0; i < tasks.Length; i++) {
                tasks[i] = Task.Run(() => AddSign());
            }
            await Task.WhenAll(tasks);
        }

        static public async Task AddSign()
        {
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            using (HttpClient client = new HttpClient())
            {
                
                var payload = new Dictionary<string, object> {
                    { "11", "350204198810094022" },
                    { "trd_session", "1529d8b09f0ece2a7e7ed497fc25fde70ae56ce8549e9391d0e208c6a025c583" },
                    { "id", "071b020f562411ee9dd700163e04ba6e" },
                    { "timezone", -480 }
                };
                string json = JsonConvert.SerializeObject(payload);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                while (true) {
                    HttpResponseMessage response = await client.PostAsync("https://baoming.lifang.biz/index.php/apii/addSign", content);

                    // Check the response status code
                    if (response.IsSuccessStatusCode)
                    {
                        // Read the response content as a string
                        byte[] res = await response.Content.ReadAsByteArrayAsync();
                        string responseBody = System.Text.Encoding.UTF8.GetString(res);
                        AddSignResponse result = JsonConvert.DeserializeObject<AddSignResponse>(responseBody);
                        Console.WriteLine(result.Code);
                        Console.WriteLine(result.Msg);
                    }
                    else
                    {
                        Console.WriteLine("The request was not successful. Status code: " + response.StatusCode);
                    }
                }
            }
        }
    }
}