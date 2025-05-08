using System.Net.Http.Json;
using MauiApp6.Entities;

namespace MauiApp6.Services;

public class RateService(HttpClient httpClient) : IRateService
{
    public async Task<IEnumerable<Rate>> GetRates(DateTime date)
    {
        HashSet<string> requiredRates = new HashSet<string> {"USD", "RUB", "EUR", "CNY", "CNF", "GBP"};
        var response = await httpClient.GetAsync($"?ondate={date:yyyy-MM-dd}&periodicity=0");
        if (response.IsSuccessStatusCode)
        {
            var allRates = await response.Content.ReadFromJsonAsync<IEnumerable<Rate>>();
            if (allRates != null)
            {
                 var rates = allRates.Where(rate => requiredRates.Contains(rate.Cur_Abbreviation)).Append(new Rate
                 {
                   Cur_Abbreviation = "BYN",
                   Cur_Name = "Белорусский рубль",
                   Date = date,
                   Cur_Scale = 1,
                   Cur_OfficialRate = 1
                 });
                 
                 return rates;
            }
            throw new Exception("Rates not found");
        }
        else
        {
          throw new HttpRequestException($"Error: {response.ReasonPhrase}");
        }
    }
}

/*
  {
    "Cur_ID": 431,
    "Date": "2025-04-19T00:00:00",
    "Cur_Abbreviation": "USD",
    "Cur_Scale": 1,
    "Cur_Name": "Доллар США",
    "Cur_OfficialRate": 3.0814
  },
    {
    "Cur_ID": 456,
    "Date": "2025-04-19T00:00:00",
    "Cur_Abbreviation": "RUB",
    "Cur_Scale": 100,
    "Cur_Name": "Российских рублей",
    "Cur_OfficialRate": 3.6695
  },
    {
    "Cur_ID": 451,
    "Date": "2025-04-19T00:00:00",
    "Cur_Abbreviation": "EUR",
    "Cur_Scale": 1,
    "Cur_Name": "Евро",
    "Cur_OfficialRate": 3.505
  },
    {
    "Cur_ID": 462,
    "Date": "2025-04-19T00:00:00",
    "Cur_Abbreviation": "CNY",
    "Cur_Scale": 10,
    "Cur_Name": "Китайских юаней",
    "Cur_OfficialRate": 4.1659
  },
    {
    "Cur_ID": 426,
    "Date": "2025-04-19T00:00:00",
    "Cur_Abbreviation": "CHF",
    "Cur_Scale": 1,
    "Cur_Name": "Швейцарский франк",
    "Cur_OfficialRate": 3.7714
  }
    {
    "Cur_ID": 429,
    "Date": "2025-04-19T00:00:00",
    "Cur_Abbreviation": "GBP",
    "Cur_Scale": 1,
    "Cur_Name": "Фунт стерлингов",
    "Cur_OfficialRate": 4.0808
  },
  */