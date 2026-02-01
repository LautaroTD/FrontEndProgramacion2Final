using FrontEndProgramacion2Final;
using FrontEndProgramacion2Final.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Net.Http;
using FrontEndProgramacion2Final.Interfaces;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:7023") //ACA EL PUERTO DE LA API //podes averiguar este ejecutando la api y viendo que te pone el visual
}); //Nota: Para hacer que el puerto del proyecto se mantenga el mismo pone en la consola "set ASPNETCORE_URLS=https://localhost:PUERTO". Aun es probable que se cambie si no usas "dotnet run" para ejecutar el proyecto.

builder.Services.AddScoped<ArticuloService>();
builder.Services.AddScoped<ImagenService>();

await builder.Build().RunAsync();

//flaco, buscate un video, ni a palo te ayuda chatgpt con esto