using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ServiceReference1;

namespace lab7.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public double Fahrenheit { get; set; }

        [BindProperty]
        public double Celsius { get; set; }

        public double? CelsiusResult { get; set; }
        public double? FahrenheitResult { get; set; }

        public async Task OnPostToFahrenheitAsync()
        {
            var client = new ConversionServiceClient();
            FahrenheitResult = await client.CelsiusToFahrenheitAsync(Celsius);
        }

        public async Task OnPostToCelsiusAsync()
        {
            var client = new ConversionServiceClient();
            CelsiusResult = await client.FahrenheitToCelsiusAsync(Fahrenheit);
        }
    }
}
