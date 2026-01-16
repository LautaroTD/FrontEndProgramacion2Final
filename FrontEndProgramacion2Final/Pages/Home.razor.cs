using Microsoft.AspNetCore.Components;

namespace FrontEndProgramacion2Final.Pages
{
    public partial class Home : ComponentBase
    {
        [Inject]
        protected NavigationManager Navigation { get; set; } = default!;

        protected void IrAExplorarCatalogo()
        {
            Navigation.NavigateTo("/ExplorarCatalogo");
        }
        protected void IrARegistrar()
        {
            Navigation.NavigateTo("/Registrar");
        }
        protected void IrALogear()
        {
            Navigation.NavigateTo("/Logear");
        }
        protected void VerUsuarios()
        {
            Navigation.NavigateTo("/VerUsuario");
        }
    }
}
