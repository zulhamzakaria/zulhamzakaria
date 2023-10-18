using Blazored.LocalStorage;
using System.Net.Http.Headers;

public class AuthorizationMessageHandler : DelegatingHandler
{
    // for storing acccess_token
    private readonly ILocalStorageService _storage;

    public AuthorizationMessageHandler(ILocalStorageService storage)
    {
        _storage = storage;
    }
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (await _storage.ContainKeyAsync("access_token"))
        {
            var token = await _storage.GetItemAsStringAsync("access_token");
            // setting the existing token to the Header
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        Console.WriteLine("Authorization Message Handler Called");

        return await base.SendAsync(request, cancellationToken);
    }
}