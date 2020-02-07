using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using ContactManagerPoC.Application;
using ContactManagerPoC.Application.ContactUseCases.AddContact;
using ContactManagerPoC.Application.ContactUseCases.GetActiveContacts;
using ContactManagerPoC.Application.ContactUseCases.GetContactById;
using ContactManagerPoC.Application.ContactUseCases.UpdateContactAddress;
using ContactManagerPoC.Application.ContactUseCases.UpdateContactNames;
using ContactManagerPoC.Application.ContactUsesCases;
using ContactManagerPoC.Infrastructure;
using ContactManagerPoC.WebAPI.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
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
            services.AddControllers().AddFluentValidation(o =>
                {
                    o.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
                });
            services.AddHealthChecks()
                .AddSqlServer(Configuration["ConnectionStrings:Contacts"]);
            services.AddMediatR(Assembly.GetExecutingAssembly(), typeof(IUnitOfWork).Assembly);
            services.AddDbContext<ContactContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:Contacts"]));
            services.AddScoped<IUnitOfWork>(p => p.GetService<ContactContext>());
            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddScoped<IGetContactByIdRepository, ContactRepository>();
            services.AddScoped<IGetActiveContactsRepository, ContactRepository>();
            services.AddScoped<IContactRepository, ContactRepository>();

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
