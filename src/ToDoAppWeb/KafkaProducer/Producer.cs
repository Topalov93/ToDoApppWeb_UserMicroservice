using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using ToDoApp.Models.Users;

namespace ToDoAppWeb.KafkaProducer
{
    public class Producer : BackgroundService
    {
        const string topic = "Users";

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")    
                .Build();
            var settings = builder.GetSection("KafkaSettings").Get<IDictionary<string, string>>();

            using (var producer = new ProducerBuilder<string, string>(
                settings).Build())
            {
                var users = GetMessageData();
                var numProduced = 0;
                for (int i = 0; i < users.Count; ++i)
                {
                    producer.Produce(topic, JsonConvert.DeserializeObject<string>(users(i)));
                },
                        (deliveryReport) =>
                        {
                            if (deliveryReport.Error.Code != ErrorCode.NoError)
                            {
                                Console.WriteLine($"Failed to deliver message: {deliveryReport.Error.Reason}");
                            }
                            else
                            {
                                Console.WriteLine($"Produced event to topic {topic}: key = {user,-10} value = {item}");
                                numProduced += 1;
                            }
                        });
                }

                producer.Flush(TimeSpan.FromSeconds(10));
                Console.WriteLine($"{numProduced} messages were produced to topic {topic}");
                return Task.CompletedTask;
            }
        }

        public List<User> GetMessageData()
        {
            return new List<User>();
        }
    }
}
