using System;
using System.Collections.Concurrent;
using System.Linq;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Hubs;

namespace SignalRChat
{
    [HubName("Chat")]
    public class ChatHub : Hub
    {
        public static ConcurrentDictionary<string, UserData> UserList = new ConcurrentDictionary<string, UserData>();

        public void Send(string name, string message)
        {
            // Call the addNewMessageToPage method to update clients.
            var msg = String.Format("{0} : {1}", Context.ConnectionId, message);
            Clients.All.addNewMessageToPage(name, msg);
        }

        public void JoinRoom(string name, string userToChatWith, string room) {
            //NOTE: This is not persisted.
            Groups.Add(Context.ConnectionId, room);
            var key = name + room;
            UserList.TryAdd(key, new UserData {RoomId = room, Connected = true, ConnectionId = Context.ConnectionId, UserName = name});
            var users = userToChatWith.Split(';');
            foreach (var user in users) {
                key = user + room;
                UserList.TryAdd(key, new UserData { RoomId = room, Connected = true, ConnectionId = "", UserName = user });
            }
        }

        public void SendMessageToRoom(string name, string room, string message)
        {
            var msg = String.Format("{0} : {1}", Context.ConnectionId, message);
            // broadcast all users in this room
            var usersInRoom = UserList.Where(a => a.Value.RoomId.Equals(room, StringComparison.OrdinalIgnoreCase));
            var returnUserList = usersInRoom.Aggregate("", (current, user) => current + ";" + user.Value.UserName);
            Clients.All.isUserInRoom(returnUserList.Substring(1), room);

            Clients.Group(room).newMessage(name, msg);
        }

        public override Task OnConnected()
        {
            string name = Context.User.Identity.Name;
            Groups.Add(Context.ConnectionId, name);

            return base.OnConnected();
        }
    }

    public class UserData
    {
        public string ConnectionId { get; set; }
        public bool Connected { get; set; }
        public string Ip { get; set; }
        public string UserName { get; set; }
        public string RoomId { get; set; }
    }
}