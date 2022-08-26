using Microsoft.AspNetCore.SignalR;
using SimpleChat.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleChat.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IChatRoomManager _chatRoomManager;

        public ChatHub(IChatRoomManager chatRoomManager)
        {
            _chatRoomManager = chatRoomManager;
        }

        public async Task JoinRoom(string username, string room)
        {
            _chatRoomManager.AddToChatRoom(username, Context.ConnectionId, room);
            await Groups.AddToGroupAsync(Context.ConnectionId, room);
            await Clients.OthersInGroup(room).SendAsync("UserEntered", username);
        }

        public async Task SendMessage(string message, string room)
        {
            var chatUser = _chatRoomManager.GetUserByConnectionID(Context.ConnectionId);
            await Clients.OthersInGroup(room).SendAsync("ReceiveMessage", chatUser.Username, message);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var chatUser = _chatRoomManager.GetUserByConnectionID(Context.ConnectionId);
            _chatRoomManager.RemoveFromChatRoom(chatUser);
            await Clients.OthersInGroup(chatUser.AssignedRoom).SendAsync("UserLeft", chatUser.Username);

            await base.OnDisconnectedAsync(exception);
        }
    }
}
