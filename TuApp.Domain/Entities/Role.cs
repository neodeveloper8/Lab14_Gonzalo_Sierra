using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuApp.Domain.Entities
{
    public partial class Role
    {
        public Guid RoleId { get; set; }

        public string RoleName { get; set; } = null!;

        public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}
