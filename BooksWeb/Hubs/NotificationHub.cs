using Microsoft.AspNetCore.SignalR;

namespace BooksWeb.Hubs
{
    public interface INotification
    {
        public void Notify();
    }
    public class NotificationHub: Hub<INotification>
    {
    }
}
