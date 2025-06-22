using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tuapp.Application.DTOs;
using Tuapp.Application.Interfaces;
using TuApp.Domain.Interfaces;

namespace Tuapp.Application.Servicios
{
    public class TicketService : ITicketService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TicketService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<TicketDto>> ObtenerTicketsPorUsuario(Guid userId)
        {
            var tickets = await _unitOfWork.Tickets.GetTicketsByUserIdAsync(userId);

            return tickets.Select(t => new TicketDto
            {
                TicketId = t.TicketId,
                Title = t.Title,
                Status = t.Status,
                CreatedAt = t.CreatedAt
            }).ToList();
        }
    }
}
