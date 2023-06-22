using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ToDoApp.Models.Users;
using ToDoApp.Services.UserService;

namespace ToDoAppWeb.KafkaProducer
{
    public class TopicProducer : BackgroundService, ITopicProducer
    {
        private const string topic = "Users";
        public User Message = new User();

        public TopicProducer()
        {
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var settings = builder.GetSection("KafkaSettings").Get<IDictionary<string, string>>();

            using (var producer = new ProducerBuilder<string, string>(
                settings).Build())
            {
                var message = GenerateMessage();
                var numProduced = 0;

                producer.Produce(topic, message,
                    (deliveryReport) =>
                    {
                        if (deliveryReport.Error.Code != ErrorCode.NoError)
                        {
                            Console.WriteLine($"Failed to deliver message: {deliveryReport.Error.Reason}");
                        }
                        else
                        {
                            Console.WriteLine($"Produced event to topic {topic}: user = {message.Value}");
                            numProduced += 1;
                        }
                    });


                producer.Flush(TimeSpan.FromSeconds(10));
                Console.WriteLine($"{numProduced} messages were produced to topic {topic}");
                return Task.CompletedTask;
            }
        }

        protected Message<string, string> GenerateMessage()
        {
            var rawMessage = Message;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"username: {rawMessage.Username}");
            sb.AppendLine($"firstname: {rawMessage.FirstName}");
            sb.AppendLine($"lastname: {rawMessage.LastName}");
            sb.AppendLine($"email: {rawMessage.Email}");
            sb.AppendLine($"role: {rawMessage.Role}");

            var message = new Message<string, string> { Key = rawMessage.Id.ToString(), Value = sb.ToString() };

            return message;
        }

        public Task Produce()
        {
            CancellationTokenSource source = new CancellationTokenSource();
            CancellationToken token = source.Token;
            return ExecuteAsync(token);
        }

    }
}
