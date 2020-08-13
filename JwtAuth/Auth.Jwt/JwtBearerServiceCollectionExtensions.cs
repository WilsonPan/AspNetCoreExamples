using System;
using JwtAuth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class JwtBearerServiceCollectionExtensions
    {

        public static AuthenticationBuilder AddJwtBearer(this IServiceCollection services, string securityKey)
        {
            return services.AddJwtBearer(options =>
            {
                options.SecurityKey = securityKey;
            });
        }

        public static AuthenticationBuilder AddJwtBearer(this IServiceCollection services, Action<JwtOptions> configureOptions)
        {
            if (configureOptions == null) throw new ArgumentNullException(nameof(configureOptions));

            var jwtOptions = new JwtOptions()
            {
                Issuer = "Jwt Authentication",
                Audience = "Wilson Pan Web Api",
            };
            // set customs optoins
            configureOptions(jwtOptions);

            //update Options 
            services.PostConfigure<JwtOptions>(options =>
            {
                options.Issuer = jwtOptions.Issuer;
                options.Audience = jwtOptions.Audience;
                options.SecurityKey = jwtOptions.SecurityKey;
            });

            return services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                           .AddJwtBearer(options =>
                           {
                               options.TokenValidationParameters = new TokenValidationParameters()
                               {
                                   ValidIssuer = jwtOptions.Issuer,
                                   ValidAudience = jwtOptions.Audience,
                                   ValidateIssuer = true,
                                   ValidateLifetime = true,
                                   ValidateIssuerSigningKey = true,
                                   IssuerSigningKey = jwtOptions.SymmetricSecurityKey
                               };
                           });

        }
    }
}