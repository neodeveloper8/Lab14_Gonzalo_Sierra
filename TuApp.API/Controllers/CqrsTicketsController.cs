using MediatR;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Tuapp.Application.UseCases.Tickets.Comands;
using Tuapp.Application.UseCases.Tickets.Queries;

namespace TuApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CqrsTicketsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CqrsTicketsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CrearTicket([FromBody] CreateTicketCommand command)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null) return Unauthorized();

            var userId = Guid.Parse(userIdClaim.Value);
            var ticketId = await _mediator.Send(command with { UserId = userId });
            return Ok(new { TicketId = ticketId });
        }

        [HttpGet("mis-tickets")]
        public async Task<IActionResult> ObtenerMisTickets()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null) return Unauthorized();

            var userId = Guid.Parse(userIdClaim.Value);
            var tickets = await _mediator.Send(new GetTicketsByUserQuery(userId));
            return Ok(tickets);
        }
    }
}
