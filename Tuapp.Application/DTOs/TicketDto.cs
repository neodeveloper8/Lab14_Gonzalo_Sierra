using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tuapp.Application.DTOs
{
    public class TicketDto
    {
        public Guid TicketId { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
