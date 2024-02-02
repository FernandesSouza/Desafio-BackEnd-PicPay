using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PicPay.Application.Interfaces;
using PicPay.Application.Mappings;
using PicPay.Application.Services;
using PicPay.Domain.Interfaces;
using PicPay.Infra.Data.Data;
using PicPay.Infra.Data.Repository;




namespace PicPay.Infra.Ioc
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfraestrutura(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<BancoContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("BancoPICPAY"), b => b.MigrationsAssembly(typeof(BancoContext).Assembly.FullName)));

            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddAutoMapper(typeof(ClassMappers));
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<ILojistaRepository, LojistaRepository>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ITransferenciaService, TransferenciaService>();
            services.AddFluentValidationAutoValidation();
        
            return services;
        }

    }
}
