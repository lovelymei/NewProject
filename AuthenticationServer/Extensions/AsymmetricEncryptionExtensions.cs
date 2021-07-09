using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using NewProject.AuthenticationServer.Certificates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewProject.AuthenticationServer.Extensions
{
    public static class AsymmetricEncryptionExtensions
    {
        public static IServiceCollection AddAsymmetricAuthentication(this IServiceCollection services)
        {
            var issuerSigningCertificate = new SigningIssuerCertificate();
            RsaSecurityKey issuerSingningKey = issuerSigningCertificate.GetIssuerSigningKey();

            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateIssuerSigningKey = false,
                    IssuerSigningKey = issuerSingningKey,
                    LifetimeValidator = LifetimeValidator
                };
            });
            return services;
        }

        private static bool LifetimeValidator(DateTime? notBefore,
            DateTime? expires, 
            SecurityToken securityToken,
            TokenValidationParameters tokenValidationParameters)
        {
            return expires != null && expires > DateTime.UtcNow;
        }
    }
}
