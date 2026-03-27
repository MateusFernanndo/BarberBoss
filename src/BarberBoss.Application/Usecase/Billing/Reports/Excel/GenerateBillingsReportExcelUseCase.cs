using BarberBoss.Domain.Extensions;
using BarberBoss.Domain.Reports;
using BarberBoss.Domain.Repositories.Billing;
using ClosedXML.Excel;
using ClosedXML.Excel.Drawings;
using PdfSharp.Drawing;

namespace BarberBoss.Application.Usecase.Billing.Reports.Excel;

public class GenerateBillingsReportExcelUseCase : IGenerateBillingsReportExcelUseCase
{
    private const string CURRENCY_SYMBOL = "€";
    private readonly IBillingReadOnlyRepository _repository;
    
    public GenerateBillingsReportExcelUseCase(IBillingReadOnlyRepository repository)
    {
        _repository = repository;
    }

    public async Task<byte[]> Execute(DateOnly week)
    {
        var billings = await _repository.FilterByWeek(week);
        if (billings.Count == 0)
        {
            return [];
        }

        using var workbook = new XLWorkbook();

        workbook.Author = "Mateus Fernando";
        workbook.Style.Font.FontSize = 12;
        workbook.Style.Font.FontName = "Times New Roman";

        var worksheet = workbook.Worksheets.Add(week.ToString("Y"));

        InsertHeader(worksheet);
        var raw = 2;

        foreach (var billing in billings)
        {
            worksheet.Cell($"A{raw}").Value = billing.ClientName;
            worksheet.Cell($"B{raw}").Value = billing.BarberName;
            worksheet.Cell($"C{raw}").Value = billing.ServiceName;
            worksheet.Cell($"D{raw}").Value = billing.UptadedAt;
            worksheet.Cell($"E{raw}").Value = billing.PaymentType.PaymentTypeToString();
            worksheet.Cell($"F{raw}").Value = billing.Amount;
            worksheet.Cell($"F{raw}").Style.NumberFormat.Format = $"{CURRENCY_SYMBOL} #, ##0.00";
            worksheet.Cell($"G{raw}").Value = billing.Notes;
            raw++;
        }
        worksheet.Columns().AdjustToContents();

        using var file = new MemoryStream();
        workbook.SaveAs(file);

        return file.ToArray();
    }

        private void InsertHeader(IXLWorksheet worksheet)
        {
            worksheet.Cell("A1").Value = ResourceReportGenerationMessages.CLIENT_NAME;
            worksheet.Cell("B1").Value = ResourceReportGenerationMessages.BARBER_NAME;
            worksheet.Cell("C1").Value = ResourceReportGenerationMessages.SERVICE_NAME;
            worksheet.Cell("D1").Value = ResourceReportGenerationMessages.DATE;
            worksheet.Cell("E1").Value = ResourceReportGenerationMessages.PAYMENT_TYPE;
            worksheet.Cell("F1").Value = ResourceReportGenerationMessages.AMOUNT;
            worksheet.Cell("G1").Value = ResourceReportGenerationMessages.NOTES;
            
            worksheet.Cells("A1:G1").Style.Font.Bold = true;
            worksheet.Cells("A1:G1").Style.Fill.BackgroundColor = XLColor.FromHtml("#0b7d8c");
            worksheet.Cell("A1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            worksheet.Cell("B1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            worksheet.Cell("C1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            worksheet.Cell("E1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            worksheet.Cell("F1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            worksheet.Cell("G1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            worksheet.Cell("D1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);
        }
}

