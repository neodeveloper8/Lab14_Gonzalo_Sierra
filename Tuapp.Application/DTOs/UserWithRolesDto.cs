using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tuapp.Application.DTOs
{
    public class UserWithRolesDto
    {
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string? Email { get; set; }
        public List<RoleDto> Roles { get; set; } = new();
    }

    public class RoleDto
    {
        public string RoleName { get; set; } = null!;
    }
}
