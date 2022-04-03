using Microsoft.EntityFrameworkCore;
using MediatR;
using APISupport.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<SupportContext>(
    options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("DBSupport"))
);

builder.Services.AddMediatR(typeof(Program));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();