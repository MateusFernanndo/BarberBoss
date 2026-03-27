using BarberBoss.Application.AutoMapper;
using BarberBoss.Application.Usecase.Billing.Delete;
using BarberBoss.Application.Usecase.Billing.GetAll;
using BarberBoss.Application.Usecase.Billing.GetById;
using BarberBoss.Application.Usecase.Billing.Register;
using BarberBoss.Application.Usecase.Billing.Reports.Excel;
using BarberBoss.Application.Usecase.Billing.Reports.Pdf;
using BarberBoss.Application.Usecase.Billing.Update;
using Microsoft.Extensions.DependencyInjection;

namespace BarberBoss.Application;

public static class DependecyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        AddAutoMapper(services);
        AddUseCases(services);
    }

    private static void AddAutoMapper(IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AutoMapping));
    }

    private static void AddUseCases(IServiceCollection services)
    {
        services.AddScoped<IRegisterBillingUseCase, RegisterBillingUseCase>();
        services.AddScoped<IGetAllBillingUseCase, GetAllBillingUseCase>();
        services.AddScoped<IGetBillingByIdUseCase, GetBillingByIdUseCase>();
        services.AddScoped<IDeleteBillingUseCase, DeleteBillingUseCase>();
        services.AddScoped<IUpdateBillingUseCase, UpdateBillingUseCase>();
        services.AddScoped<IGenerateBillingsReportExcelUseCase, GenerateBillingsReportExcelUseCase>();
        services.AddScoped<IGenerateBillingsReportPdfUseCase, GenerateBillingsReportPdfUseCase>();
    }
}

