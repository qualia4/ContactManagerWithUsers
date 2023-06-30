using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ContactManagerWithUsers
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureServices((hostContext, services) =>
                    {
                        services.AddSession(options =>
                        {
                            options.Cookie.IsEssential = true; // Make the session cookie essential
                            options.IdleTimeout = TimeSpan.FromMinutes(30); // Set session timeout duration
                        });

                        services.AddRazorPages();
                    })
                    .Configure((hostContext, app) =>
                    {
                        var env = hostContext.HostingEnvironment;

                        if (!env.IsDevelopment())
                        {
                            app.UseExceptionHandler("/Error");
                            app.UseHsts();
                        }

                        app.UseStaticFiles();
                        app.UseRouting();
                        app.UseAuthorization();

                        app.UseSession(); // Add this line to enable session middleware

                        app.UseEndpoints(endpoints =>
                        {
                            endpoints.MapRazorPages();
                        });
                    });
                });
    }
}

