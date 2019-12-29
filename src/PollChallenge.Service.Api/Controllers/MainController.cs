using Microsoft.AspNetCore.Mvc;
using PollChallenge.Domain.Interfaces.Services;
using PollChallenge.Domain.ValueObjects;
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
                return BadRequest(
                    new ResultBadRequest(_notificationService.GetNotifications().Select(n => n.Message)));

            if (resultNotNull && result == null)
                return NotFound(null);

            return Ok(new ResultOk(result));
        }
    }
}