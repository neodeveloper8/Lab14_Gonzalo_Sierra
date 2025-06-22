using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TuApp.Domain.Entities;
using TuApp.Domain.Interfaces;

namespace Tuapp.Application.UseCases.Tickets.Comands
{
    internal sealed class CreateTicketCommandHandler : IRequestHandler<CreateTicketCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateTicketCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateTicketCommand request, CancellationToken cancellationToken)
        {
            var ticket = new Ticket
            {
                TicketId = Guid.NewGuid(),
                UserId = request.UserId,
                Title = request.Title,
                Description = request.Description,
                Status = "abierto",
                CreatedAt = DateTime.Now
            };

            await _unitOfWork.Tickets.AddAsync(ticket);
            await _unitOfWork.SaveChangesAsync();

            return ticket.TicketId;
        }
    }
}
