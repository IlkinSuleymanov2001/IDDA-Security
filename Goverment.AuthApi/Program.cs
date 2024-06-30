using Core.CrossCuttingConcerns.Exceptions;
using Goverment.AuthApi.Repositories;
using Goverment.AuthApi.Services.Extensions;
using Goverment.AuthApi.Services.Filters.Transaction;



var builder = WebApplication.CreateBuilder(args);



builder.Services.AddJWTServices(builder.Configuration);
builder.Services.AddControllers()
    .AddJsonOptions(x => x.JsonSerializerOptions.PropertyNameCaseInsensitive = false);



builder.Services.AddRepos(builder.Configuration);
builder.Services.AddServices(builder.Configuration);
//builder.Services.AddScoped<TransactionAttribute>();

builder.Services.AddCors();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

//app.ConfigureCustomExceptionMiddleware();

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors(builder =>
{
    builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
});
app.UseAuthentication();
app.UseAuthorization();
//await app.ApplyMigrations();
app.MapControllers();

app.Run();
