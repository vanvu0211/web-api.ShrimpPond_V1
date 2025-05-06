using Microsoft.AspNetCore.SignalR;
namespace ShrimpPond.API.Hubs
{
    public class NotificationHub: Hub
    {
        // Phương thức để client gọi và nhận thông báo từ server
        public async Task SendMessage(string user, string message)
        {
            // Gửi thông điệp đến tất cả client đang kết nối
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        // Sự kiện khi client kết nối
        public override async Task OnConnectedAsync()
        {
            await Clients.Caller.SendAsync("ReceiveMessage", "System", $"Connected with ID: {Context.ConnectionId}");
            await base.OnConnectedAsync();
        }

        // Sự kiện khi client ngắt kết nối
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await Clients.All.SendAsync("ReceiveMessage", "System", $"Client {Context.ConnectionId} disconnected");
            await base.OnDisconnectedAsync(exception);
        }
    }
}
