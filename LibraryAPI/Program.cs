using LibraryAPI.Data;
using LibraryAPI.Services.Abstract;
using LibraryAPI.Services.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
#region Scoped and DbContext

builder.Services.AddDbContext<ILibraryContext, LibraryContext>(x => x.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection")));
builder.Services.AddScoped<IBooksService, BooksService>();
builder.Services.AddScoped<IBookGenresService, BookGenresService>();
builder.Services.AddScoped<IBookAuthorsService, BookAuthorsService>();

#endregion





#region Authentication

    // For Identity
    builder.Services.AddIdentity<IdentityUser, IdentityRole>(
        options =>
        {
            options.Password.RequireUppercase = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredLength = 5;
            options.Password.RequireDigit = false;
        })
            .AddEntityFrameworkStores<LibraryContext>()
            .AddDefaultTokenProviders();

    //Adding Authentication
    builder.Services.AddAuthentication(options => {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = builder.Configuration.GetSection("Token").GetValue<bool>("CheckIssuer"),
            ValidIssuers = builder.Configuration.GetSection("Token").GetValue<IEnumerable<string>>("Issuers"),
            ValidateAudience = builder.Configuration.GetSection("Token").GetValue<bool>("CheckAudience"),
            ValidAudiences = builder.Configuration.GetSection("Token").GetValue<IEnumerable<string>>("Audiences"),
            ValidateIssuerSigningKey = builder.Configuration.GetSection("Token").GetValue<bool>("CheckSigningKey"),
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Token").GetValue<string>("SuperSecretKey")))
        };
    });

#endregion
builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
#region Swagger

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ASP.NET_Booking", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please insert your token",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                          new OpenApiSecurityScheme
                          {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                                Scheme = "oauth2",
                                Name = "Bearer",
                                In = ParameterLocation.Header,
                          },
                          new List<string>()
                        }
                    });
});
#endregion
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Authentication & Authorization
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
