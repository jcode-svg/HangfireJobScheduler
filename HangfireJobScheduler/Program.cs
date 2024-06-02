using Hangfire;
using Hangfire.MemoryStorage;
using HangfireJobScheduler.Middleware;
using HangfireJobScheduler.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHangfire(config => config.UseMemoryStorage());
builder.Services.AddHangfireServer();

builder.Services.AddSingleton<IEmailService, EmailService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseMiddleware<ErrorHandler>();

app.UseHangfireDashboard("/hangfire");

RecurringJob.AddOrUpdate<IEmailService>(
    "send-daily-emails",
    service => service.SendDailyEmails(),
    Cron.Daily
);

app.Run();
