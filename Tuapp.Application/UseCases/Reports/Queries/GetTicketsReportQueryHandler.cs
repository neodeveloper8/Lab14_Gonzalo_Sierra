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
    internal sealed class GetTicketsReportQueryHandler : IRequestHandler<GetTicketsReportQuery, byte[]>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetTicketsReportQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<byte[]> Handle(GetTicketsReportQuery request, CancellationToken cancellationToken)
        {
            var tickets = await _unitOfWork.Tickets.GetAllAsync();

            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Tickets");

            // Encabezados
            worksheet.Cell(1, 1).Value = "ID";
            worksheet.Cell(1, 2).Value = "Usuario ID";
            worksheet.Cell(1, 3).Value = "Título";
            worksheet.Cell(1, 4).Value = "Estado";
            worksheet.Cell(1, 5).Value = "Fecha de Creación";

            var headerRange = worksheet.Range(1, 1, 1, 5);
            headerRange.Style.Font.Bold = true;
            headerRange.Style.Fill.BackgroundColor = XLColor.LightGray;
            headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            int row = 2;
            foreach (var t in tickets)
            {
                worksheet.Cell(row, 1).Value = t.TicketId.ToString();
                worksheet.Cell(row, 2).Value = t.UserId.ToString();
                worksheet.Cell(row, 3).Value = t.Title;
                worksheet.Cell(row, 4).Value = t.Status;
                worksheet.Cell(row, 5).Value = t.CreatedAt?.ToString("dd/MM/yyyy");

                row++;
            }

            // Aplicar bordes y ajuste de columnas
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
