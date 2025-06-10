using WrkArk.Components;
using ElectronNET.API;
using ElectronNET.API.Entities;
using Blazorise;
using Blazorise.Icons.FontAwesome;
using Blazorise.Tailwind;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseElectron(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddElectron();
builder.Services.AddBlazorise()
    .AddTailwindProviders()
    .AddFontAwesomeIcons();

if (HybridSupport.IsElectronActive)
{
    // Open the Electron-Window here
    Task.Run(async () => {
        var window = await Electron.WindowManager.CreateWindowAsync();
        window.OnClosed += () => {
            Electron.App.Quit();
        };
    });
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<WrkArk.Components.App>()
    .AddInteractiveServerRenderMode();

app.Run();

