/*
 * @Author: Kylan Frittelli ST10438112
 * @Since[Updated]: 22/03/25
 * @Function: Factory for the ApplicationDbContext
 */

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

//----------------------------namespace-----------------//
namespace EventManagerMVC.Data
{
    //--ApplicationDbContextFactory class-----------------//
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        //CreateDbContext method-----------------//
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder() //new instance of ConfigurationBuilder
                .SetBasePath(Directory.GetCurrentDirectory()) //sets the base path
                .AddJsonFile("appsettings.json") //adds the appsettings.json file 
                .Build(); 

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            //new instance of DbContextOptionsBuilder

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            //gets the connection string from the appsettings.json file

            optionsBuilder.UseSqlServer(connectionString);
            //uses the connection string

            return new ApplicationDbContext(optionsBuilder.Options);
            //returns a new instance of ApplicationDbContext
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
 * OpenAI, 2025. ChatGPT. [online] Available at: https://openai.com/chatgpt/ [Accessed 20 March 2025].
 * Github Inc., 2025. GitHub Copilot. [online] Available at: https://github.com [Accessed 14 March 2025].
 * Varsity College, 2025. INSY6112 Module Manual. Cape Town: The Independent Institute of Education.
 */