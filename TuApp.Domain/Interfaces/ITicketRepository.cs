using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuApp.Domain.Entities;

namespace TuApp.Domain.Interfaces
{
    public interface ITicketRepository
    {
        Task<List<Ticket>> GetTicketsByUserIdAsync(Guid userId);
        Task AddAsync(Ticket ticket);

        Task<List<Ticket>> GetAllAsync();

        Task<List<Ticket>> GetClosedTicketsAsync(DateTime? fechaInicio, DateTime? fechaFin);
    }
}
