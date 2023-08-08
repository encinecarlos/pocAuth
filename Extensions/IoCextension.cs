using System.Reflection;
using System.Text;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Poc.Authentication.Services;

namespace Poc.Authentication.Extensions
{
    public static class IoCExtension
    {
        public static void AddIoC(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(opt => {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x => {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration.GetSection("ApiKey").Value)),
                    ValidateAudience = false,
                    ValidateIssuer = false,
                };
            });

            services.AddAuthorization();

            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddTransient<TokenService>();
            
        }                
    }
}