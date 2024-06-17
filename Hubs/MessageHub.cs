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

        //send message to the one calling 
        public Task SendMessageToCaller(string message)
        {
            return Clients.Caller.SendAsync("RecieveMessage", "got your message:" + message);
        }

        //send message to a specific user
        public Task SendMessageToUser(string connectionid,string message){
            return Clients.Client(connectionid).SendAsync("RecieveMessage",message);
        }
        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("UserConnected",Context.ConnectionId);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await Clients.All.SendAsync("UserDisconnected",Context.ConnectionId);
            await base.OnDisconnectedAsync(exception);
        }
    }
    }

