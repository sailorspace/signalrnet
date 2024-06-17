using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using signalrnet.hubs;

namespace signalrnet.Controllers
{

    public class Announcement : Controller
    {

        private IHubContext<MessageHub> _hubContext;
        public Announcement(IHubContext<MessageHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpGet("/Announcement")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("/Announcement")]
        public async Task<IActionResult> Post([FromForm] string message)
        {
            await _hubContext.Clients.All.SendAsync("RecieveMessage", message);
            return RedirectToAction("Index");
        }
    }
}