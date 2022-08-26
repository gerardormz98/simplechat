using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleChat.Model
{
    public class ChatUser
    {
        public string Username { get; set; }
        public string ConnectionID { get; set; }
        public string AssignedRoom { get; set; }

        public ChatUser(string username, string connectionID)
        {
            Username = username;
            ConnectionID = connectionID;
        }
    }
}
