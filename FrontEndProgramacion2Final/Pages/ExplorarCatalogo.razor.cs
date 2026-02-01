using FrontEndProgramacion2Final.DTO;
using FrontEndProgramacion2Final.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;
using FrontEndProgramacion2Final.Models;

namespace FrontEndProgramacion2Final.Pages
{
    public partial class ExplorarCatalogo : ComponentBase
    {
        private IEnumerable<DTOArticuloExpandible> productos = Enumerable.Empty<DTOArticuloExpandible>();
        private IEnumerable<DTOArticuloExpandible> todos = Enumerable.Empty<DTOArticuloExpandible>();
        
        private List<GrupoObjeto> grupos = new();
        private List<GrupoObjeto> gruposPaginados = new();

        private int paginaActual = 1;
        private int itemsPorPagina = 5;
        private int totalPaginas => (int)Math.Ceiling((double)grupos.Count / itemsPorPagina);

        private string terminoBusqueda = "";

        protected IEnumerable<Imagen> Imagenes = Enumerable.Empty<Imagen>();


        protected override async Task OnInitializedAsync()
        {
            var temp = await articuloService.ObtenerTodos();

            foreach(var item in temp)
            {
                var articuloExpandible = new DTOArticuloExpandible
                {
                    Id = item.Id,
                    NombrePublicador = item.NombrePublicador,
                    Url = item.Url,
                    Precio = item.Precio,
                    Descripcion = item.Descripcion,
                    NombreProducto = item.NombreProducto,
                    IsExpanded = false
                };
                todos = todos.Append(articuloExpandible);
            }

            FiltrarYAgrupar();
        }

        private async Task BuscarImagenes(GrupoObjeto grupo,int id, string tipo)
        {
            foreach (var item in grupo.ItemsPaginados)
            {
                if (item.Id == id)
                {
                    item.IsExpanded = true;
                } else
                {
                    item.IsExpanded = false;
                }
            }
            Imagenes = await imagenService.ObtenerTodos(id, tipo); //Nota: los "using" de los services estan en el .razor, son los inject, se comparten con la isolacion de visual
        }

        private void FiltrarYAgrupar()
        {
            // filtrar
            var filtrados = string.IsNullOrWhiteSpace(terminoBusqueda)
                ? todos
                : todos.Where(x =>
                    x.Descripcion.Contains(terminoBusqueda, StringComparison.OrdinalIgnoreCase)
                    || x.NombreProducto.Contains(terminoBusqueda, StringComparison.OrdinalIgnoreCase));

            // agrupar
            grupos = filtrados
                .GroupBy(x => x.NombreProducto)
                .Select(g => new GrupoObjeto
                {
                    Nombre = g.Key,
                    Items = g.ToList()
                })
                .OrderBy(g => g.Nombre)
                .ToList();

            paginaActual = 1;
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
            var grupoParaReducir = gruposPaginados.ToList();

            foreach (var item in grupoParaReducir)
            {
                foreach (var item2 in item.ItemsPaginados)
                {
                    item2.IsExpanded = false;
                }
                item.IsExpanded = false;
            }

            var grupo = gruposPaginados.FirstOrDefault(g => g.Nombre == nombre);
            if (grupo != null)
            {
                grupo.IsExpanded = !grupo.IsExpanded;

                if (grupo.IsExpanded)
                    grupo.PaginaActual = 1;
            }

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

        void CambiarPaginaGrupo(GrupoObjeto grupo, int nuevaPagina)
        {
            if (nuevaPagina < 1 || nuevaPagina > grupo.TotalPaginas)
                return;

            grupo.PaginaActual = nuevaPagina;
        }

        private void OnBusquedaChanged(ChangeEventArgs e)
        {
            terminoBusqueda = e.Value?.ToString() ?? "";
            FiltrarYAgrupar();
        }

    }
}
