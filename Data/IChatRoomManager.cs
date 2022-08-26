using SimpleChat.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleChat.Data
{
    public interface IChatRoomManager
    {
        public bool IsUsernameOccupied(string username, string room);
        public bool AddToChatRoom(string username, string connectionId, string room);
        public void RemoveFromChatRoom(ChatUser chatUser);
        public ChatUser GetUserByConnectionID(string connectionId);
    }
}
