using Core.Infrastructure;
using Core.Middlewares;
using Core.Models;
using Core.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Text.Json;

////////////////////
//+--------------+//
//|   SERVICES   |//
//+--------------+//
////////////////////

//setup services
var builder = WebApplication.CreateBuilder(args);

//-----------------------
// OPTION PATTERN - START
builder.Services.Configure<FruitOptions>(options =>
{
    options.Name = "Wassermelon";
});
// OPTION PATTERN - END
//---------------------
//-----------------------------
// DEPENDENCY INJECTION - START

//builder.Services.AddSingleton<IResponseFormatter, HtmlResponseFormatter>();

//builder.Services.AddTransient<IResponseFormatter, GuidService>();

// Can only be used within a scope
builder.Services.AddScoped<IResponseFormatter, GuidService>();

// DEPENDENCY INJECTION - END
//---------------------------
//---------------
// CONFIG - START

// Calls "Fruit" from appsettings.json
var serviceConfig = builder.Configuration;
builder.Services.Configure<FruitOptions>(serviceConfig.GetSection("Fruit"));

// CONFIG - END
//-------------
//---------------------------
// COOKIES & SESSIONS - START

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.IsEssential = true;
});

// COOKIES & SESSIONS - END
//-------------------------
//--------------
// HTTPS - START

// Enable HSTS
builder.Services.AddHsts(options =>
{
    // Max age for how long should the browser makes only HTTPS requests
    options.MaxAge = TimeSpan.FromDays(1);
    options.IncludeSubDomains = true;
});

// HTTPS - END
//------------
//------------------
// DB ACCESS - START

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration["ConnectionStrings:SQLDbConnection"]);
});

// DB ACCESS - END
//----------------
//-----------------
// IDENTITY - START

builder.Services.AddDbContext<IdentityDataContext>(options =>
{
    options.UseSqlServer(builder.Configuration["ConnectionStrings:SQLDbConnection"]);
});

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<IdentityDataContext>()
    .AddDefaultTokenProviders();

// Re-set default password validity chcking
builder.Services.Configure<IdentityOptions>(options =>
    {
        options.Password.RequiredLength = 4;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireLowercase  = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireDigit = false;
        options.User.RequireUniqueEmail = true;
    });

// IDENTITY - END
//---------------
//----------------------------------------
// JSON OPTION FOR RETURNED OBJECT - START

builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.Configure<MvcNewtonsoftJsonOptions>(options =>
{
    options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
});

// JSON OPTION FOR RETURNED OBJECT - END
//--------------------------------------
//--------------------
// RAZOR PAGES - START

//builder.Services.AddRazorPages();

// RAZOR PAGES - END
//------------------

//// How to change the defaults paths
//builder.Services.Configure<CookieAuthenticationOptions>(IdentityConstants.ApplicationScheme, options => {
//    options.LoginPath = "/login2";
//    options.AccessDeniedPath = "/accessdenied2";
//});

//// Register Controllers
//builder.Services.AddControllers();
//// Register Controllers with Views
builder.Services.AddControllersWithViews();


//+--------------+
//|  MIDDLEWARE  |
//+--------------+
//

//setup middleware component
var app = builder.Build();
//----------------
// LOGGING - START

app.Logger.LogDebug("Pipeline configuration starting...");

// LOGGING - END
//--------------
//-------------------
////CUSTOM MIDDLEWARE

//app.Use(async (context, next) => { 
//    if(context.Request.Method == HttpMethods.Get && context.Request.Query["custom"] == "true")
//    {
//        context.Response.ContentType = "application/json";
//        await context.Response.WriteAsync("Custom Middleware...\n");
//    }
//    //similar
//    //await next.Invoke();
//    await next();
//});



////CHANGE MIDDLEWARE AFTER IT"S BEING CALLED
////this will allow to middleware to add content along the pipeline
////allows middleware to make changes to the response
//app.Use(async (context, next) => { 
//    await next();
//    await context.Response.WriteAsync($"\nStatus Code:{context.Response.StatusCode}");
//});



////TO SHORT_CURCUIT MIDDLEWARE
////not calling the next middleware
//app.Use(async (context, next) => { 
//    if(context.Request.Path == "/short")
//    {
//        await context.Response.WriteAsync("\nRequest short-circuited");
//    }
//    else
//    { await next(); }
//});



////MAP METHOD 
////is used to process request for specific URL
//((IApplicationBuilder)app).Map("/branch", branch => {
//    branch.Use(async (HttpContext context, Func<Task> next) => {
//        await context.Response.WriteAsync("Branch Middleware");
//    });
//});



//.Run IS A TERMINAL MIDDLEWARE
/*
    /branch -> empty page
    /branch/?custom=true -> returns From Middlesware class...
*/
//((IApplicationBuilder)app).Map("/branch", branch => {
//    branch.Run(new Middleware().Invoke);
//});


//-----------------------
// OPTION PATTERN - START

////Commented to use FruitMiddleware class instead
//app.MapGet("/fruit", async(HttpContext context, IOptions<FruitOptions> FruitOptions) => 
//{
//    FruitOptions options = FruitOptions.Value;
//    await context.Response.WriteAsync($"{options.Name},{options.Color}");
//});
app.UseMiddleware<FruitMiddleware>();

// OPTION PATTERN - END
//---------------------
//-----------------------------
// DEPENDENCY INJECTION - START

//IResponseFormatter formatter = new TextResponseFormatter();
app.MapGet("/formatter1", async (HttpContext context, IResponseFormatter formatter) =>
{
    await formatter.Format(context, "Formatter 1");
});
app.MapGet("/formatter2", async (HttpContext context, IResponseFormatter formatter) =>
{
    await formatter.Format(context, "Formatter 2");
});

app.UseMiddleware<CustomMiddleware>();
// For AddScoped, this is invalid since it's creating another Scoped service?
//app.UseMiddleware<CustomMiddlewareTransient>();

app.MapGet("/endpoint", CustomEndpoint.Endpoint);

// DEPENDENCY INJECTION - END
//---------------------------
//----------------------
// CONFIGURATION - START

app.MapGet("/config", async (HttpContext context, IConfiguration configuration) =>
{

    // Looks for "Default" value thats nested inside Logging > LogLevel of appsettings.Development.json
    // This is defined inside launchSettings.json
    string defaultDebug = configuration["Logging:LogLevel:Default"];
    await context.Response.WriteAsync(defaultDebug);

    // Get env from launchSettings.json
    string env = configuration["ASPNETCORE_ENVIRONMENT"];
    await context.Response.WriteAsync($"\n{env}");

    // Determine the environment
    if (app.Environment.IsDevelopment())
    {
        await context.Response.WriteAsync("\nisDevelopment");
    }
    else { await context.Response.WriteAsync($"\n{env}"); }

});

app.UseMiddleware<FruitMiddleware>();

// CONFIGURATION - END
//--------------------
//-----------------
// DATABASE - START

var context = app.Services.CreateScope().ServiceProvider.GetRequiredService<DataContext>();
SeedData.SeedDatabase(context);

// DATABASE - END
//---------------
//--------------------
// RAZOR PAGES - START

//app.MapRazorPages();

// RAZOR PAGES - END
//------------------


app.UseMiddleware<Middleware>();

//// Commented to use HOME controller as the default
//app.MapGet("/", () => "Program file!\n");
//app.MapGet("/sweets", () => "Sweets");


//--------------------------
// STATIC MIDDLEWARE - START

//To use static file:
app.UseStaticFiles();

// STATIC MIDDLEWARE - END
//------------------------
//---------------------------
// COOKIES & SESSIONS - START

// Set Cookies
app.MapGet("/cookie", async (context) =>
{
    int counter = int.Parse(context.Request.Cookies["counter"] ?? "0") + 1;
    context.Response.Cookies.Append(
            "counter",
            counter.ToString(),
            new CookieOptions
            {
                MaxAge = TimeSpan.FromMinutes(30),
            }
        );
    await context.Response.WriteAsync($"cookie {counter}");
});

// Clear Cookies
app.MapGet("/clearcookie", context =>
{
    context.Response.Cookies.Delete("counter");
    context.Response.Redirect("/");
    return Task.CompletedTask;
});

// Call Session Middleware
// Note: data is stored in dictionary with values in either string or int
// Note: use serialization for more complex data type
app.UseSession();

// Set Cookies
app.MapGet("/session", async (context) =>
{
    int counter = (context.Session.GetInt32("counter") ?? 0) + 1;
    context.Session.SetInt32("counter", counter);
    // Optional, but using it is a good practice since it'll throw an exception
    // if the session data cant be stroed inside the cache
    await context.Session.CommitAsync();
    await context.Response.WriteAsync($"Session: {counter}");
});

// COOKIES & SESSIONS - END
//-------------------------
//--------------
// HTTPS - START

app.MapGet("/https", async (context) =>
{
    await context.Response.WriteAsync($"HTTPS Request: { context.Request.IsHttps}");
});

// Enforce HTTPS
app.UseHttpsRedirection();

// HSTS header lets browser sends https request tho http is used
// Set the rule only or Production
if (app.Environment.IsProduction())
    app.UseHsts();


// HTTPS - END
//------------
//-----------------
// APIS - START

//const string BASEURL = "/api/products";

//// GET Product by Id
//app.MapGet($"{BASEURL}/{{id}}", async (HttpContext context, DataContext dataContext) =>
//{
//    string id = context.Request.RouteValues["id"] as string;
//    if(id != null)
//    {
//        Product product = dataContext.Products.Find(long.Parse(id));
//        if(product == null)
//        {
//            context.Response.StatusCode = StatusCodes.Status404NotFound;
//        }
//        else
//        {
//            context.Response.ContentType = "application/json";
//            await context.Response.WriteAsync(JsonSerializer.Serialize<Product>(product));
//        }
//    }
//});

//// GET All Products
//app.MapGet($"{BASEURL}", async (HttpContext context, DataContext dataContext) =>
//{
//            context.Response.ContentType = "application/json";
//            await context.Response.WriteAsync(JsonSerializer.Serialize<IEnumerable<Product>>(dataContext.Products));
//});

//// POST Products
//app.MapPost($"{BASEURL}", async (HttpContext context, DataContext dataContext) =>
//{
//    Product product = await JsonSerializer.DeserializeAsync<Product>(context.Request.Body);
//    if(product != null)
//    {
//        await dataContext.AddAsync(product);
//        await dataContext.SaveChangesAsync();
//        context.Response.StatusCode = StatusCodes.Status200OK;
//    }
//});

// APIS - END
//---------------
//-----------------
// IDENTITY - START

app.UseAuthentication();
app.UseAuthorization();

// IDENTITY - END
//---------------


// Register Controllers
app.MapControllers();
//--------------
// AREAS - START

// Registering Area
// Has to be before MapDefaultControllerRoute()
app.MapControllerRoute(
    name: "Areas",
    pattern: "/{area:exists}/{controller=Users}/{action=Index}/{id?}"
);

// AREAS - END
//------------
//------------------------
// ROUTING FOR MVC - START

//// Of the similar result
//app.MapControllerRoute("Default", "{controller=Home}/{action=Index}/{id?}");
//app.MapControllerRoute("Default", "{controller=Home}/{action=Index}/{id?}");
app.MapDefaultControllerRoute();

// ROUTING FOR MVC - END
//----------------------
//----------------
// LOGGING - START

app.Logger.LogDebug("Pipeline configuration stopped...");

// LOGGING - END
//--------------

// terminal middleware
app.Run();
