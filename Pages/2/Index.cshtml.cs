using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using ServiceReference1;
using ServiceReference2;

namespace lab7.Pages._2
{
    public class IndexModel : PageModel
    {
        //private const string ApiUrl = "https://www.cbr-xml-daily.ru/daily_json.js";

        //[BindProperty]
        //public decimal Amount { get; set; }

        //[BindProperty]
        //public string SourceCurrency { get; set; }

        //[BindProperty]
        //public string TargetCurrency { get; set; }

        //public decimal? ConvertedAmount { get; set; }

        //public List<Currency> AvailableCurrencies { get; set; }

        //public async Task OnGet()
        //{
        //    AvailableCurrencies = await GetAvailableCurrencies();
        //}

        //public async Task<IActionResult> OnPost()
        //{
        //    AvailableCurrencies = await GetAvailableCurrencies();

        //    if (!ModelState.IsValid)
        //    {
        //        return Page();
        //    }

        //    var exchangeRates = await GetExchangeRates();

        //    if (!exchangeRates.ContainsKey(SourceCurrency) || !exchangeRates.ContainsKey(TargetCurrency))
        //    {
        //        ModelState.AddModelError(string.Empty, "Invalid currency selection.");
        //        return Page();
        //    }

        //    var sourceRate = exchangeRates[SourceCurrency].Value;
        //    var targetRate = exchangeRates[TargetCurrency].Value;

        //    ConvertedAmount = Amount / sourceRate * targetRate;

        //    return Page();
        //}

        //private async Task<List<Currency>> GetAvailableCurrencies()
        //{
        //    using (var client = new HttpClient())
        //    {
        //        var response = await client.GetAsync(ApiUrl);
        //        var content = await response.Content.ReadAsStringAsync();
        //        var currencyData = JsonConvert.DeserializeObject<CurrencyData>(content);
        //        return currencyData.Valute.Values.Select(currencyRate => new Currency
        //        {
        //            Code = currencyRate.CharCode,
        //            Name = currencyRate.Name
        //        }).ToList();
        //    }
        //}

        //private async Task<Dictionary<string, CurrencyRate>> GetExchangeRates()
        //{
        //    using (var client = new HttpClient())
        //    {
        //        var response = await client.GetAsync(ApiUrl);
        //        var content = await response.Content.ReadAsStringAsync();
        //        var currencyData = JsonConvert.DeserializeObject<CurrencyData>(content);
        //        return currencyData.Valute;
        //    }
        //}


        private readonly ICurrencyConversionService _conversionService;

        public IndexModel(ICurrencyConversionService conversionService)
        {
            _conversionService = conversionService;
        }

        public List<string> AvailableCurrencies { get; } = new List<string>
        {
            "AUD", // Австралийский доллар
            "AZN", // Азербайджанский манат
            "GBP", // Фунт стерлингов Соединенного королевства
            "AMD", // Армянский драм
            "BYN", // Белорусский рубль
            "BGN", // Болгарский лев
            "BRL", // Бразильский реал
            "HUF", // Венгерский форинт
            "HKD", // Гонконгский доллар
            "DKK", // Датская крона
            "USD", // Доллар США
            "EUR", // Евро
            "INR", // Индийская рупия
            "KZT", // Казахстанский тенге
            "CAD", // Канадский доллар
            "KGS", // Киргизский сом
            "CNY", // Китайский юань
            "MDL", // Молдавский лей
            "NOK", // Норвежская крона
            "PLN", // Польский злотый
            "RON", // Румынский лей
            "XDR", // СДР (специальные права заимствования)
            "SGD", // Сингапурский доллар
            "TJS", // Таджикский сомони
            "TRY", // Турецкая лира
            "TMT", // Новый туркменский манат
            "UZS", // Узбекский сум
            "UAH", // Украинская гривна
            "CZK", // Чешская крона
            "SEK", // Шведская крона
            "CHF", // Швейцарский франк
            "ZAR", // Южноафриканский рэнд
            "KRW", // Южнокорейская вона
            "JPY",  // Японская иена
        };


        [BindProperty]
        public decimal? ConversionResult { get; set; }

        public void OnGet()
        {
            // Пустый метод, используется для отображения страницы при первом запросе
        }


        public async Task OnPostAsync(decimal amount, string sourceCurrency, string targetCurrency)
        {

            ConversionResult = await _conversionService.ConvertCurrencyAsync(amount, sourceCurrency, targetCurrency);
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
