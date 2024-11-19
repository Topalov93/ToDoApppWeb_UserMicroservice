using Confluent.Kafka;
using DAL.Models;
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
    public class EventProducer : BackgroundService, IEventProducer
    {
        private const string topic = "users";
        public UserEvent Message = null;

        public EventProducer()
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

                if (message is null) return Task.CompletedTask;

                producer.Produce(topic, message,
                    (deliveryReport) =>
                    {
                        if (deliveryReport.Error.Code != ErrorCode.NoError)
                        {
                            Console.WriteLine($"Failed to deliver message: {deliveryReport.Error.Reason}");
                        }
                        else
                        {
                            Console.WriteLine(message.Value);
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

            if (rawMessage is null) return null;

            var user = new UserEvent()
            {
                Id = rawMessage.Id,
                FullName = rawMessage.FullName,
                Email = rawMessage.Email,
            };

            var userAsJson = JsonConvert.SerializeObject(user);
            var message = new Message<string, string> { Key = "", Value = userAsJson };

            return message;
        }

        public Task Produce(string userId, User rawMessage, string action)
        {
            Message.Id = userId;
            if (action == "edit")
            {
                Message.Email = rawMessage.Email;
                Message.FullName = rawMessage.FirstName + " " + rawMessage.LastName;
            }
            CancellationTokenSource source = new CancellationTokenSource();
            CancellationToken token = source.Token;
            return ExecuteAsync(token);
        }
    }
}
