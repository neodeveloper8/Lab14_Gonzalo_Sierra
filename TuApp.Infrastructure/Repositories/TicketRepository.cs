using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TuApp.Domain.Entities;
using TuApp.Domain.Interfaces;
using TuApp.Infrastructure.Context;

namespace TuApp.Infrastructure.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly TicketeraDbContext _context;

        public TicketRepository(TicketeraDbContext context)
        {
            _context = context;
        }

        public async Task<List<Ticket>> GetTicketsByUserIdAsync(Guid userId)
        {
            return await _context.Tickets
                .Where(t => t.UserId == userId)
                .ToListAsync();
        }

        public async Task<List<Ticket>> GetClosedTicketsAsync(DateTime? fechaInicio, DateTime? fechaFin)
        {
            var query = _context.Tickets
                .Where(t => t.Status == "cerrado" && t.ClosedAt != null)
                .AsQueryable();

            if (fechaInicio.HasValue)
                query = query.Where(t => t.ClosedAt >= fechaInicio.Value);

            if (fechaFin.HasValue)
                query = query.Where(t => t.ClosedAt <= fechaFin.Value);

            return await query.ToListAsync();
        }


        public async Task AddAsync(Ticket ticket)
        {
            await _context.Tickets.AddAsync(ticket);
        }

        public async Task<List<Ticket>> GetAllAsync()
        {
            return await _context.Tickets.ToListAsync();
        }
    }
}
