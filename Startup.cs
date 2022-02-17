using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Security.Cryptography.Xml;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StudentPlatformAPI.Data;
using StudentPlatformAPI.Map;
using StudentPlatformAPI.Models.Auth;
using StudentPlatformAPI.Services;
using StudentPlatformAPI.Settings;
using Swashbuckle.AspNetCore.Filters;

namespace StudentPlatformAPI
{
    public class Startup
    {
        readonly string _myAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // Add cors policy
            services.AddCors(options =>
            {
                options.AddPolicy(name: _myAllowSpecificOrigins,
                    builder =>
                    {
                        builder.WithOrigins(Configuration.GetSection("CorsAllowedOrigins")["Ui"])
                            .AllowAnyMethod().AllowAnyHeader();
                    });
            });
            
            // Add Database
            services.AddDbContext<StudentPlatformContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.Configure<JwtSettings>(Configuration.GetSection("Jwt"));



            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<StudentPlatformContext>()
                .AddDefaultTokenProviders();

            // Get from appsettings
            var jwtSettings = Configuration.GetSection("Jwt").Get<JwtSettings>();

            // Add auth
            services
                .AddAuthorization()
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = jwtSettings.Issuer,
                        ValidAudience = jwtSettings.Issuer,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
                        ClockSkew = TimeSpan.Zero
                    };
                });


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "StudentPlatformAPI", Version = "v1" });


                //Enable auth for swagger
                c.AddSecurityDefinition("Auth",
                    new OpenApiSecurityScheme
                    {
                        Name = "Authorization",
                        Type = SecuritySchemeType.ApiKey,
                        Description = "JWT containing userid guid claim",
                        In = ParameterLocation.Header,
                        BearerFormat = "JWT",
                        Scheme = "Bearer"
                    });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme()
                        {
                            Reference = new OpenApiReference() {Id = "Auth", Type = ReferenceType.SecurityScheme}
                        },
                        new List<string>()
                    }
                });

                // Add Examples value
                c.ExampleFilters();

                // Add XML support
                var filePath = Path.Combine(System.AppContext.BaseDirectory, "StudentPlatformAPI.xml");
                c.IncludeXmlComments(filePath);
            });

            //Enable Swagger example values
            services.AddSwaggerExamplesFromAssemblies(Assembly.GetEntryAssembly());

            // Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            // Add dependency injection
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ITaskService, TaskService>();
            services.AddScoped<ICalendarEventService, CalendarEventService>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<User> userManager, StudentPlatformContext context)
        {
            // Seed database
            StudentPlatformSeeder.SeedData(userManager, context);

            // Adding cors
            app.UseCors(_myAllowSpecificOrigins);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "StudentPlatformAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


        }
    }
}
