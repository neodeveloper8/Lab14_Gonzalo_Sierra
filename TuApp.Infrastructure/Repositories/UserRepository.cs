using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tuapp.Application.DTOs;
using TuApp.Domain.Entities;
using TuApp.Domain.Interfaces;
using TuApp.Infrastructure.Context;



namespace TuApp.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TicketeraDbContext _context;

        public UserRepository(TicketeraDbContext context)
        {
            _context = context;
        }

        

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }
    }
}
