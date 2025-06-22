using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Tuapp.Application.DTOs;
using TuApp.Domain.Interfaces;

namespace Tuapp.Application.UseCases.Tickets.Queries
{
    internal sealed class GetTicketsByUserQueryHandler : IRequestHandler<GetTicketsByUserQuery, List<TicketDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetTicketsByUserQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<TicketDto>> Handle(GetTicketsByUserQuery request, CancellationToken cancellationToken)
        {
            var tickets = await _unitOfWork.Tickets.GetTicketsByUserIdAsync(request.UserId);

            return tickets.Select(t => new TicketDto
            {
                TicketId = t.TicketId,
                Title = t.Title,
                Status = t.Status,
                CreatedAt = t.CreatedAt ?? DateTime.MinValue
            }).ToList();
        }
    }
}
