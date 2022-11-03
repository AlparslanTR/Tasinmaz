using CoreLayer.Models;
using CoreLayer.Security;
using CoreLayer.Services.Abstract;
using CoreLayer.Services.Concrete;
using EntityLayer.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Configuration;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(options =>

                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore

            );
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITasinmazService, TasinmazService>();
builder.Services.AddScoped<ILogService, LogService>();
builder.Services.AddHttpClient();
builder.Services.AddCors(options=>options.AddDefaultPolicy(policy=> policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));
// Swaggera Token yazdýrma
builder.Services.AddSwaggerGen(setup =>
{
    // Include 'SecurityScheme' to use JWT Authentication
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

    setup.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtSecurityScheme, Array.Empty<string>() }
    });

});
// Swaggera Token yazdýrma
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("SqlConnection"),
    sqloptions =>
    {
        sqloptions.MigrationsAssembly("CoreLayer");
    });
});
var key = Encoding.ASCII.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
    };
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors();
app.UseAuthentication();
app.UseHttpsRedirection();
app.UseAuthorization();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor |
    ForwardedHeaders.XForwardedProto
});
app.MapControllers();

app.Run();
