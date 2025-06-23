/*
 * @Author: Kylan Frittelli ST10438112
 * @Since[Updated]: 04/05/25
 * @Function: Program.cs for CLDV6211 POE Part 1
 *   Program.cs is the entry point for the application and is responsible for configuring the application and starting the server.
 */

using EventManagerMVC.Data;
using EventManagerMVC.Services;
using Microsoft.EntityFrameworkCore;

//----------------------------namespace-----------------//
namespace EventManagerMVC
{
    //--Program class-----------------//
    public class Program
    {
        //Main method-----------------//
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args); //new instance of WebApplicationBuilder

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            //gets the connection string from the appsettings.json file

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            //adds the ApplicationDbContext to the services using SQL Server

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddScoped<IBlobStorageService, BlobStorageService>();

            var app = builder.Build(); //builds the application

            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                db.Database.Migrate();
            }

            //configures the HTTP request pipeline
            if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            //uses files from dependencies
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            //--end of middleware-----------------//

            //maps the default route
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run(); //runs the application itself
        }
        //----------------------------//
    }
    //--------------------------------//
}
//END OF FILE>>>>>>>>>>>>>>>>>>>>>>>>>>>

/* Refrences:
 * Huawei Technologies, 2023. Cloud Computing Technologies. Hangzhou: Posts & Telecom Press.
 * Mrzyglód, K., 2022. Azure for Developers. 2nd ed. Birmingham: Packt Publishing.
 * Microsoft Corporation, 2022. The Developer’s Guide to Azure. Redmond: Microsoft Press.
 * OpenAI, 2025. ChatGPT. [online] Available at: https://openai.com/chatgpt/ [Accessed 04 May 2025].
 * Github Inc., 2025. GitHub Copilot. [online] Available at: https://github.com [Accessed 04 May 2025].
 * Varsity College, 2025. INSY6112 Module Manual. Cape Town: The Independent Institute of Education.
 */