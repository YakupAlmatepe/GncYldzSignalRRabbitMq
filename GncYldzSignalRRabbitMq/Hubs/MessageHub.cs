using Microsoft.AspNetCore.SignalR;

namespace GncYldzSignalRRabbitMq.Hubs
{
    public class MessageHub : Hub
    {

        public async Task SendMessageAsync(string message)
        {

            await Clients.All.SendAsync("receiveMessage", message);// subscribe olan tüm clientsler gönderilen nasajı alabilir
        }
    }
}
