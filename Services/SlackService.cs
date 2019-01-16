using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

using TodoApi.Models;

namespace TodoApi.Services
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public class SlackService: ISlackService {

       private HttpClient client;

       public SlackService() {
           client = new HttpClient();
            client.BaseAddress = new Uri("https://hooks.slack.com/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
       }

       
        public async Task<string> sendAsyncRequest(Message message) 
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(
                "services/TF3MRCH6C/BF4DZN410/6awHiKdCe4UuC0x5uznsU44J", message);
            
            response.EnsureSuccessStatusCode();
            Console.Write(response.Content);
            return "Message Sent";
        }
    }
}    