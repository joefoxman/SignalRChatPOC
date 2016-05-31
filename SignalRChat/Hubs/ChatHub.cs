using System;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNet.SignalR.Hubs;

namespace SignalRChat
{
    [HubName("Chat")]
    public class ChatHub : Hub
    {
        
        public void Send(string name, string message)
        {
            // Call the addNewMessageToPage method to update clients.
            var msg = String.Format("{0} : {1}", Context.ConnectionId, message);
            Clients.All.addNewMessageToPage(name, msg);
        }

        public void JoinRoom(string room)
        {
            //NOTE: This is not persisted.
            Groups.Add(Context.ConnectionId, room);
        }

        public void SendMessageToRoom(string room, string message)
        {

            var msg = String.Format("{0} : {1}", Context.ConnectionId, message);
            Clients.Group(room).newMessage(msg);
        }

        public override Task OnConnected()
        {
            string name = Context.User.Identity.Name;
            Groups.Add(Context.ConnectionId, name);

            return base.OnConnected();
        }
    }
}