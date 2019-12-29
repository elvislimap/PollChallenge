using Microsoft.AspNetCore.Mvc;
using PollChallenge.Application.Interfaces;
using PollChallenge.Domain.Entities;
using PollChallenge.Domain.Interfaces.Services;
using PollChallenge.Domain.ValueObjects;
using System.Threading.Tasks;

namespace PollChallenge.Service.Api.Controllers.V1
{
    [Route("poll")]
    public class PollController : MainController
    {
        private readonly IPollAppService _pollAppService;
        private readonly IVoteAppService _voteAppService;

        public PollController(INotificationService notificationService,
            IPollAppService pollAppService, IVoteAppService voteAppService) : base(notificationService)
        {
            _pollAppService = pollAppService;
            _voteAppService = voteAppService;
        }


        [HttpGet("{pollId}")]
        public async Task<ActionResult<Poll>> GetById(int pollId)
        {
            return CustomResponse(await _pollAppService.GetById(pollId));
        }

        [HttpGet("{pollId}/stats")]
        public async Task<ActionResult<Poll>> GetStatsById(int pollId)
        {
            return CustomResponse(await _pollAppService.GetStatsById(pollId));
        }

        [HttpPost]
        public async Task<ActionResult<Poll>> Insert([FromBody] RequestInsertPoll requestInsertPoll)
        {
            return CustomResponse(await _pollAppService.Insert(requestInsertPoll));
        }

        [HttpPost("{pollId}/vote")]
        public async Task<ActionResult<Poll>> Vote(int pollId, [FromBody] Vote vote)
        {
            return CustomResponse(await _voteAppService.Insert(pollId, vote.OptionId));
        }
    }
}