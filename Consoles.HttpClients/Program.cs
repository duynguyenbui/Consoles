using Newtonsoft.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace Consoles.HttpClients
{
    internal class Program
    {
        private static HttpClient sharedClient = new()
        {
            BaseAddress = new Uri("https://jsonplaceholder.typicode.com"),
        };


        static async Task Main(string[] args)
        {
            using HttpResponseMessage response = await sharedClient.GetAsync("/todos/3");
            
            response.EnsureSuccessStatusCode()
                .WriteRequestToConsole();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"{jsonResponse}\n");

            Todo todo = JsonConvert.DeserializeObject<Todo>(jsonResponse);

            Console.WriteLine(todo.ToString());

        }
    }
}
