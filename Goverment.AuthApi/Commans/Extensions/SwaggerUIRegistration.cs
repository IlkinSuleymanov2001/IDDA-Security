﻿using Microsoft.OpenApi.Models;

namespace Goverment.AuthApi.Commans.Extensions
{
    public static class SwaggerUIRegistration
    {

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {

            services.AddSwaggerGen(option =>
            {

                option.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Goverment.Security",
                    Version = "v1"
                });
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
            });
            return services;


        }
    }
}
