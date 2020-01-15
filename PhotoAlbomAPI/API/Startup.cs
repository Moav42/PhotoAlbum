using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using BLL.Extensions;
using AutoMapper;
using BLL.Maping;
using API.Extensions;

namespace API
{
    public class Startup
    {
        private const string SecretKey = "The Answer to Life the Universe and Everything is ...";
        private readonly SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext(Configuration);

            services.AddControllers();

            services.AddBllServices();

            services.AddJWT(_signingKey, Configuration);

            services.AddJWTAuthorization();

            services.AddIdentity();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder => { builder .AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();});
            });

            services.AddSwagger();

            services.AddAutoMapper(c => c.AddProfile<MapingProfiles>(), typeof(Startup));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCustomExceptionMiddleware();

            app.UseDefaultFiles();

            app.UseStaticFiles();

            app.UseCors("AllowAll");

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
