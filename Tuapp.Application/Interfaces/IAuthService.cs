using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tuapp.Application.DTOs;

namespace Tuapp.Application.Interfaces
{
    public interface IAuthService
    {
        Task<string> LoginAsync(LoginRequestDto dto);
        Task RegisterAsync(RegisterUserDto dto);
    }
}
