using BennerMicroOndas.Controllers;
using BennerMicroOndas.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BennerMicroOndas
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // Este m�todo � chamado em tempo de execu��o. Use este m�todo para adicionar servi�os ao cont�iner.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IProgramaAquecimentoService, ProgramaAquecimentoService>();
            services.AddControllersWithViews();
            services.AddScoped<MicroondasController>(); // Adiciona MicroondasController como um servi�o
            services.AddHttpContextAccessor(); // Adiciona o servi�o IHttpContextAccessor
            services.AddSession(); // Adiciona suporte a sess�es
        }

        // Este m�todo � chamado em tempo de execu��o. Use este m�todo para configurar o pipeline de solicita��o HTTP.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseSession(); // Adiciona o uso de sess�o

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "CadastroPrograma",
                    pattern: "CadastroPrograma",
                    defaults: new { controller = "CadastrarProgamasController", action = "CadastroPrograma" }
                );

                endpoints.MapControllerRoute(
                    name: "microondas",
                    pattern: "microondas/iniciaraquecimento",
                    defaults: new { controller = "Microondas", action = "IniciarAquecimento" }
                );

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
