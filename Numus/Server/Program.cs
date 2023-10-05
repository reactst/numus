using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Numus.Server.Data;
using Numus.Shared.Interfaces;
using Numus.Server.Services;
using System.Text;
using Numus.Server;
using AutoMapper;
using QuestPDF.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddAutoMapper(typeof(Program));
var mapperConfiguration = new MapperConfiguration(cfg =>
{
    var serviceProvider = builder.Services.BuildServiceProvider();
    var mapper = serviceProvider.GetService<IMapper>();
    cfg.AddProfile(new AutoMapperProfile(mapper));
});

var mapper = mapperConfiguration.CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<NumusServerContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("NumusServerContext")));
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<IInvoiceService, InvoiceService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IPdfService, PdfService>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JWTSecret").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins("https://localhost:7212",
                                "http://localhost:5070")
            .AllowAnyMethod()
            .AllowAnyHeader();
        });
});

QuestPDF.Settings.License = LicenseType.Community;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetService<NumusServerContext>();
        dbContext.Database.Migrate();
    }
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();
app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();