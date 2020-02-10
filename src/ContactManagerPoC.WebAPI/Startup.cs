using System;
using System.Data;
using System.Data.Common;
using System.Reflection;
using ContactManagerPoC.Application;
using ContactManagerPoC.Application.ContactUseCases;
using ContactManagerPoC.Application.ContactUseCases.GetContactById;
using ContactManagerPoC.Application.ContactUseCases.GetContacts;
using ContactManagerPoC.Infrastructure;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MySql.Data.MySqlClient;
using Swashbuckle.AspNetCore.Swagger;

namespace ContactManagerPoC.WebAPI
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
            var connectionString = Configuration["ConnectionStrings:Contacts"];
            services.AddControllers().AddFluentValidation(o =>
                {
                    o.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
                });
            services.AddHealthChecks();
            services.AddMediatR(Assembly.GetExecutingAssembly(), typeof(IUnitOfWork).Assembly);
            services.AddDbContext<ContactContext>(options => options.UseMySql(connectionString));
            services.AddScoped<IUnitOfWork>(p => p.GetService<ContactContext>());
            services.AddScoped<IContactRepository, DomainContactRepository>();
            services.AddScoped<IGetContactByIdRepository, ReadContactRepository>();
            services.AddScoped<IGetContactsRepository, ReadContactRepository>();
            services.AddScoped<IDbConnection>((provider) => new MySqlConnection(connectionString));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ContactManagerAPI", Version = "v1" });
                c.AddFluentValidationRules();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ContactManagerAPI");
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseHealthChecks("/healthchecks");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
