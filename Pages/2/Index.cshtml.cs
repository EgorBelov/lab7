using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using ServiceReference1;
using ServiceReference2;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace lab7.Pages._2
{
    public class IndexModel : PageModel
    {
        private const string ApiUrl = "https://www.cbr-xml-daily.ru/daily_json.js";

        public List<Currency> AvailableCurrencies { get; set; }

        [BindProperty]
        public decimal Amount { get; set; }

        [BindProperty]
        public string SourceCurrency { get; set; }

        [BindProperty]
        public string TargetCurrency { get; set; }

        public decimal? ConvertedAmount { get; set; }

        public async Task OnGet()
        {
            AvailableCurrencies = await GetAvailableCurrencies();
        }

        public async Task<IActionResult> OnPost()
        {
            AvailableCurrencies = await GetAvailableCurrencies();

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var exchangeRates = await GetExchangeRates();

            if (!exchangeRates.ContainsKey(SourceCurrency) || !exchangeRates.ContainsKey(TargetCurrency))
            {
                ModelState.AddModelError(string.Empty, "Invalid currency selection.");
                return Page();
            }

            var sourceRate = exchangeRates[SourceCurrency].Value;
            var targetRate = exchangeRates[TargetCurrency].Value;

            ConvertedAmount = Amount / sourceRate * targetRate;

            return Page();
        }
        

        private async Task<List<Currency>> GetAvailableCurrencies()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(ApiUrl);
                var content = await response.Content.ReadAsStringAsync();
                var currencyData = JsonConvert.DeserializeObject<CurrencyData>(content);
                return currencyData.Valute.Values.Select(currencyRate => new Currency
                {
                    Code = currencyRate.CharCode,
                    Name = currencyRate.Name
                }).ToList();
            }
        }

        private async Task<Dictionary<string, CurrencyRate>> GetExchangeRates()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(ApiUrl);
                var content = await response.Content.ReadAsStringAsync();
                var currencyData = JsonConvert.DeserializeObject<CurrencyData>(content);
                return currencyData.Valute;
            }
        }
    }

    public class CurrencyData
    {
        public Dictionary<string, CurrencyRate> Valute { get; set; }
    }

    public class CurrencyRate
    {
        public string CharCode { get; set; }
        public int Nominal { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
    }

    public class Currency
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
