
using FX.FP.NetCore.Business;
using FX.FP.NetCore.Interface;
using FX.FP.NetCore.Service;
using FX.FP.NetCore.WebApi.Data;
using FX.FP.NetCore.WebApi.Data.Models;
using FX.FP.NetCore.WebApi.Features.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FX.FP.NetCore.WebApi.Infrastructure.Services;

namespace FX.FP.NetCore.WebApi
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FX.FP.NetCore.WebApi", Version = "v1" });
            });
           
            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins, builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
            });

            var applicationSettingsConfiguration = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(applicationSettingsConfiguration);

            services.AddDbContext<MyIdentityDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("MsSqlConnection")));

            services.AddIdentity<User, IdentityRole>(options =>
             {
                 options.Password.RequiredLength = 6;
                 options.Password.RequireDigit = false;
                 options.Password.RequireLowercase = false;
                 options.Password.RequireNonAlphanumeric = false;
                 options.Password.RequireUppercase = false;
             }).AddEntityFrameworkStores<MyIdentityDbContext>();

            var key = Encoding.ASCII.GetBytes(applicationSettingsConfiguration.Get<AppSettings>().Secret);
            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            //services.AddTransient<IIdentityService, IdentityService>();


            //ע��˲ʱ���񣬷���
            //services.AddTransient(typeof(IBlogPost<>), typeof(BlogPostBusiness<>));
            //ע��˲ʱ������ͨ
            //services.AddTransient<Class>();

            services.AddTransient(typeof(IBlogPost<>), typeof(BlogPostBusiness<>));

            services.AddScoped<ICurrentUserService, CurrentUserService>();

            services.AddTransient<IIdentityService, IdentityService>();

        }


        ///// <summary>
        ///// ע��autofac����
        ///// </summary>
        ///// <param name="builder"></param>
        //public void ConfigureContainer(ContainerBuilder builder)
        //{
        //    //InstancePerDependency ˲ʱģʽ         AddTransient˲ʱģʽ
        //    //SingleInstance ����ģʽ                AddSingleton����ģʽ
        //    //InstancePerLifetimeScope ��������ģʽ  AddScoped��������ģʽ

        //    //�Է��������ע��
        //    builder.RegisterGeneric(typeof(BlogPostBusiness<>)).As(typeof(IBlogPost<>)).InstancePerDependency();

        //    //�Բ��Ƿ��͵������ע��
        //    builder.RegisterType<Class>();

        //    //.AddScoped<ICurrentUserService, CurrentUserService>()
        //    builder.RegisterType<CurrentUserService>().As<ICurrentUserService>().InstancePerLifetimeScope();

        //    builder.RegisterType<IdentityService>().As<IIdentityService>();

        //    /*
        //    //�������Щ������ڵ����Ĺ�����,�����ɵĳ���ΪFX.FP.NetCore.Business���Ϳ���ʹ��
        //    //Assembly.Load("FX.FP.NetCore.Business")�����Ӧ�ĳ��򼯡���
        //    //������е��ļ���һ������̨�����C/SӦ����ʹ�ã�������ͨ��Assembly.GetExecutingAssembly();��ֱ�ӻ����Ӧ�ĳ��򼯡�
        //    Assembly dataAccess = Assembly.Load("FX.FP.NetCore.Businesse");
        //    builder.RegisterAssemblyTypes(dataAccess)
        //            .Where(t => typeof(IBlogPost<>).IsAssignableFrom(t) && t.Name.EndsWith("Business"));
        //    //RegisterAssemblyTypes������ʵ��IBlogPost�ӿڲ���Business��β���඼ע���ˣ��﷨�ǳ��ļ򵥡�
        //    */
        //}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FX.FP.NetCore.WebApi v1"));
            }

            app.UseRouting();

            app.UseCors(option => option
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.ApplicationServices.CreateScope()
                .ServiceProvider.GetService<MyIdentityDbContext>()
                .Database.Migrate();
        }
    }
}
