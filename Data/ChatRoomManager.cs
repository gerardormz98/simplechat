using Microsoft.AspNetCore.SignalR;
using SimpleChat.Hubs;
using SimpleChat.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleChat.Data
{
    public class ChatRoomManager : IChatRoomManager
    {
        public Dictionary<string, List<ChatUser>> ChatRoomsOccupationDict { get; set; } = new Dictionary<string, List<ChatUser>>();

        public bool IsUsernameOccupied(string username, string room)
        {
            if (ChatRoomsOccupationDict.TryGetValue(room, out var users))
            {
                return users.Any(u => u.Username == username);
            }

            return false;
        }

        public bool AddToChatRoom(string username, string connectionId, string room)
        {
            if (!ChatRoomsOccupationDict.ContainsKey(room))
                ChatRoomsOccupationDict[room] = new List<ChatUser>();

            if (!IsUsernameOccupied(username, room))
            {
                ChatRoomsOccupationDict[room].Add(new ChatUser(username, connectionId));
                return true;
            }

            return false;
        }

        public void RemoveFromChatRoom(ChatUser chatUser)
        {
            if (ChatRoomsOccupationDict.TryGetValue(chatUser.AssignedRoom, out var users))
            {
                ChatRoomsOccupationDict[chatUser.AssignedRoom] = users.Where(u => u.Username != chatUser.Username).ToList();
            }
        }

        public ChatUser GetUserByConnectionID(string connectionId)
        {
            foreach (var room in ChatRoomsOccupationDict)
            {
                var userConnection = room.Value.FirstOrDefault(u => u.ConnectionID == connectionId);

                if (userConnection != null)
                {
                    userConnection.AssignedRoom = room.Key;
                    return userConnection;
                }
            }

            return null;
        }
    }
}
