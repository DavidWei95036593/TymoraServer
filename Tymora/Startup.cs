using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tymora.Models;
using Tymora.Services;

namespace Tymora {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddDbContext<TymoraContext>(options =>options.UseMySQL(Configuration.GetConnectionString("TymoraDatabase")));
            services.AddMvc();
            services.AddScoped<IRuleServices, RuleServices>();
            services.AddCors(o => o.AddPolicy("TymoraCors", builder => {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseCors(builder => builder.WithOrigins("htpp://localhost"));
            app.UseMvc();
        }
    }
}