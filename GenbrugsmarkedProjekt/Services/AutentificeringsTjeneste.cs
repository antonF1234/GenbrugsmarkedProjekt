using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Security.Claims;
using System.Text.Json;

namespace GenbrugsmarkedProjekt.Services;

public class AutentificeringsTjeneste : AuthenticationStateProvider
{
    private readonly IJSRuntime _jsRuntime;
    private ClaimsPrincipal _anonymBruger = new ClaimsPrincipal(new ClaimsIdentity());

    public AutentificeringsTjeneste(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        string brugerJson;
        try
        {
            // Slet eventuelle gamle user nøgler for at rydde op
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "user");

            // Prøv at hente brugerdata fra localStorage
            // Dette vil kaste en InvalidOperationException
            brugerJson = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "bruger");
        }
        catch (InvalidOperationException)
        {
            return await Task.FromResult(new AuthenticationState(_anonymBruger));
        }
        catch (Exception ex)
        {
            // Log andre undtagelser
            Console.WriteLine($"Fejl ved hentning af bruger fra localStorage: {ex.Message}");
            return await Task.FromResult(new AuthenticationState(_anonymBruger));
        }

        if (string.IsNullOrWhiteSpace(brugerJson))
        {
            return await Task.FromResult(new AuthenticationState(_anonymBruger));
        }

        try
        {
            // Opret JsonSerializerOptions for at håndtere camelCase fra localStorage
            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var bruger = JsonSerializer.Deserialize<BrugerSession>(brugerJson, options);
            if (bruger == null || string.IsNullOrWhiteSpace(bruger.Id))
            {
                return await Task.FromResult(new AuthenticationState(_anonymBruger));
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, bruger.Id),
                new Claim(ClaimTypes.Name, bruger.Navn),
                new Claim(ClaimTypes.Email, bruger.Email)
            };
            var identitet = new ClaimsIdentity(claims, "CustomAuth");
            var principal = new ClaimsPrincipal(identitet);

            return await Task.FromResult(new AuthenticationState(principal));
        }
        catch
        {
            // Ryd ugyldige data i local storage
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "bruger");
            return await Task.FromResult(new AuthenticationState(_anonymBruger));
        }
    }

    public void MeddelBrugerLoggetInd(BrugerSession bruger)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, bruger.Id),
            new Claim(ClaimTypes.Name, bruger.Navn),
            new Claim(ClaimTypes.Email, bruger.Email)
        };
        var identitet = new ClaimsIdentity(claims, "CustomAuth");
        var principal = new ClaimsPrincipal(identitet);
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(principal)));
    }

    public void MeddelBrugerLoggetUd()
    {
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_anonymBruger)));
    }
}

public class BrugerSession
{
    public string Id { get; set; } = string.Empty;
    public string Navn { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}
