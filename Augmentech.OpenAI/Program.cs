using Azure.AI.OpenAI;
using Azure;

namespace Augmentech.OpenAI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var configuration = builder.Configuration;
            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddSingleton<OpenAIClient>(c =>
            {
                return new OpenAIClient(new Uri(configuration["AzureOpenAI:ImageModelEndpoint"]), new AzureKeyCredential(configuration["AzureOpenAI:ImageModelKey"]));
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {

                app.UseCors(x => x
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .SetIsOriginAllowed(origin => true) // allow any origin
                                                        //.WithOrigins("https://localhost:44351")); // Allow only this origin can also have multiple origins separated with comma
                    .AllowCredentials()); // allow credentials
            }


            // Configure the HTTP request pipeline.
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
