using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using SystemMenu.Model;

namespace SystemMenu
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
            services.AddControllersWithViews();
            #region 配置数据库
            services.AddDbContext<SystemMenuDBContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("default")
                    //,读取配置文件中的连接字符串                                             
                    //,b=>b.UseRowNumberForPaging()//已失效，解决"'OFFSET' 附近有语法错误",失效后的解决方案，1、数据库升级到2012及以上，2、降ef core的版本
                    );
                //options.UseLazyLoadingProxies();//延迟加载
            });
            #endregion
            services.AddSession();

            services.AddControllersWithViews().AddJsonOptions(
                options =>
                {
                    //options.JsonSerializerOptions.PropertyNamingPolicy = null;//更正json返回首字母小写
                    options.JsonSerializerOptions.Converters.Add(new DatetimeJsonConverter());
                });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSession();
            // 配置获取客户端的IP地址以及端口号中间件
            app.UseForwardedHeaders(
                new ForwardedHeadersOptions {
                    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto });
            app.UseHttpsRedirection();
            app.UseStaticFiles();



            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }

    /// <summary>
    /// .net core 返回时间格式转化
    /// </summary>
    public class DatetimeJsonConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateTime.Parse(reader.GetString());
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            if (value.Hour == 0 && value.Minute == 0 && value.Second == 0)
                writer.WriteStringValue(value.ToString("yyyy-MM-dd"));
            else
                writer.WriteStringValue(value.ToString("yyyy-MM-dd HH:mm:ss"));
        }
    }

}



