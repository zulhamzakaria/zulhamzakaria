using Microsoft.AspNetCore.Authentication.JwtBearer;
using MinimalAPI.Models;
using MinimalAPI.Services;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Description = "Bearer Authentication with JWT Token",
        Type = SecuritySchemeType.Http
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new List<string>()
        }
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateActor = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidAudience = builder.Configuration["JWT:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
    };
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton<IMovieService, MovieService>();
builder.Services.AddSingleton<IUserService, UserService>();

var app = builder.Build();

app.UseSwagger();
app.UseAuthorization();
app.UseAuthentication();

//app.MapGet("/", () => "Hello World!");

app.MapPost("/login", (UserLogin user, IUserService service) => Login(user, service)).Accepts<UserLogin>("application/json").Produces<string>();

app.MapPost("/create",
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
(Movie movie, IMovieService movieService) => Create(movie, movieService))
    .Accepts<Movie>("application/json")
    .Produces<Movie>(statusCode: 200, contentType: "application/json");

app.MapGet("get",
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Standard,Administrator")]
(int id, IMovieService movieService) => Get(id, movieService))
    .Produces<Movie>();

app.MapGet("list/", (IMovieService movieService) => List(movieService))
    .Produces<List<Movie>>(statusCode:200, contentType: "application/json");

app.MapPut("/update",
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
(Movie movie, IMovieService movieService) => Update(movie, movieService))
    .Accepts<Movie>("application/json")
    .Produces<Movie>(statusCode:200, contentType:"application/json");

app.MapDelete("/delete",
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
(int id, IMovieService movieService) => Delete(id, movieService));

IResult Login(UserLogin user, IUserService service)
{
    if (string.IsNullOrEmpty(user.Username) && string.IsNullOrEmpty(user.Password))
    {
        var loggedInUser = service.GetUser(user);
        if (loggedInUser != null) return Results.NotFound("User Not Found");

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, loggedInUser.Username),
            new Claim(ClaimTypes.Email, loggedInUser.EmailAddress),
            new Claim(ClaimTypes.GivenName, loggedInUser.Givenname),
            new Claim(ClaimTypes.Surname, loggedInUser.Surname),
            new Claim(ClaimTypes.Role, loggedInUser.Role)
        };

        var token = new JwtSecurityToken(
            issuer: builder.Configuration["JWT:Issuer"],
            audience: builder.Configuration["JWT:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddDays(60),
            notBefore: DateTime.UtcNow,
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])),
                SecurityAlgorithms.HmacSha256)
            );
        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
        return Results.Ok(tokenString);
    }
    return Results.Ok();
}

IResult Create(Movie movie, IMovieService movieService)
{
    var result = movieService.CreateMovie(movie);
    return Results.Ok(result);
}

IResult Get(int id, IMovieService movieService)
{
    var movie = movieService.Get(id);
    if (movie is null) return Results.NotFound("Movie not found");
    return Results.Ok(movie);
}

IResult List(IMovieService movieService)
{
    var movies = movieService.List();
    return Results.Ok(movies);
}

IResult Update(Movie movie, IMovieService movieService)
{
    var updateMovie = movieService.Update(movie);
    if (updateMovie is null) return Results.NotFound("Movie not Found");
    return Results.Ok(updateMovie);
}

IResult Delete(int id, IMovieService movieService)
{
    var result = movieService.Delete(id);
    if (!result) return Results.BadRequest("Something went wrong");
    return Results.Ok(result);
}
app.UseSwaggerUI();
app.Run();
