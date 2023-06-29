using Microsoft.AspNetCore.Mvc;
using ChatApi.DTOS;
using ChatApi.Services;
using Microsoft.Extensions.Configuration;

namespace ChatApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly ChatService _chatService;

        public ChatController(ChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpGet("get-online-users")]
        public async Task<string[]> RegisterUser()
        {
            return _chatService.GetOnlineUsers();
        }

        [HttpPost("register-user")]
        public async Task<IActionResult> RegisterUser(UserDto user)
        {
            if (_chatService.AddUserToList(user.Username))
            {
                return NoContent();
            }
            return BadRequest("This name is taken");
        }
    }
}
