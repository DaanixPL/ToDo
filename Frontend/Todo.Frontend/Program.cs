using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Todo.Frontend;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
var apiUrl = builder.Configuration["API_BASE_URL"] ?? "https://localhost:5000";
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMudServices();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://todo-t283.onrender.com") });

await builder.Build().RunAsync();
