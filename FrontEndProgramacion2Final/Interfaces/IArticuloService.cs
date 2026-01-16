using FrontEndProgramacion2Final.DTO;

namespace FrontEndProgramacion2Final.Interfaces
{
    public interface IArticuloService
    {
        Task<IEnumerable<DTOArticulo?>> ObtenerTodos();
    }
}
