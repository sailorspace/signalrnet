using Microsoft.AspNetCore.SignalR;

namespace signalrnet.hubs
{
    public class MessageHub : Hub
    {
        //the send message is used to send message to all clients or target clients
        //we can have any kind of parameter and any number of parameters 
        //on senAsync method we need to pass the method that will be invoked at the 
        //recieving end i.e client and any number of the parameters.
        public Task SendMessageToClient(string message)
        {
            return Clients.All.SendAsync("RecieveMessage", message);
        }
    }
}
