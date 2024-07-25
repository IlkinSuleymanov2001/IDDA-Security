using Core.CrossCuttingConcerns.Exceptions;
using Goverment.AuthApi.Commans.Extensions;
using Goverment.AuthApi.Commans.Log.ColumnWriter;
using Goverment.AuthApi.Repositories;
using Microsoft.AspNetCore.HttpLogging;
using Newtonsoft.Json;
using NpgsqlTypes;
using Serilog;
using Serilog.Sinks.PostgreSQL;
using System.Net;
using Goverment.Core.CrossCuttingConcers.Resposne.Error;
using Castle.DynamicProxy;
using Goverment.AuthApi.Commans.AOP.Intercept;
using Autofac.Core;
using System.Reflection;
using System.Security.Claims;
using Core.Security.Extensions;
using Core.Security.JWT;
using Serilog.Context;
using Autofac.Extensions.DependencyInjection;
using Autofac;
using Goverment.AuthApi.Commans.AOP.Autofac;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddJwtServices(builder.Configuration);
builder.Services.AddControllers()
    .AddJsonOptions(x => x.JsonSerializerOptions.PropertyNameCaseInsensitive = false);


builder.Services.AddRepos(builder.Configuration);
builder.Services.AddServices(builder.Configuration);

// configure autofac container 
/*builder.Host
    .UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>((builder) =>
    {
        builder.RegisterModule(new AutofacBusinessModule());
    });
*/

builder.Services.AddCors();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();
//builder.Services.AddLogConfig(builder.Configuration,builder.Host);


var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();

/*
app.Use(async (context, next) =>
{
    await next(context);
    if (context.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
        await context.Response.WriteAsync(new ErrorResponse().ToString());
});*/
app.ConfigureCustomExceptionMiddleware();
//app.UseSerilogRequestLogging();
/*app.UseHttpLogging();*/

app.UseHttpsRedirection();
app.UseRouting();
/*app.UseCors(builder =>
{
    builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
});*/
app.UseAuthentication();
app.UseAuthorization();
//await app.ApplyMigrations();
//app.SetUsernameSerilogContext();
app.MapControllers();
app.Run();
