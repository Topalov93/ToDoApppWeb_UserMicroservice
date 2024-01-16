using System.Threading.Tasks;
using System.Threading;
using ToDoApp.Models.Users;

namespace ToDoAppWeb.KafkaProducer
{
    public interface IEventProducer
    {
        public Task Produce(string userId, User message);
    }
}
