using Microsoft.AspNetCore.Authentication;
using Restaurant.Web;
using Restaurant.Web.Services;
using Restaurant.Web.Services.IServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// IHTTPClientFactory
builder.Services.AddHttpClient<IProductService, ProductService>();
builder.Services.AddHttpClient<ICartService, CartService>();
builder.Services.AddHttpClient<ICouponService, CouponService>();

// URL
SD.ProductAPIBase = builder.Configuration["ServiceURLs:ProductAPI"];
SD.ShoppingCartAPIBase = builder.Configuration["ServiceURLs:ShoppingCartAPI"];
SD.CouponAPIBase = builder.Configuration["ServiceURLs:CouponAPI"];

// Services
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<ICouponService, CouponService>();

//Identity
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "Cookies";
    options.DefaultChallengeScheme = "oidc";
}).AddCookie("Cookies", c => c.ExpireTimeSpan = TimeSpan.FromMinutes(10))
.AddOpenIdConnect("oidc", options =>
{
    options.Authority = builder.Configuration["ServiceURLs:IdentityServer"];
    options.GetClaimsFromUserInfoEndpoint = true;

    // the defined clientID inside StaticDetails.cs of Identity project
    options.ClientId = "restaurant";

    // the defined clientsecret inside StaticDetails.cs of Identity project
    options.ClientSecret = "restaurant";

    // the defined granttype inside StaticDetails.cs of Identity project
    options.ResponseType = "code";

    // This is for UserId retrieval in HomeController DetailsPost()
    options.ClaimActions.MapJsonKey("role", "role", "role");
    options.ClaimActions.MapJsonKey("sub", "sub", "sub");

    options.TokenValidationParameters.NameClaimType = "name";
    options.TokenValidationParameters.RoleClaimType = "role";
    // the defined scope inside StaticDetails.cs of Identity project
    options.Scope.Add("restaurant");
    options.SaveTokens = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Identity
// wont work if UseAuthentication is placed after UseAuthorization()
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
