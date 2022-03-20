using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Reports.Core.Models;
using Reports.DAL;
using Reports.Interfaces;
using Reports.Services.Repositories;
using Reports.Services.Repositories.Employees;
using Reports.Services.Repositories.Goals;
using Reports.Services.Repositories.Reports;

namespace Reports
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
            services.AddControllers().AddNewtonsoftJson(opts =>
            {
                opts.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            services.AddEntityFrameworkSqlite().AddDbContext<ReportContext>();
            services.AddScoped<IRepository<Employee>, EmployeeRepository>();
            services.AddScoped<IPagedFilter<Employee>, EmployeeRepository>();
            services.AddScoped<IRepository<Goal>, GoalRepository>();
            services.AddScoped<IPagedFilter<Goal>, GoalRepository>();
            services.AddScoped<IRepository<DailyReport>, DailyReportRepository>();
            services.AddScoped<IPagedFilter<DailyReport>, DailyReportRepository>();
            services.AddScoped<IRepository<WeeklyReport>, WeeklyReportRepository>();
            services.AddScoped<IPagedFilter<WeeklyReport>, WeeklyReportRepository>();
            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Reports v1"));
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}