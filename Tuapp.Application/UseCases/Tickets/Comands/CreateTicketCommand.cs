using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Tuapp.Application.UseCases.Tickets.Comands
{
    public record CreateTicketCommand(Guid UserId, string Title, string? Description) : IRequest<Guid>;
}
