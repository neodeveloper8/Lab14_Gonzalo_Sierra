using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;
using MediatR;
using TuApp.Domain.Interfaces;

namespace Tuapp.Application.UseCases.Reports.Queries
{
    internal sealed class GetClosedTicketsReportQueryHandler : IRequestHandler<GetClosedTicketsReportQuery, byte[]>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetClosedTicketsReportQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<byte[]> Handle(GetClosedTicketsReportQuery request, CancellationToken cancellationToken)
        {
            var tickets = await _unitOfWork.Tickets.GetClosedTicketsAsync(request.FechaInicio, request.FechaFin);

            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("TicketsCerrados");

            // Encabezados
            worksheet.Cell(1, 1).Value = "ID";
            worksheet.Cell(1, 2).Value = "Título";
            worksheet.Cell(1, 3).Value = "Estado";
            worksheet.Cell(1, 4).Value = "Creado el";
            worksheet.Cell(1, 5).Value = "Cerrado el";

            var headerRange = worksheet.Range(1, 1, 1, 5);
            headerRange.Style.Font.Bold = true;
            headerRange.Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
            headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            int row = 2;
            foreach (var t in tickets)
            {
                worksheet.Cell(row, 1).Value = t.TicketId.ToString();
                worksheet.Cell(row, 2).Value = t.Title;
                worksheet.Cell(row, 3).Value = t.Status;
                worksheet.Cell(row, 4).Value = t.CreatedAt?.ToString("dd/MM/yyyy");
                worksheet.Cell(row, 5).Value = t.ClosedAt?.ToString("dd/MM/yyyy");
                row++;
            }

            // Estilo y ajuste
            var dataRange = worksheet.RangeUsed();
            dataRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            dataRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
            worksheet.Columns().AdjustToContents();

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            return stream.ToArray();
        }
    }
}
