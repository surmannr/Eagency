using Eagency.BLL.Config.Mapper;
using Eagency.BLL.Services;
using Eagency.BLL.Services.Interfaces;
using Eagency.Dal;
using Eagency.Dal.Entities;
using Eagency.Web.Server.Mail;
using Eagency.Web.Shared.Exceptions;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Linq;
using System.Security.Claims;

namespace Eagency.Web.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<EagencyDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<EagencyDbContext>()
                .AddDefaultTokenProviders()
                .AddClaimsPrincipalFactory<MyUserClaimsPrincipalFactory>();

            services.AddAutoMapper(typeof(MapProfile));

            services.AddScoped<IClaimsTransformation, MyClaimTransformation>();

            services.AddIdentityServer()
                .AddApiAuthorization<User, EagencyDbContext>(opt =>
                {
                    opt.IdentityResources["openid"].UserClaims.Add("AllName");
                    opt.IdentityResources["openid"].UserClaims.Add(ClaimTypes.Name);
                    opt.IdentityResources["openid"].UserClaims.Add(ClaimTypes.Email);
                    opt.IdentityResources["openid"].UserClaims.Add(ClaimTypes.NameIdentifier);
                    opt.IdentityResources["openid"].UserClaims.Add(ClaimTypes.Role);

                });

            services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = "/access-denied";
                options.LoginPath = "/login";
                options.LogoutPath = "/logout";
            });

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.Lax;
            });

            services.AddAuthentication()
                .AddFacebook(facebookOptions =>
                {
                    facebookOptions.AppId = Configuration["Authentication:Facebook:AppId"];
                    facebookOptions.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
                })
                .AddCookie(p => p.SlidingExpiration = true)
                .AddGoogle(options =>
                {
                    IConfigurationSection googleAuthNSection =
                        Configuration.GetSection("Authentication:Google");

                    options.ClientId = googleAuthNSection["ClientId"];
                    options.ClientSecret = googleAuthNSection["ClientSecret"];
                });

            services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));
            services.AddTransient<IEmailSender, EmailSender>();

            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Eagency API",
                    Description = ".NET Szoftverfejlesztés 2021",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Surmann Roland",
                        Email = "surmannroland@gmail.com"
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = new Uri("https://example.com/license"),
                    }
                });
                c.EnableAnnotations();
                // Set the comments path for the Swagger JSON and UI.
                //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                //c.IncludeXmlComments(xmlPath);
            });

           

            services.AddScoped<ICommentService, CommentService>()
                    .AddScoped<IPropertyService, PropertyService>()
                    .AddScoped<IContractService, ContractService>()
                    .AddScoped<IUserService, UserService>();
      
            services.AddAuthentication()
                .AddIdentityServerJwt();

            services.AddProblemDetails(ConfigureProblemDetails).AddControllers()
                .AddJsonOptions(x => x.JsonSerializerOptions.IgnoreNullValues = true);
     
            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AgentPolicy", policy => policy.RequireClaim(ClaimTypes.Role, "Agent"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseProblemDetails();

            app.UseIdentityServer();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors("CorsPolicy");

            app.UseSwagger();
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Eagency");
                c.RoutePrefix = "swagger";
            });
        
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
        }
          private void ConfigureProblemDetails(ProblemDetailsOptions options)
          {
              // DbNullException -> NotFound : Ha nem találja meg az adott entitást, akkor NotFound-dal tér vissza.
              options.MapToStatusCode<DbNullException>(StatusCodes.Status404NotFound);
              // InvalidQueryParamsException -> BadRequest : Ha a kötelezõ paraméterek nincsenek kitöltve (azaz null), akkor BadRequest-tel tér vissza.
              options.MapToStatusCode<InvalidQueryParamsException>(StatusCodes.Status400BadRequest);

              options.MapToStatusCode<Exception>(StatusCodes.Status500InternalServerError);
        } 
    }
}
