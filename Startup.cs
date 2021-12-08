using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Quiz_back.repositories;
using Quiz_back.repositories.interfaces;
using Quiz_back.services;
using Quiz_back.services.interfaces;
using System.Linq;

namespace Quiz_back
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        private string Headers = 
            "Authorization,Accept,Content-Type,Accept-Encoding,Accept-Language,Connection,Cookie," +
            "Host,Origin,Referer,Sec-Fetch-Dest,Sec-Fetch-Mode,Sec-Fetch-Site,User-Agent";
        private string Methods = "GET,POST,PUT,PATCH,DELETE,HEAD,OPTIONS";
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(opt =>
            {
                opt.AddPolicy("GlobalCors",
                builder =>
                {
                    builder.WithHeaders(Headers.Trim().Split(",").ToArray());
                    builder.WithExposedHeaders("Set-Cookie");
                    builder.WithOrigins("http://localhost:3000", "http://localhost:5000", "https://localhost:5001", "http://localhost:55780", "http://localhost:44370");
                    builder.WithMethods(Methods.Trim().Split(",").ToArray());
                    builder.AllowCredentials();
                });
            });
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Quiz_back", Version = "v1" });
            });
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddTransient<IQuizRepository, QuizRepository>();
            services.AddTransient<IQuizService, QuizService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Quiz_back v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
