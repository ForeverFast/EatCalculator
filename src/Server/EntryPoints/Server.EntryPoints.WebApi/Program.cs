using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Server.Core;
using Server.Core.Resources;
using Server.EntryPoints.WebApi;
using Server.EntryPoints.WebApi.Middlewares;
using Server.EntryPoints.WebApi.Swagger.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ApiBehaviorOptions>(options => options.SuppressInferBindingSourcesForParameters = true);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "Server.EntryPoints.WebApi API", Version = "v1" });

    opt.OperationFilter<OptionalRouteParameterOperationFilter>();
    opt.OperationFilter<SwaggerHideParameterOperationFilter>();
    opt.DocumentFilter<JsonPatchDocumentFilter>();

    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    opt.AddSecurityRequirement(new OpenApiSecurityRequirement()
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

builder.Services.AddCors(options
    => options.AddDefaultPolicy(policy
        => policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader())
    );

builder.Services
    .AddAuthentication(opt =>
    {
        opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = TokenDefaults.Issuer,
            ValidAudience = TokenDefaults.Audience,
            IssuerSigningKey = TokenDefaults.GetSymmetricSecurityKey(),

            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
        };
    });
builder.Services.AddAuthorization();
builder.Services.AddHealthChecks();

builder.Services.AddServerCore(builder.Configuration);
builder.Services.AddApiServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<RequestCheckMiddleware>();

app.MapControllers();
app.MapHealthChecks("/health-check");

app.UseServerCore();

app.Run();
