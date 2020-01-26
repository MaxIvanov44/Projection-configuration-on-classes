using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;

namespace Projection_configuration_on_classes
{
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public List<string> Languages { get; set; }
        public Company Company { get; set; }
    }
    public class Company
    {
        public string Title { get; set; }
        public string Country { get; set; }
    }
    public class Startup
    {
        public Startup()
        {
            var builderr = new ConfigurationBuilder()
                        .AddXmlFile("person2.xml");
            AppConfiguration = builderr.Build();


            var builder = new ConfigurationBuilder().AddJsonFile("person.json");
            AC = builder.Build();
        }
        public IConfiguration AC { get; set; }
        public IConfiguration AppConfiguration { get; set; }
        public void ConfigureServices(IServiceCollection services)
        {
           
        }

        public void Configure(IApplicationBuilder app)
        {

            var tom = new Person();
            AC.Bind(tom);

            app.Run(async (context) =>
            {
                Company companyy = AppConfiguration.GetSection("company").Get<Company>();

                string name = $"<p>Name: {tom.Name}</p>";
                string age = $"<p>Age: {tom.Age}</p>";
                string company = $"<p>Company: {tom.Company.Title}</p>";
                string langs = "<p>Languages:</p><ul>";
                foreach (var lang in tom.Languages)
                {
                    langs += $"<li><p>{lang}</p></li>";
                }
                langs += "</ul>";

                await context.Response.WriteAsync($"{name}{age}{company}{langs}");
                await context.Response.WriteAsync($"<br><p>Title: {companyy.Title}</p><p>Country: {companyy.Country}</p>");
            });
        }
    }
}
