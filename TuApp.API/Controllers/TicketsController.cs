using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Tuapp.Application.Interfaces;
using TuApp.Domain.Entities;

namespace TuApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // 👈 Esto protege todo el controlador
    public class TicketsController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        public TicketsController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpGet("mis-tickets")]
        public async Task<IActionResult> GetMisTickets()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized();

            Guid userId = Guid.Parse(userIdClaim.Value);
            var tickets = await _ticketService.ObtenerTicketsPorUsuario(userId);
            return Ok(tickets);
        }
    }
}
