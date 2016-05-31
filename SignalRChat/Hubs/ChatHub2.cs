using System;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SignalRChat
{
    public class ChatHub2 : Hub
    {
        
        public void Send(string name, string message)
        {
            // Call the addNewMessageToPage method to update clients.
            Clients.All.addNewMessageToPage(name, message);
        }

        public override Task OnConnected()
        {
            string name = Context.User.Identity.Name;
            Groups.Add(Context.ConnectionId, name);

            return base.OnConnected();
        }
    }
}