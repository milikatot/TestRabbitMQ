// See https://aka.ms/new-console-template for more information
using RabbitMQ.Client;
using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Hello, Sender!");

        ConnectionFactory factory = new() { HostName = "localhost" };

        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            Console.WriteLine("Routing key:");
            var routing = Console.ReadLine();

            while (true)
            {
                Console.WriteLine("Message:");
                string? message = Console.ReadLine();
                var body = Encoding.UTF8.GetBytes(message ?? "not valid message");

                channel.BasicPublish(
                        exchange: "",
                        routingKey: routing,
                        basicProperties: null,
                        body: body);

                Console.WriteLine(" [x] Sent {0}", message);

            }
        }


    }
}