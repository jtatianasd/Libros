using CRUDLibros.Data;
using CRUDLibros.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Net;
using System.Reflection;

namespace CRUDLibros
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
			services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Conexion")));
			services.AddControllers();

			// Documentacion Swagger
			services.AddSwaggerGen(options =>
			{
				options.SwaggerDoc("ApiLibros", new Microsoft.OpenApi.Models.OpenApiInfo()
				{
					Title = "API Libros",
					Version = "1",
					Description = "Backend Libros - Prueba tecnica Nexos - software",
					Contact = new Microsoft.OpenApi.Models.OpenApiContact()
					{
						Email = "jtatianasd@gmail.com",
						Name = "Tatiana Salamanca",

					}
				}); ;
				var archivoXmlComentarios = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
				var rutaApiComentarios = Path.Combine(AppContext.BaseDirectory, archivoXmlComentarios);
				options.IncludeXmlComments(rutaApiComentarios);
			});

			// CORS 
			services.AddCors();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler(builder =>
				{
					builder.Run(async context =>
					{
						context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
						var error = context.Features.Get<IExceptionHandlerFeature>();

						if (error != null)
						{
							context.Response.AddApplicationError(error.Error.Message);
							await context.Response.WriteAsync(error.Error.Message);
						}

					});
				});
			}

			app.UseSwagger();
			app.UseSwaggerUI(options =>
			{
				options.SwaggerEndpoint("/swagger/CRUDLibros/swagger.json", "API Libros");
				options.RoutePrefix = "";
			});

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});

			app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
		}
	}
}
