using FrontEndProgramacion2Final.Interfaces;
using FrontEndProgramacion2Final.Models;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace FrontEndProgramacion2Final.Services
{
    public class ImagenService : IImagenService
    {
        private readonly HttpClient _http;
        private readonly ILogger<ImagenService> _logger;
        public ImagenService(HttpClient http, ILogger<ImagenService> logger)
        {
            _http = http;
            _logger = logger;
        }

        public async Task<IEnumerable<Imagen?>> ObtenerTodos(int id, string tipo)
        {
            try
            {
                return await _http.GetFromJsonAsync<IEnumerable<Imagen>>($"api/Imagen/search/{tipo}/{id}"); //Nota: tenes que usar $"", NO SIRVE "", y tenes que poner los {} con los nombres de las variables de aca, en el caso que la api use {} claro.
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message, "Error en ImagenService");
                throw new Exception(ex.Message);
            }
        }
    }
}
