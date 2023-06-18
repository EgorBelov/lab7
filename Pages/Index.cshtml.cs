
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ServiceReference1;


namespace lab7.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public double Fahrenheit { get; set; }

        public double? Result { get; set; }

        public async Task OnPostAsync()
        {
            // Создаем прокси-клиента для вашего WCF-сервиса
            var client = new ConversionServiceClient();

            // Вызываем метод сервиса для конвертации фаренгейтов в цельсии
            Result = await client.FahrenheitToCelsiusAsync(Fahrenheit);
        }
    }
}