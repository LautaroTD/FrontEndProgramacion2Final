using FrontEndProgramacion2Final.DTO;

namespace FrontEndProgramacion2Final.Models
{
    public class GrupoObjeto
    {

        public string Nombre { get; set; } = "";
        public List<DTOArticuloExpandible> Items { get; set; } = new();
        public bool IsExpanded { get; set; }

        // PAGINACIÓN INTERNA
        public int PaginaActual { get; set; } = 1;
        public int ItemsPorPagina { get; set; } = 5; //cambiar este valor aumentar el tama;o del as sublistas

        public int TotalPaginas =>
            (int)Math.Ceiling((double)Items.Count / ItemsPorPagina);

        public IEnumerable<DTOArticuloExpandible> ItemsPaginados =>
            Items
                .Skip((PaginaActual - 1) * ItemsPorPagina)
                .Take(ItemsPorPagina);
    }
}
