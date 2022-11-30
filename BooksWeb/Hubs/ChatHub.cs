using Microsoft.AspNetCore.SignalR;

namespace BooksWeb.Hubs
{
    public class ChatHub:Hub
    {
        public void OnMessage(string client, string message)
        {
            var id = this.Context.ConnectionId;           

            //send this message to everyone except the sender
            Clients.AllExcept(id).SendAsync("Broadcast", new
            {
                From=client,
                Message=message
                
            });
        }
    }
}
