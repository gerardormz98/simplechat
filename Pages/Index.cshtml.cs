using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using SimpleChat.Data;
using SimpleChat.Hubs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleChat.Pages
{
    [BindProperties]
    public class IndexModel : PageModel
    {
        private readonly IChatRoomManager _chatRoomManager;

        [MaxLength(25)]
        [Required(ErrorMessage = "Please enter your username.")]
        public string Username { get; set; }
        [Required]
        public string ChatRoom { get; set; }

        public IndexModel(IChatRoomManager chatRoomManager)
        {
            _chatRoomManager = chatRoomManager;
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (_chatRoomManager.IsUsernameOccupied(Username, ChatRoom))
            {
                ModelState.AddModelError(nameof(Username), "This username is already in use. Pick a different one.");
                return Page();
            }

            TempData["SelectedUsername"] = Username;
            TempData["SelectedChatRoom"] = ChatRoom;

            return RedirectToPage("chat");
        }
    }
}
