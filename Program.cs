using ChgCharityJamPrototype.HostedService;
using ChgCharityJamPrototype.Hubs;
using ChgCharityJamPrototype.Models;
using SDCS.Engine;

namespace ChgCharityJamPrototype
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddControllersWithViews();
			builder.Services.AddRazorPages();
			builder.Services.AddSignalR();

			// Add dependency injection classes
			builder.Services.AddSingleton(BackendModel.Instance);

			var engine = new Engine();
			var game = new Game(engine);

			builder.Services.AddSingleton(engine);
			builder.Services.AddSingleton(game);
			builder.Services.AddHostedService<GameEngineHostedService>();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Board/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();
			app.MapRazorPages();
			app.MapHub<CommunicationHub>("/communicationHub");

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Board}/{action=Index}/{id?}");

			app.Run();
		}
	}
}
