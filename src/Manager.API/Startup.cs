using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EscNet.IoC.Cryptography;
using EscNet.IoC.Hashers;
using Isopoh.Cryptography.Argon2;
using Manager.API.Token;
using Manager.API.ViewModels;
using Manager.API.ViewModels.ClaS;
using Manager.API.ViewModels.Teachers;
using Manager.Domain.Entities;
using Manager.Identity.Data;
using Manager.Infra.Context;
using Manager.Infra.Interfaces;
using Manager.Infra.Repositiries;
using Manager.Infra.Repositories;
using Manager.Services.DTO;
using Manager.Services.Interface;
using Manager.Services.Interfaces;
using Manager.Services.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Manager.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region Jwt

            //services.Configure<JwtConfig>(Configuration.GetSection("JwtConfig"));

            var secretKey = Encoding.ASCII.GetBytes(Configuration["Jwt:Key"]);

            //var key = Encoding.ASCII.GetBytes(Configuration["JwtConfig:Secret"]);

            var tokenValidationParams = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                RequireExpirationTime = false,
                ClockSkew = TimeSpan.Zero
            };

            services.AddSingleton(tokenValidationParams);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.SaveToken = true;
                x.TokenValidationParameters = tokenValidationParams;
            });


            #endregion

            #region AutoMapper

            var AutoMapperConfig = new MapperConfiguration(cfg =>
            {
                // cfg.CreateMap<User, UserDTO>().ReverseMap();
                // cfg.CreateMap<CreateUserViewModel, UserDTO>().ReverseMap();
                // cfg.CreateMap<UpdateUserViewModel, UserDTO>().ReverseMap();
                cfg.CreateMap<Student, StudentDTO>().ReverseMap();
                cfg.CreateMap<CreateStudentViewModel, StudentDTO>().ReverseMap();
                cfg.CreateMap<UpdateStudentViewModel, StudentDTO>().ReverseMap();

                cfg.CreateMap<Teacher, TeacherDTO>().ReverseMap();
                cfg.CreateMap<CreateTeacherViewModel, TeacherDTO>().ReverseMap();
                cfg.CreateMap<UpdateTeacherViewModel, TeacherDTO>().ReverseMap();

                cfg.CreateMap<Class, ClassDTO>().ReverseMap();
                cfg.CreateMap<CreateClassViewModel, ClassDTO>().ReverseMap();
                cfg.CreateMap<UpdateClassViewModel, ClassDTO>().ReverseMap();

                cfg.CreateMap<Boletim, BoletimDTO>().ReverseMap();
                cfg.CreateMap<CreateBoletimViewModel, BoletimDTO>().ReverseMap();
                
            });

            services.AddSingleton(AutoMapperConfig.CreateMapper());

            #endregion

            #region DI

            services.AddSingleton(d => Configuration);
            services.AddDbContext<ManagerContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:USER_MANAGER"]), ServiceLifetime.Transient);
            services.AddDbContext<IdentityDataContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:USER_MANAGER"]), ServiceLifetime.Transient);
            services.AddScoped<ITokenGenerator, TokenGenerator>();

            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IStudentRepository, StudentRepository>();

            services.AddScoped<ITeacherRepository, TeacherRepository>();
            services.AddScoped<ITeacherService, TeacherService>();

            services.AddScoped<IClassRepository, ClassRepository>();
            services.AddScoped<IClassService, ClassService>();

            services.AddScoped<IBoletimRepository, BoletimRepository>();
            services.AddScoped<IBoletimService, BoletimService>();

            services.AddDefaultIdentity<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<IdentityDataContext>()
                .AddDefaultTokenProviders();

            #endregion

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Manager.API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please use Bearer <TOKEN>",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                 {  new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
                    }
                });
                var xmlApiPath = System.IO.Path.Combine(AppContext.BaseDirectory, $"{typeof(Startup).Assembly.GetName().Name}.xml");
            });


            #region Hash

            var config = new Argon2Config
            {
                Type = Argon2Type.DataIndependentAddressing,
                Version = Argon2Version.Nineteen,
                Threads = Environment.ProcessorCount,
                TimeCost = int.Parse(Configuration["Hash:TimeCost"]),
                MemoryCost = int.Parse(Configuration["Hash:MemoryCost"]),
                Lanes = int.Parse(Configuration["Hash:Lanes"]),
                HashLength = int.Parse(Configuration["Hash:HashLength"]),
                Salt = Encoding.UTF8.GetBytes(Configuration["Hash:Salt"])
            };

            services.AddArgon2IdHasher(config);

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Manager.API v1"));
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