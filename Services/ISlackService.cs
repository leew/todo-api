using System.Threading.Tasks;

using TodoApi.Models;


namespace TodoApi.Services
{
    public interface ISlackService
    {
        Task<string> sendAsyncRequest(Message message);
    }
}