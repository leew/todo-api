    using System.Threading.Tasks;

using TodoApi.Models;


namespace TodoApi.Services
{
    public interface IMQTTClientService
    {
        void publish(object sender, string message);
    }
}