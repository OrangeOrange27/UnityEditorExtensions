using Microsoft.AspNetCore.Mvc;
using UnityCommunication.Data;
using UnityCommunication.Models;

namespace UnityCommunication.Controllers
{
    [ApiController]
    [Route("message")]
    public class MessageController : Controller
    {
        private AppDbContext _dbContext;
        public MessageController(AppDbContext db)
        {
            _dbContext = db;
        }
        [HttpGet]
        public string GetMessage([FromQuery]string token)
        {
            var msg = _dbContext.messageModels.OrderBy(m => m.Id).LastOrDefault(m => m.Token==token);
            if (msg == null)
            {
                return "No message";
            }
            return msg.Msg;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Message([FromForm] MessageModel messageModel, [FromQuery] string token)
        {
            messageModel.Token = token;
            _dbContext.Add(messageModel);
            _dbContext.SaveChanges();

            return Redirect("~/Home/Privacy");
        }
    }
}
