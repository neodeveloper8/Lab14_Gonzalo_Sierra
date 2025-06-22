using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tuapp.Application.UseCases.Reports.Queries;

namespace TuApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ReportesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReportesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Exporta todos los tickets a un archivo Excel.
        /// </summary>
        [HttpGet("tickets")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DescargarReporteTickets()
        {
            var bytes = await _mediator.Send(new GetTicketsReportQuery());
            return File(bytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "reporte_tickets.xlsx");
        }

        [HttpGet("tickets-cerrados")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DescargarReporteTicketsCerrados([FromQuery] DateTime? fechaInicio, [FromQuery] DateTime? fechaFin)
        {
            var bytes = await _mediator.Send(new GetClosedTicketsReportQuery(fechaInicio, fechaFin));
            return File(bytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "tickets_cerrados.xlsx");
        }




    }
}
