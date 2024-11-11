using Assignment.CustomMiddleware;
using Assignment.Helper;
using Assignment.Repositories.Implements.MSSql;
using Assignment.Repositories.Interfaces;
using Assignment.Services.Implements;
using Assignment.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using NLog.Extensions.Logging;
using System.ComponentModel;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


// Configure NLog
builder.Services.AddLogging(logging =>
{
    logging.ClearProviders();
    logging.SetMinimumLevel(LogLevel.Trace);
});

// Add NLog as the logger provider
builder.Services.AddSingleton<ILoggerProvider, NLogLoggerProvider>();

//For setting the connection string for DBContext
builder.Services.AddDbContext<Assignment.DBO.DbAa587cAssesmentContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string have some issues.")));

// lifetime for the services

builder.Services.AddScoped<IAccountsRepo, AccountRepoMsSql>();
builder.Services.AddScoped<ITransactionsRepo, TransactionRepoMsSql>();


builder.Services.AddTransient<IAccountService, AccountService>();
builder.Services.AddTransient<ITransactionService, TransactionService>();

builder.Services.AddTransient<ValidateHelper>();
builder.Services.AddTransient<InputSanitizationService>();

var encryptionKey = builder.Configuration["EncryptionSettings:EncryptionKey"];
builder.Services.AddSingleton(new EncryptionService(encryptionKey));




builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

//Adding the rule for the CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", policy =>
    {
        policy.WithOrigins("https://localhost:7195")
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

//Add,ing the custom middleware
app.UseMiddleware<ContentSecurityPolicyMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("AllowSpecificOrigin");


app.UseAuthorization();

app.MapControllers();

app.Run();

//Below is the Scaffold command to generate classes corrosponding database tables
//Scaffold-DbContext "Data Source=SQL6031.site4now.net;Initial Catalog=db_aa587c_assesment;Persist Security Info=True;User ID=db_aa587c_assesment_admin;Password=Arijit@123;Trust Server Certificate=True" Microsoft.EntityFrameworkCore.SqlServer -OutputDir scaffolding -force
