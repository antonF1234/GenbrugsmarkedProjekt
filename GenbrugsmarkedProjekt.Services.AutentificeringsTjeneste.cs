    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        // Forsøg at hente data
        string brugerJson = null;
        try
        {
            brugerJson = await _localStorage.GetItemAsStringAsync("bruger");
        }
        catch (InvalidOperationException)
        {
            // This exception occurs during prerendering when JavaScript interop is not available.
            // In this case, return an anonymous user.
            return new AuthenticationState(_anonymBruger);
        }


        if (string.IsNullOrWhiteSpace(brugerJson))
        {
            return new AuthenticationState(_anonymBruger);
        }
