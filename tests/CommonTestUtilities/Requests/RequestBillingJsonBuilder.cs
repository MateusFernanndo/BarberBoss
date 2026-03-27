using BarberBoss.Comunication.Enums;
using BarberBoss.Comunication.Request;
using Bogus;

namespace CommomTestUtilities.Requests;

public  class RequestBillingJsonBuilder
{
    public static RequestBillingJson Build()
    {
        return new Faker<RequestBillingJson>()
            .RuleFor(r => r.ClientName, faker => faker.Commerce.ProductName())
            .RuleFor(r => r.BarberName, faker => faker.Commerce.ProductName())
            .RuleFor(r => r.ServiceName, faker => faker.Commerce.ProductDescription())
            .RuleFor(r => r.CreatedAt, faker => faker.Date.Past())
            .RuleFor(r => r.UptadedAt, faker => faker.Date.Past())
            .RuleFor(r => r.PaymentType, faker => faker.PickRandom<PaymentType>())
            .RuleFor(r => r.Amount, faker => faker.Random.Decimal(min: 1, max: 1000))
            .RuleFor(r => r.Date, faker => DateOnly.FromDateTime(faker.Date.Past()));
    }
}
