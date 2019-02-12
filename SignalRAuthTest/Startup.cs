using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SignalRAuthTest.Hub;

namespace SignalRAuthTest
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddSingleton<TokenConfiguration>();
            services.AddTokenConfiguration(services.BuildServiceProvider().GetRequiredService<TokenConfiguration>());
            services.AddSingleton<IUserIdProvider, NameUserIdProvider>();

            services.AddCors();
            services.AddSignalR()
                .AddHubOptions<ChatHub>(options => options.EnableDetailedErrors = true);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder =>
            {
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
                builder.WithOrigins("http://localhost:5010", "http://localhost");
                builder.AllowCredentials();
            });

            var wsOptions = new WebSocketOptions();
            wsOptions.AllowedOrigins.Add("http://localhost:5010");
            wsOptions.AllowedOrigins.Add("http://localhost");
            app.UseWebSockets(wsOptions);

            app.UseSignalR(hrb => hrb.MapHub<ChatHub>("/hubs/chat"));
            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
