using GenbrugsmarkedProjekt.Components;
using GenbrugsmarkedProjekt.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Blazored.LocalStorage;


var builder = WebApplication.CreateBuilder(args);

// Razor Components (interactive server)
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Registrer AutentificeringsTjeneste
builder.Services.AddScoped<AuthenticationStateProvider, AutentificeringsTjeneste>();
builder.Services.AddAuthorizationCore(); // Nødvendig for autentificering/autorisering
builder.Services.AddScoped<FileService, FileService>();

// HttpClient
// Optionally set ApiBaseUrl via configuration (e.g., environment variable "ApiBaseUrl")
builder.Services.AddHttpClient("Api", (sp, client) =>
{
    var cfg = sp.GetRequiredService<IConfiguration>();
    var baseUrl = cfg["ApiBaseUrl"];

    // Fallback for development if not configured
    if (string.IsNullOrWhiteSpace(baseUrl))
    {
        baseUrl = "https://localhost:5000";   // <--- SKIFT TIL DEN PORT, JERES API FAKTISK KØRER PÅ!
    }

    // Ensure trailing slash to build relative URIs correctly
    if (!baseUrl.EndsWith("/"))
    {
        baseUrl += "/";
    }

    client.BaseAddress = new Uri(baseUrl);
});

// Default HttpClient resolves to the named "Api" client
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("Api"));
builder.Services.AddBlazoredLocalStorage();

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();