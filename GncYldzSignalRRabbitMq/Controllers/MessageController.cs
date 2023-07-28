using GncYldzSignalRRabbitMq.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace GncYldzSignalRRabbitMq.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post([FromForm ] User model)
        {
            ConnectionFactory factory = new ConnectionFactory();
            factory.Uri = new Uri("http://localhost:15672");
            using IConnection connection = factory.CreateConnection();
            using IModel channel = connection.CreateModel();//rabbitmqya bağlantı kanalı oluşturduk

            string serializeData = JsonSerializer.Serialize(model);

            channel.QueueDeclare("messagequeue",false,false,false);// kuyruk oluşturduk
            byte[] data = Encoding.UTF8.GetBytes(serializeData);//rabbite mesajlar byte olarak gelir o yuzden encodding yaptık
            channel.BasicPublish("", "messagequeue", body: data);

            return Ok();
        }
    }
}
