using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Hello, Receiving 1!");

        ConnectionFactory factory = new() { HostName = "localhost" };

        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            Console.WriteLine("Code name:");
            var queueName = Console.ReadLine();
            channel.QueueDeclare(
                queue: queueName,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            EventingBasicConsumer consumer = new(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                if (message.Contains("1"))
                    Thread.Sleep(5000);

                Console.WriteLine($"{DateTime.Now} [x] Received {message}");
            };

            channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

            Console.WriteLine("Press key to exit"); ;
            Console.ReadLine();
        }


    }
}