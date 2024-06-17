using Microsoft.AspNetCore.SignalR;

namespace signalrnet.hubs
{
    public class MessageHub : Hub
    {
        //the send message is used to send message to all clients or target clients
        //we can have any kind of parameter and any number of parameters 
        //on senAsync method we need to pass the method that will be invoked at the 
        //recieving end i.e client and any number of the parameters.
        public Task SendMessageToAll(string message)
        {
            return Clients.All.SendAsync("RecieveMessage", message);
        }

        public Task SendMessageToCaller(string message)
        {
            return Clients.Caller.SendAsync("RecieveMessage", "got your message:" + message);
        }
        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await base.OnDisconnectedAsync(exception);
        }
    }
}
