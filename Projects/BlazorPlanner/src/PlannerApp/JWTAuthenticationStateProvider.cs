using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

public class JWTAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly ILocalStorageService _localStorageService;
    public JWTAuthenticationStateProvider(ILocalStorageService localStorageService)
    {
        _localStorageService = localStorageService;
    }
    public async override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        if(await _localStorageService.ContainKeyAsync("access_token"))
        {
            var tokenString = await _localStorageService.GetItemAsStringAsync("access_token");
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.ReadJwtToken(tokenString);
            var identity = new ClaimsIdentity(token.Claims, "Bearer");
            var user = new ClaimsPrincipal(identity);
            var authState = new AuthenticationState(user);

            NotifyAuthenticationStateChanged(Task.FromResult(authState));

            return authState;
        }
        // empty claims -> no identity -> user not logged in
        return new AuthenticationState(new ClaimsPrincipal());
    }
}