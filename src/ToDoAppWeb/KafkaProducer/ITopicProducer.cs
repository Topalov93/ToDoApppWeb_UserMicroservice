using System.Threading.Tasks;
using System.Threading;
using ToDoApp.Models.Users;

namespace ToDoAppWeb.KafkaProducer
{
    public interface ITopicProducer
    {
        public Task Produce();
    }
}
