using BarberBoss.Application.Usecase.Billing.Reports.Pdf.Colors;
using BarberBoss.Application.Usecase.Billing.Reports.Pdf.Fonts;
using BarberBoss.Domain.Extensions;
using BarberBoss.Domain.Reports;
using BarberBoss.Domain.Repositories.Billing;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using PdfSharp.Fonts;
using System.Reflection;

namespace BarberBoss.Application.Usecase.Billing.Reports.Pdf;

public class GenerateBillingsReportPdfUseCase : IGenerateBillingsReportPdfUseCase
{
    private const string CURRENCY_SYMBOL = "€";
    private const int HEIGHT_ROW_EXPENSE_TABLE = 25;

    private readonly IBillingReadOnlyRepository _repository;

    public GenerateBillingsReportPdfUseCase(IBillingReadOnlyRepository repository)
    {
        _repository = repository;

        GlobalFontSettings.FontResolver = new BillingReportFontResolver();
    }

    public async Task<byte[]> Execute(DateOnly week)
    {
        var billings = await _repository.FilterByWeek(week);
        if(billings.Count ==0)
        {
            return [];
        }

        var document = CreateDocument(week);
        var page = CreatePage(document);

        CreateHeaderWithProfilePhotoAndName(page);

        var totalBillingsPaid = billings.Where(billing => billing.Status == Domain.Enums.Status.Pago).Sum(billing => billing.Amount);

        CreateTotalBillingSection(page, week, totalBillingsPaid);

        foreach(var billing in billings)
        {
            var table = CreateBillingTable(page);

            var row = table.AddRow();
            row.Height = HEIGHT_ROW_EXPENSE_TABLE;

            AddBillingServiceName(row.Cells[0], billing.ServiceName);
            AddBillingClientName(row.Cells[2], billing.ClientName);
            AddHeaderForAmount(row.Cells[3]);


            row = table.AddRow();
            row.Height = HEIGHT_ROW_EXPENSE_TABLE;


            row.Cells[0].AddParagraph(billing.Date.ToString("D"));
            SetStyleBaseBillingInformation(row.Cells[0]);
            row.Cells[0].MergeRight= 1;
            row.Cells[0].Format.LeftIndent = 20;

           

            row.Cells[2].AddParagraph(billing.PaymentType.PaymentTypeToString());
            SetStyleBaseBillingInformation(row.Cells[2]);

            AddAmountForBilling(row.Cells[3], billing.Amount);

            if (string.IsNullOrWhiteSpace(billing.Notes) == false)
            {
                var descriptionRow = table.AddRow();
                descriptionRow.Height = HEIGHT_ROW_EXPENSE_TABLE;

                descriptionRow.Cells[0].AddParagraph(billing.Notes);
                descriptionRow.Cells[0].Format.Font = new Font { Name = FontHelper.WORKSANS_REGULAR, Size = 10, Color = ColorHelper.BLACK };
                descriptionRow.Cells[0].Shading.Color = ColorHelper.BROWN;
                descriptionRow.Cells[0].VerticalAlignment = VerticalAlignment.Center;
                descriptionRow.Cells[0].MergeRight = 2;
                descriptionRow.Cells[0].Format.LeftIndent = 20;
                row.Cells[3].MergeDown = 1;
            }

            AddWhiteSpace(table);

        }

        return RenderDocument(document);

    }


    private Document CreateDocument(DateOnly week)
    {
        var document = new Document();
        document.Info.Title = $"{ResourceReportGenerationMessages.BILLING_FOR} {week:Y}";
        document.Info.Author = "Mateus Fernando";

        var style = document.Styles["Normal"];
        style.Font.Name = FontHelper.RALEWAY_REGULAR;

        return document;
    }

    private Section CreatePage(Document document)
    {
        var section = document.AddSection();
        section.PageSetup = document.DefaultPageSetup.Clone();
        section.PageSetup.PageFormat = PageFormat.A4;
        section.PageSetup.LeftMargin = 40;
        section.PageSetup.RightMargin = 40;
        section.PageSetup.TopMargin = 80;
        section.PageSetup.BottomMargin = 80;

        return section;
    }

    private void CreateHeaderWithProfilePhotoAndName(Section page)
    {
        var table = page.AddTable();
        table.AddColumn();
        table.AddColumn("300");

        var row = table.AddRow();

        var assembly = Assembly.GetExecutingAssembly();
        var directoryName = Path.GetDirectoryName(assembly.Location);
        var pathFile = Path.Combine(directoryName!, "Logo", "logoProf.png");

        if (File.Exists(pathFile))
        {
            row.Cells[0].AddImage(pathFile);
        }

        row.Cells[1].AddParagraph("Hey, Tony Stark");
        row.Cells[1].Format.Font = new Font { Name = FontHelper.RALEWAY_BLACK, Size = 16 };
        row.Cells[1].VerticalAlignment = MigraDoc.DocumentObjectModel.Tables.VerticalAlignment.Center;
    }

    private void CreateTotalBillingSection(Section page, DateOnly week, decimal totalBillings)
    {
        var paragraph = page.AddParagraph();
        paragraph.Format.SpaceBefore = "40";
        paragraph.Format.SpaceAfter = "40";

        var title = string.Format(ResourceReportGenerationMessages.TOTAL_BILLING_IN, week.ToString("Y"));
        paragraph.AddFormattedText(title, new Font { Name = FontHelper.RALEWAY_REGULAR, Size = 15 });
        paragraph.AddLineBreak();

        paragraph.AddFormattedText($"{totalBillings} {CURRENCY_SYMBOL}", new Font { Name = FontHelper.WORKSANS_BLACK, Size = 50 });
    }

    private Table CreateBillingTable(Section page)
    {
        var table = page.AddTable();

        table.AddColumn("195").Format.Alignment = ParagraphAlignment.Left;
        table.AddColumn("80").Format.Alignment = ParagraphAlignment.Center;
        table.AddColumn("120").Format.Alignment = ParagraphAlignment.Center;
        table.AddColumn("120").Format.Alignment = ParagraphAlignment.Right;


        return table;
    }

    private void AddBillingServiceName(Cell cell, string billingServiceName)
    {
        cell.AddParagraph(billingServiceName);
        cell.Format.Font = new Font { Name = FontHelper.RALEWAY_BLACK, Size = 14, Color = ColorHelper.BLACK };
        cell.Shading.Color = ColorHelper.GREEN;
        cell.VerticalAlignment = VerticalAlignment.Center;
        cell.MergeRight = 1;
        cell.Format.LeftIndent = 20;
    }
    private void AddBillingClientName(Cell cell, string billingClientName)
    {
        cell.AddParagraph(billingClientName);
        cell.Format.Font = new Font { Name = FontHelper.RALEWAY_BLACK, Size = 14, Color = ColorHelper.BLACK };
        cell.Shading.Color = ColorHelper.GREEN;
        cell.VerticalAlignment = VerticalAlignment.Center;
        cell.Format.LeftIndent = 20;
    }

    private void AddHeaderForAmount(Cell cell)
    {
        cell.AddParagraph(ResourceReportGenerationMessages.AMOUNT);
        cell.Format.Font = new Font { Name = FontHelper.RALEWAY_BLACK, Size = 14, Color = ColorHelper.BLACK };
        cell.Shading.Color = ColorHelper.GREEN_LIGHT;
        cell.VerticalAlignment = VerticalAlignment.Center;
    }

    private void SetStyleBaseBillingInformation(Cell cell)
    {
        cell.Format.Font = new Font { Name = FontHelper.WORKSANS_REGULAR, Size = 12, Color = ColorHelper.BLACK };
        cell.Shading.Color = ColorHelper.BROWN_LIGHT;
        cell.VerticalAlignment = VerticalAlignment.Center;
    }

    private void AddAmountForBilling(Cell cell, decimal amount)
    {
        cell.AddParagraph($"+{amount} {CURRENCY_SYMBOL}");
        cell.Format.Font = new Font { Name = FontHelper.WORKSANS_REGULAR, Size = 14, Color = ColorHelper.BLACK };
        cell.Shading.Color = ColorHelper.WHITE;
        cell.VerticalAlignment = VerticalAlignment.Center;
    }

    private void AddWhiteSpace(Table table)
    {
        var row = table.AddRow();
        row.Height = 30;
        row.Borders.Visible = false;
    }

    private byte[] RenderDocument(Document document)
    {
        var renderer = new PdfDocumentRenderer
        {
            Document = document,
        };

        renderer.RenderDocument();
        using var file = new MemoryStream();
        renderer.PdfDocument.Save(file);
        return file.ToArray();
    }
}
