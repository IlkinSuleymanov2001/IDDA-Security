using Core.CrossCuttingConcerns.Exceptions;
using Goverment.AuthApi.Services.Extensions;



var builder = WebApplication.CreateBuilder(args);
builder.Services.AddJWTServices(builder.Configuration);
builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.PropertyNameCaseInsensitive = false);



builder.Services.AddRepos(builder.Configuration);
builder.Services.AddServices(builder.Configuration);

builder.Services.AddCors();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.ConfigureCustomExceptionMiddleware();

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
await app.ApplyMigrations();
app.MapControllers();

app.Run();
