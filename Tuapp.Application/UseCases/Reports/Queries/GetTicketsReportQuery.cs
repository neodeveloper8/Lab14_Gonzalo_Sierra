using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Tuapp.Application.UseCases.Reports.Queries
{
    public record GetTicketsReportQuery : IRequest<byte[]>; // Devuelve archivo en bytes
}
