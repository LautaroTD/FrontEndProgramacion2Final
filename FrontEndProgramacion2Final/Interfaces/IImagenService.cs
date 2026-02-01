using FrontEndProgramacion2Final.DTO;
using FrontEndProgramacion2Final.Models;
namespace FrontEndProgramacion2Final.Interfaces
{
    public interface IImagenService
    {
        Task<IEnumerable<Imagen?>> ObtenerTodos(int id, string tipo);
        //Task<Tuple<bool,string>> Crear(Imagen imagenNueva);
        //Task<Tuple<bool,string>> Eliminar(int id, string nombreDeImagen);

        //Visto en el Video
        //public Tuple<bool, string> GuardarImagen(IFormFile archivoDeImagen); //Ya combine esto con el crear que usaba antes, esto esta para la memoria, NO IMPLEMENTAR.
        //public bool DeleteImage(string imageFileName);
    }
}
