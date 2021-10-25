using Autofac;
using FX.FP.NetCore.Business;
using FX.FP.NetCore.Interface;
using FX.FP.NetCore.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace FX.FP.NetCore.WebApi
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
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FX.FP.NetCore.WebApi", Version = "v1" });
            });

            //ע��˲ʱ���񣬷���
            //services.AddTransient(typeof(IBlogPost<>), typeof(BlogPostBusiness<>));
            //ע��˲ʱ������ͨ
            //services.AddTransient<Class>();
        }

        /// <summary>
        /// ע��autofac����
        /// </summary>
        /// <param name="builder"></param>
        public void ConfigureContainer(ContainerBuilder builder)
        {
            //�Է��������ע��
            builder.RegisterGeneric(typeof(BlogPostBusiness<>)).As(typeof(IBlogPost<>)).InstancePerDependency();

            //�Բ��Ƿ��͵������ע��
            builder.RegisterType<Class>();

            /*
            //�������Щ������ڵ����Ĺ�����,�����ɵĳ���ΪFX.FP.NetCore.Business���Ϳ���ʹ��
            //Assembly.Load("FX.FP.NetCore.Business")�����Ӧ�ĳ��򼯡���
            //������е��ļ���һ������̨�����C/SӦ����ʹ�ã�������ͨ��Assembly.GetExecutingAssembly();��ֱ�ӻ����Ӧ�ĳ��򼯡�
            Assembly dataAccess = Assembly.Load("FX.FP.NetCore.Businesse");
            builder.RegisterAssemblyTypes(dataAccess)
                    .Where(t => typeof(IBlogPost<>).IsAssignableFrom(t) && t.Name.EndsWith("Business"));
            //RegisterAssemblyTypes������ʵ��IBlogPost�ӿڲ���Business��β���඼ע���ˣ��﷨�ǳ��ļ򵥡�
            */
        }

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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
