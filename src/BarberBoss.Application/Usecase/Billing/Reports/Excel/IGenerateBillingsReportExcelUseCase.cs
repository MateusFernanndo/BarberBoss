namespace BarberBoss.Application.Usecase.Billing.Reports.Excel;

public interface IGenerateBillingsReportExcelUseCase
{
    public Task<byte[]> Execute(DateOnly week);
}
