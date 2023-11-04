using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;

namespace LibraryAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);//builder object will take all method in webApplication to build the appliction


            //start building the appliction.
            //And Add services to the container.
            builder.Services.AddControllers();//to run all classes that inherited from controller clas

             //builder.Services.AddDbContext<ApplicationContext>();//to use my database for(get,update,put,delete).old way

            builder.Services.AddDbContext<ApplicationContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DBContext")));//add service to use database.but the configuration of database will be in (appsettins.json).new way

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


            //Authentication structure
            string txt = "AllowAll";
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(txt,
                builder =>
                {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyMethod();
                    builder.AllowAnyHeader();
                });
            });
          


            //execute swagger to test my application on virual site
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            //JWT Validate
            builder.Services.AddAuthentication(options => { options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters  //here will validate or check the token if it is validate time or validate token
                {
                    ValidateLifetime = true,
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "Salim",
                    ValidAudience = "TRA",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this is my custom Secret key for authentication"))

                };
            });



            //create logger to get all information when users use app and will save the details in file but this is old way
            //Log.Logger = new LoggerConfiguration()
            //                       .MinimumLevel.Information()
            //                       .WriteTo.File("C:\\Users\\TRA\\Desktop\\code-TRA\\LibraryAPI\\logs.txt", rollingInterval: RollingInterval.Day)
            //                       .CreateLogger();


            
            //create Logger service to get all details about using the app that used by customer. bath of file will save in (appsettings.json) file. this is new way
            Log.Logger = new LoggerConfiguration()
                                .ReadFrom.Configuration(builder.Configuration)//builder.Configuration means file of appSettings.json
                                .CreateLogger();


             builder.Host.UseSerilog();//to get full details about using the app in file

            var app = builder.Build();
            //end building appliction


            //start run the builder
            //Also Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();//if program drop in middle, this feare will re-direction the program again

            app.UseSerilogRequestLogging();//run the Logger service
            app.UseCors(txt);
            app.UseAuthentication();//here will execute this authentication in method that taken a authorized tag[Authorize]
            app.UseAuthorization();//here is feature of tag authorize[Authorize] that we will give to methods to be not usable, except by key or token
           
            app.MapControllers();

            app.Run();
            //end run builder
        }
    }
}