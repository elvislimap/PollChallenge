using Microsoft.AspNetCore.Mvc;
using PollChallenge.Domain.Interfaces.Services;
using System.Linq;

namespace PollChallenge.Service.Api.Controllers
{
    [ApiController]
    public abstract class MainController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        protected MainController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }


        protected ActionResult CustomResponse(object result = null)
        {
            if (_notificationService.HaveNotification())
                return BadRequest(_notificationService.GetNotifications().Select(n => n.Message));

            if (result == null)
                return NotFound(null);

            return Ok(result);
        }
    }
}