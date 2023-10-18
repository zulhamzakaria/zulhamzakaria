using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using PlannerApp;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// register the services. go to mudblazor.com Installation page and get the ocntent from
// 'Add font and style references' and 'Add script reference'
// then paste into wwwroot/index.html
builder.Services.AddMudServices();

// fro message handler
// register an httpclient thru IHttpFactory 
// any requests will go thru the message handler
builder.Services.AddHttpClient("PlannerApp.Api", client =>
{
    client.BaseAddress = new Uri("https://plannerapp-api-17102023.azurewebsites.net");
}).AddHttpMessageHandler<AuthorizationMessageHandler>();
builder.Services.AddTransient<AuthorizationMessageHandler>();

// register Blazor Local Storage container
builder.Services.AddBlazoredLocalStorage();

// register AuthorizationMessageHandler container
builder.Services.AddScoped(sp => sp.GetService<IHttpClientFactory>()!.CreateClient("PlannerApp.Api"));

// authorization
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, JWTAuthenticationStateProvider>();

await builder.Build().RunAsync();
