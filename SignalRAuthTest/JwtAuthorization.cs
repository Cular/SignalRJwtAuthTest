using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SignalRAuthTest
{
    public static class JwtAuthorization
    {
        public static IServiceCollection AddTokenConfiguration(this IServiceCollection services, TokenConfiguration configuration)
        {
            var symmetricKeyAsBase64 = configuration.Key;
            var keyByteArray = Encoding.ASCII.GetBytes(symmetricKeyAsBase64);
            var signingKey = new SymmetricSecurityKey(keyByteArray);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,

                ValidateIssuer = true,
                ValidIssuer = configuration.Issuer,

                ValidateAudience = true,
                ValidAudience = configuration.Audience,

                ValidateLifetime = true,

                ClockSkew = TimeSpan.Zero,

                NameClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"
            };

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = tokenValidationParameters;
                    options.Configuration = new OpenIdConnectConfiguration { TokenEndpoint = configuration.Path };
                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var accessToken = context.Request.Query["access_token"];

                            if (!string.IsNullOrEmpty(accessToken))
                            {
                                context.Token = accessToken;
                            }

                            return Task.CompletedTask;
                        }
                    };
                });

            return services;
        }

        public static string GenerateToken(string uniqueName, TokenConfiguration configuration)
        {
            var token = new JwtSecurityToken(
                issuer: configuration.Issuer,
                audience: configuration.Audience,
                expires: DateTime.UtcNow.Add(configuration.Expiration),
                signingCredentials: GetCredentials(configuration.Key),
                claims: new Claim[]
                {
                    new Claim(ClaimTypes.Name, uniqueName),
                    new Claim(ClaimTypes.NameIdentifier, uniqueName)
                });

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private static SigningCredentials GetCredentials(string asymetricKey)
        {
            var bytes = Encoding.ASCII.GetBytes(asymetricKey);
            var key = new SymmetricSecurityKey(bytes);
            return new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
        }
    }
}
