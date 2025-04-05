using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consoles.HttpClients
{
    // Todo myDeserializedClass = JsonConvert.DeserializeObject<Todo>(myJsonResponse);
    public class Todo
    {
        public int userId { get; set; }
        public int id { get; set; }
        public string title { get; set; }
        public bool completed { get; set; }

        public override string? ToString()
        {
            return $"{userId} {id} {title} {completed}";
        }
    }
}
