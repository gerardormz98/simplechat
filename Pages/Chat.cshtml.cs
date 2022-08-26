using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SimpleChat.Pages
{
    public class ChatModel : PageModel
    {
        public string Username { get; set; }
        public string ChatRoom { get; set; }

        public IActionResult OnGet()
        {
            var selectedUserName = TempData["SelectedUsername"];
            var selectedChatRoom = TempData["SelectedChatRoom"];

            if (selectedUserName == null || selectedChatRoom == null)
            {
                return RedirectToPage("/Index");
            }

            Username = selectedUserName.ToString();
            ChatRoom = selectedChatRoom.ToString();

            return Page();
        }
    }
}
