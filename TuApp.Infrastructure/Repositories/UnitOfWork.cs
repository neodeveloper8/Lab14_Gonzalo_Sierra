using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuApp.Domain.Entities;
using TuApp.Domain.Interfaces;
using TuApp.Infrastructure.Context;


namespace TuApp.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TicketeraDbContext _context;

        public UnitOfWork(TicketeraDbContext context)
        {
            _context = context;
            Users = new UserRepository(_context);
            Tickets = new TicketRepository(_context);
        }

        public IUserRepository Users { get; }
        public ITicketRepository Tickets { get; }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
