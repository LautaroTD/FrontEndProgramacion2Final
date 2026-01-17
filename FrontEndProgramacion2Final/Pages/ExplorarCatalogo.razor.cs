using FrontEndProgramacion2Final.DTO;
using Microsoft.AspNetCore.Components;

namespace FrontEndProgramacion2Final.Pages
{
    public partial class ExplorarCatalogo : ComponentBase
    {
        private IEnumerable<DTOArticulo> productos = Enumerable.Empty<DTOArticulo>();

        private IEnumerable<DTOArticulo> todos = Enumerable.Empty<DTOArticulo>();
        private List<GrupoObjeto> grupos = new();
        private List<GrupoObjeto> gruposPaginados = new();

        private int paginaActual = 1;
        private int itemsPorPagina = 5;
        private int totalPaginas => (int)Math.Ceiling((double)grupos.Count / itemsPorPagina);

        protected override async Task OnInitializedAsync()
        {

            // Ejemplo de carga — reemplazar con tu fuente real
            todos = await articuloService.ObtenerTodos();

            // agrupar por nombre
            grupos = todos
                .GroupBy(x => x.NombreProducto)
                .Select(g => new GrupoObjeto
                {
                    Nombre = g.Key,
                    Items = g.ToList()
                })
                .OrderBy(g => g.Nombre)
                .ToList();

            CargarPagina();
        }

        void CargarPagina()
        {
            gruposPaginados = grupos
                .Skip((paginaActual - 1) * itemsPorPagina)
                .Take(itemsPorPagina)
                .ToList();
        }

        void ToggleGrupo(string nombre)
        {
            var grupo = gruposPaginados.FirstOrDefault(g => g.Nombre == nombre);
            if (grupo != null)
                grupo.IsExpanded = !grupo.IsExpanded;
        }

        void NextPage()
        {
            if (paginaActual < totalPaginas)
            {
                paginaActual++;
                CargarPagina();
            }
        }

        void PrevPage()
        {
            if (paginaActual > 1)
            {
                paginaActual--;
                CargarPagina();
            }
        }

        // clase interna solo para UI
        public class GrupoObjeto
        {
            public string Nombre { get; set; } = "";
            public List<DTOArticulo> Items { get; set; } = new();
            public bool IsExpanded { get; set; }
        }
    }
}
