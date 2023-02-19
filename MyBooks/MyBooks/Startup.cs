using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MyBooks.Data;
using MyBooks.Data.Services;

namespace MyBooks
{
	public class Startup
	{
		public string ConnectionStrings { get; set; }
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
			ConnectionStrings = Configuration.GetConnectionString("DFConnectionString");
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{

			services.AddControllers();

			//configure DbContext with SQL 
			services.AddDbContext<AppDbContext>(options => options.UseSqlServer(ConnectionStrings));

			//Configure our services
			services.AddTransient<BooksService>();
			services.AddTransient<AuthorsService>();
			services.AddTransient<PublishersService>();
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "MyBooks", Version = "v1" });
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyBooks v1"));
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});

			//for seeding DATA
			//AppDbInitializer.Seed(app);
		}
	}
}
