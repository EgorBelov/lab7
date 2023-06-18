using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ServiceReference2;

namespace lab7.Pages._4
{
    public class IndexModel : PageModel
    {
        private readonly ICurrencyConversionService _currencyConversionService;

        public IndexModel(ICurrencyConversionService currencyConversionService)
        {
            _currencyConversionService = currencyConversionService;
        }

        [BindProperty]
        public decimal? ConversionResult { get; set; }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost(decimal amount, string sourceCurrency, string targetCurrency)
        {
            try
            {
                ConversionResult = await _currencyConversionService.ConvertCurrencyAsync(amount, sourceCurrency, targetCurrency);
            }
            catch (ArgumentException)
            {
                ModelState.AddModelError(string.Empty, "Invalid currency code.");
            }

            return Page();
        }
    }
}
