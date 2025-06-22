using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TuApp.Domain.Entities
{
    public partial class Response
    {
        public Guid ResponseId { get; set; }

        public Guid TicketId { get; set; }

        public Guid ResponderId { get; set; }

        public string Message { get; set; } = null!;

        public DateTime? CreatedAt { get; set; }

        public virtual User Responder { get; set; } = null!;

        public virtual Ticket Ticket { get; set; } = null!;
    }
}
