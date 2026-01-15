using Microsoft.AspNetCore.Components;

namespace FrontEndProgramacion2Final.Pages
{
    public partial class Home : ComponentBase
    {
        protected string Title { get; set; } = "Home";

        protected int CurrentCount { get; set; }

        protected void IncrementCount()
        {
            CurrentCount++;
        }
    }
}
