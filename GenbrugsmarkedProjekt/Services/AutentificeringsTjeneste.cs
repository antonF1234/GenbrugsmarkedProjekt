using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Text.Json;

namespace GenbrugsmarkedProjekt.Services;

public class AutentificeringsTjeneste : AuthenticationStateProvider
{
    private readonly ILocalStorageService _localStorage;

    private ClaimsPrincipal _anonymBruger = new ClaimsPrincipal(new ClaimsIdentity());

    public AutentificeringsTjeneste(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        string brugerJson = null;
        try
        {
            brugerJson = await _localStorage.GetItemAsStringAsync("bruger");
        }
        catch (InvalidOperationException)
        {
            // This happens during prerendering when JavaScript interop is not available.
            // Return an anonymous user state. The client-side will re-evaluate later.
            return new AuthenticationState(_anonymBruger);
        }

        if (string.IsNullOrWhiteSpace(brugerJson))
        {
            return new AuthenticationState(_anonymBruger);
        }

        try
        {
            var bruger = JsonSerializer.Deserialize<BrugerSession>(brugerJson);
            if (bruger == null || string.IsNullOrWhiteSpace(bruger.Id))
            {
                return new AuthenticationState(_anonymBruger);
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, bruger.Id),
                new Claim(ClaimTypes.Name, bruger.Navn),
                new Claim(ClaimTypes.Email, bruger.Email)
            };

            var identitet = new ClaimsIdentity(claims, "CustomAuth");
            var principal = new ClaimsPrincipal(identitet);

            return new AuthenticationState(principal);
        }
        catch
        {
            await _localStorage.RemoveItemAsync("bruger");
            return new AuthenticationState(_anonymBruger);
        }
    }

    public async Task MeddelBrugerLoggetInd(BrugerSession bruger)
    {
        await _localStorage.SetItemAsync("bruger", bruger);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, bruger.Id),
            new Claim(ClaimTypes.Name, bruger.Navn),
            new Claim(ClaimTypes.Email, bruger.Email)
        };

        var identitet = new ClaimsIdentity(claims, "CustomAuth");
        var principal = new ClaimsPrincipal(identitet);

        NotifyAuthenticationStateChanged(
            Task.FromResult(new AuthenticationState(principal)));
    }

    public async Task MeddelBrugerLoggetUd()
    {
        await _localStorage.RemoveItemAsync("bruger");
        NotifyAuthenticationStateChanged(
            Task.FromResult(new AuthenticationState(_anonymBruger)));
    }
}

public class BrugerSession
{
    public string Id { get; set; } = string.Empty;
    public string Navn { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}
