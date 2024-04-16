using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PicoForum.Components;
using PicoForum.Components.Account;
using PicoForum.Data;
using PicoForum.Models;
using PicoForum.Service;

namespace PicoForum
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            ////Add Authentication
            builder.Services.AddCascadingAuthenticationState();
            builder.Services.AddScoped<IdentityUserAccessor>();
            builder.Services.AddScoped<IdentityRedirectManager>();
            builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

            builder.Services.AddAuthentication(options =>
                {
                    options.DefaultScheme = IdentityConstants.ApplicationScheme;
                    options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
                })
                .AddIdentityCookies();

            //Add Database
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            //Identity Core
            builder.Services.AddIdentityCore<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddSignInManager()
                .AddDefaultTokenProviders();

            ////configure lock out
            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;
            });

            //Configure password complexity
            builder.Services.Configure<IdentityOptions>(options =>
            {
                // Default Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 0;
            });

            //Configure Email.
            builder.Services.Configure<IdentityOptions>(options =>
            {
                // Default SignIn settings.
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
                options.SignIn.RequireConfirmedAccount = false;
            });

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = "/Error";
                options.Cookie.Name = "pico_forum_cookie";
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                options.LoginPath = "/Account/Login";
                options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
                options.SlidingExpiration = true;
            });

            builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();
            string configFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "serverconfig.json");
            Console.WriteLine($"Server Config Path: {configFilePath}");
            builder.Services.AddSingleton(provider => new PFConfig(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "serverconfig.json")));
            //Add update request
            builder.Services.AddSingleton<UpdateRequestService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            // Add additional endpoints required by the Identity /Account Razor components.
            app.MapAdditionalIdentityEndpoints();

            await CreateRoles(app.Services);
            //await AddFakeCategory(app.Services);
            //await AddFakePost(app.Services);
            app.Run();

            async Task CreateRoles(IServiceProvider serviceProvider)
            {
                using var scope = serviceProvider.CreateScope();
                //initializing custom roles
                var RoleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var UserManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                string[] roleNames = { "SuperAdmin", "Admin", "User" };
                IdentityResult roleResult;

                foreach (var roleName in roleNames)
                {
                    var roleExist = await RoleManager.RoleExistsAsync(roleName);
                    // ensure that the role does not exist
                    if (!roleExist)
                    {
                        //create the roles and seed them to the database:
                        roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                    }
                }

                // find the user with the admin email
                var _user = await UserManager.FindByNameAsync(@"admin@picoforum.com");

                // check if the user exists
                if (_user == null)
                {
                    Console.Write(@"Input Password For admin@picoforum.com: ");

                    //Here you could create the super admin who will maintain the web app
                    var RootUser = new ApplicationUser
                    {
                        UserName = @"admin@picoforum.com",
                        Friendlyname = "Admin",
                        Email = @"admin@picoforum.com",
                        EmailConfirmed = true,
                        AvatarUrl = "avatar/default.png"
                    };

                    var validator = new PasswordValidator<ApplicationUser>() { };

                    string adminPassword = Console.ReadLine();
                    Console.Write(@"Confirm Password: ");
                    string adminPassword2 = Console.ReadLine();
                    Console.WriteLine();
                    while (adminPassword != adminPassword2)
                    {
                        Console.Write(@"Password Not Matched, Retry: ");
                        adminPassword = Console.ReadLine();
                        Console.Write(@"Confirm Password: ");
                        adminPassword2 = Console.ReadLine();
                        Console.WriteLine();
                    }

                    var createPowerUser = await UserManager.CreateAsync(RootUser, adminPassword);
                    if (createPowerUser.Succeeded)
                    {
                        //here we tie the new user to the role
                        await UserManager.AddToRoleAsync(RootUser, "SuperAdmin");
                        Console.WriteLine($"Super Admin Account Added.");
                        Console.WriteLine("THIS ACCOUNT CAN NOT BE RECOVERED");
                        Console.WriteLine("SAVE YOUR PASSWORD SAFELY.");
                        Console.WriteLine("****************************************");
                        Console.WriteLine($"Account:{RootUser.Email}");
                        Console.WriteLine($"Password:{adminPassword}");

                        Console.WriteLine("****************************************");
                    }
                    else
                    {
                        Console.WriteLine("Create Admin Fail. Check your password");
                    }
                }
            }
        }

        private static async Task AddFakePost(IServiceProvider serviceProvider)
        {
            var scope = serviceProvider.CreateScope();

            ApplicationDbContext DB = scope.ServiceProvider.GetService<ApplicationDbContext>();
            var UserManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var _user = DB.Users.FirstOrDefault();

            if (DB.Posts.Any()) { return; }

            for (int i = 0; i < 127; i++)
            {
                var post = new PFPost(DB.Sections.FirstOrDefault(), _user, $"This Is Test Post Title {i}.", $"This is a Test Post {i}.");
                DB.Posts.Add(post);
            }

            DB.SaveChanges();
        }

        private static async Task AddFakeCategory(IServiceProvider serviceProvider)
        {
            var scope = serviceProvider.CreateScope();

            ApplicationDbContext DB = scope.ServiceProvider.GetService<ApplicationDbContext>();

            if (DB.Sections.Any())
            {
                return;
            }
            PFSection category = new PFSection()
            {
                Name = "欧美"
            };

            DB.Sections.Add(category);
            category = new PFSection()
            {
                Name = "日韩"
            };

            DB.Sections.Add(category);
            category = new PFSection()
            {
                Name = "国产"
            };

            DB.Sections.Add(category);
            category = new PFSection()
            {
                Name = "3D"
            };

            DB.Sections.Add(category);
            DB.SaveChanges();
        }
    }
}