using Backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenAI_API;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatbotController : ControllerBase
    {
        private readonly string API_KEY = "YOUR_API_KEY";
        private OpenAIAPI api;
        private OpenAI_API.Chat.Conversation conversation;
        public ChatbotController()
        {
            api = new OpenAIAPI(API_KEY);
            conversation = api.Chat.CreateConversation();
        }
        [HttpPost("ask")]
        public async Task<IActionResult> AskQuestion([FromBody] Message req)
        {
            if (req == null || string.IsNullOrWhiteSpace(req.Value))
            {
                return BadRequest("Invalid input.");
            }

            conversation.AppendUserInput(req.Value);
            Console.WriteLine(req.Value);
            var answer = await conversation.GetResponseFromChatbotAsync();
            Console.WriteLine(answer);
            Message response = new Message();
            response.Sender = "bot";
            response.Value = answer;
            return Ok(response);
        }
    }
}