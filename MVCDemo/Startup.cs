using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using MVCDemo.Models.EFCoreDb;

namespace MVCDemo
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            var connection = "Data Source=HG-D87;Initial Catalog=BlogDB;User ID=sa;Password=Test_1234";
            services.AddDbContext<MovieRateContext>(options => options.UseSqlServer(connection));


            var mappingconfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mappingconfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });

            services.AddCors();
           
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            //.ConfigureApiBehaviorOptions(options =>
            // options.SuppressConsumesConstraintForFormFileParameters = true
            //)
            //.AddJsonOptions(options =>
            //options.SerializerSettings.ContractResolver
            //= new CamelCase
            //)

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseStaticFiles();
            app.UseCookiePolicy();
            //app.UseMvc();
            app.UseCors(builder =>
                builder.WithOrigins("http://localhost:4200")
                .AllowAnyHeader()
                .AllowAnyMethod()
                );
            app.UseMvc(routes =>
            {
                //routes.MapRoute(
                //    name: "MovieReleaseDate",
                //    template: "movies/released/{year}/{month}",
                //    defaults: new { controller = "Movies", action = "ByReleaseDate" },
                //    //new { year = @"\d{4}", month=@"\d{2}"} // year 4 digit and month 2 digit
                //    //new { year = @"2018 | 2019", month = @"\d{2}" } // does not work because of space between |
                //    new { year = @"2018|2019", month = @"\d{2}" }
                //    );

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
