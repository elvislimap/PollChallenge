using Microsoft.AspNetCore.Mvc;
using PollChallenge.Domain.Interfaces.Services;
using System.Linq;

namespace PollChallenge.Service.Api.Controllers
{
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        protected MainController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }


        protected ActionResult CustomResponse(object result = null, bool resultNotNull = true)
        {
            if (_notificationService.HaveNotification())
                return BadRequest(_notificationService.GetNotifications().Select(n => n.Message));

            if (resultNotNull && result == null)
                return NotFound(null);

            return Ok(result);
        }
    }
}