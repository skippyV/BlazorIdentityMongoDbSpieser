using AspNetCore.Identity.MongoDbCore.Extensions;
using AspNetCore.Identity.MongoDbCore.Infrastructure;
using IdentityWithSpieser.Components;
using IdentityWithSpieser.Models;
using Microsoft.AspNetCore.Identity;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;

/*
 * https://github.com/alexandre-spieser/AspNetCore.Identity.MongoDbCore
 * https://www.yogihosting.com/aspnet-core-identity-mongodb/
 */
namespace IdentityWithSpieser
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            MongoDbSettings mongoJsonConfigSettings = builder.Configuration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>()!;

            var mongoDbIdentityConfiguration = new MongoDbIdentityConfiguration
            {
                MongoDbSettings = new MongoDbSettings
                {
                    ConnectionString = mongoJsonConfigSettings.ConnectionString,
                    DatabaseName = mongoJsonConfigSettings.DatabaseName
                },
                IdentityOptionsAction = options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 4;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;

                    // Lockout settings
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                    options.Lockout.MaxFailedAccessAttempts = 10;

                    // ApplicationUser settings
                    options.User.RequireUniqueEmail = true;
                    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789@.-_";
                }
            };

            /*
             * Got this error:
             *   InvalidOperationException: 
             *      No converter registered for type 'IdentityWithSpieser.Models.ApplicationRole'
             *   when trying to create a Role from CreateRoleForm.razor.
             *   So added below RegisterClassMap as a hail-mary but it did not work. Same error.
             *   For some reason I had to bind my EditForms to my own Models. Could not bind to
             *   either ApplicationRole or ApplicationUser. Worked when I bound to Models.User and Models.Role.
             */
            //BsonClassMap.RegisterClassMap<ApplicationRole>(cm =>
            //        {
            //            cm.AutoMap();
            //        }
            //    );

            if (mongoDbIdentityConfiguration.IdentityOptionsAction != null)
            {
                builder.Services.Configure(mongoDbIdentityConfiguration.IdentityOptionsAction);
                builder.Services.ConfigureMongoDbIdentity<ApplicationUser, ApplicationRole, ObjectId>(mongoDbIdentityConfiguration)
                    .AddDefaultTokenProviders();
            }



            //builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
            //    .AddMongoDbStores<ApplicationUser, ApplicationRole, ObjectId>
            //    (
            //        mongoJsonConfigSettings.ConnectionString,
            //        mongoJsonConfigSettings.DatabaseName
            //    );

            builder.Services.AddScoped<UserManager<ApplicationUser>>();
            //builder.Services.AddSingleton<UserManager<ApplicationUser>>(); Using a Singleton must be done with a service provider or factory
            // It appears that any framework wrapping Microsoft's Identity package must
            // use Scoped services for Managers - if they inherit from the underlying Identity classes.
            // https://www.stevejgordon.co.uk/reminder-to-take-care-when-registering-dependencies

            builder.Services.AddScoped<RoleManager<ApplicationRole>>();

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
