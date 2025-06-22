using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Tuapp.Application.DTOs;

namespace Tuapp.Application.UseCases.Tickets.Queries
{
    public record GetTicketsByUserQuery(Guid UserId) : IRequest<List<TicketDto>>;
}
