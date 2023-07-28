using EmailSenderExampleCunsomer;
using Microsoft.AspNetCore.SignalR.Client;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;


public class Program
{
     static async  Task Main(string[] args)
    {
        HubConnection connectionSignalR = new HubConnectionBuilder().WithUrl("https://localhost:5001/messagehub").Build();
        await connectionSignalR.StartAsync();

        ConnectionFactory factory = new ConnectionFactory();
        factory.Uri = new Uri("http://localhost:15672");
        using IConnection connection = factory.CreateConnection();
        using IModel channel = connection.CreateModel();//rabbitmqya bağlantı kanalı oluşturduk

        channel.QueueDeclare("messagequeue", false, false, false);// kuyruk oluşturduk

        EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
        channel.BasicConsume("messagequeue", true, consumer);//mesajlar consume edildi

        consumer.Received += async(s, e) => //comsume edilen mesaj burada yakalanacak
        {

            string serializeData = Encoding.UTF8.GetString(e.Body.Span);
            User user = JsonSerializer.Deserialize<User>(serializeData);

            EmailSender.Send(user.Email, user.Message);
            Console.WriteLine($"{user.Email} mail gönderilmiştir.");


          await  connectionSignalR.InvokeAsync("SendMessageAsync", $"{user.Email} mail gönderilmiştir.");
        };
    }
}