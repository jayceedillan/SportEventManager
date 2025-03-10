﻿using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportEventManager.Application.Common;
using SportEventManager.Application.Features.SportCategories.Commands.CreateSportCategory;
using SportEventManager.Application.Mappings;
using SportEventManager.Application.Services;
using SportEventManager.Domain.Entities;
using SportEventManager.Domain.Interfaces.IRepositories;
using SportEventManager.Persistence;
using SportEventManager.Persistence.DbContext;
using SportEventManager.Persistence.Identity.Services;
using SportEventManager.Persistence.Middleware;
using SportEventManager.Persistence.Repositories;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IDateTime, DateTimeService>();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("sportEventsDB")));

// 🔥 Register API Versioning
builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
});

// Register repositories and unit of work
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
//builder.Services.AddScoped<ISportCategoryRepository, SportCategoryRepository>();
//builder.Services.AddScoped<ISportRepository, SportRepository>();
builder.Services.AddScoped<IPlayerRepository, PlayerRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped(provider => new Lazy<UserManager<User>>(() => provider.GetRequiredService<UserManager<User>>()));
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<UserManager<User>>(); 
builder.Services.AddScoped<SignInManager<User>>();

// Register MediatR
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(CreateSportCategoryCommand).Assembly);
});

// Register AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// ✅ Register FluentValidation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

// Logging
builder.Services.AddLogging();

// Configure Authorization Policies
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdminRole", policy =>
        policy.RequireRole("Admin"));

    options.AddPolicy("RequireEventOrganizer", policy =>
        policy.RequireAssertion(context =>
            context.User.IsInRole("Admin") ||
            context.User.IsInRole("User")));
});

// Add Controllers
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
    });

builder.Services.AddValidatorsFromAssemblyContaining<CreateSportCommandValidator>();
// Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    Console.WriteLine("Seeding roles and admin user...");
    await SeedData.InitializeAsync(services);
}

// Configure Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<GlobalExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();
app.MapControllers();
app.Run();
