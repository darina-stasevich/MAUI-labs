using MauiApp6.Entities;

namespace MauiApp6.Services;

public interface IRateService
{
    Task<IEnumerable<Rate>> GetRates(DateTime date);
}