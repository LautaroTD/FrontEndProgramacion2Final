using FrontEndProgramacion2Final.DTO;
using FrontEndProgramacion2Final.Models;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;
using FrontEndProgramacion2Final.Interfaces;

namespace FrontEndProgramacion2Final.Services
{
    public class ArticuloService : IArticuloService
    {
        private readonly HttpClient _http;
        private readonly ILogger<ArticuloService> _logger;
        public ArticuloService(HttpClient http, ILogger<ArticuloService> logger)
        {
            _http = http;
            _logger = logger;
        }
        public async Task<IEnumerable<DTOArticulo>> ObtenerTodos()
        {
            try
            {
                return await _http.GetFromJsonAsync<IEnumerable<DTOArticulo>>("api/Articulo/getAll");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message, "Error en ArticuloService");
                throw new Exception(ex.Message);
            }
        }
    }
}
