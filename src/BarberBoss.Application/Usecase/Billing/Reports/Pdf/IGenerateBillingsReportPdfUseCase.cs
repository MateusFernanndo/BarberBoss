namespace BarberBoss.Application.Usecase.Billing.Reports.Pdf;

public interface IGenerateBillingsReportPdfUseCase
{
    public Task<byte[]> Execute(DateOnly week);
}
