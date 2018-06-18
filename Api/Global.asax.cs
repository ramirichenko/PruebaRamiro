using Api.Jobs;
using Api.Utils;
using Newtonsoft.Json;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            ConfigureQuartz();
        }

        private void ConfigureQuartz()
        {

            //Crearemos job para expirar todos los productos que caduquen cada dia a las 12 de la noche.
            try
            {
                
                IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler().Result;

               
                scheduler.Start();

                IJobDetail job = JobBuilder.Create<ExpirationJob>()
                   .WithIdentity("ExpirationJob", "InventoryJobs")
                   .Build();


                DateTime now = DateTime.UtcNow;
                //Hora cuando empezará la job
                DateTime startDate = new DateTime(now.Year,now.Month,now.Day + 1,0,0,0);               

              
                ITrigger trigger = TriggerBuilder.Create()
                    .WithIdentity("ExpirationTrigger", "InventoryTriggers")
                    .StartAt(startDate)
                    //.StartNow()
                    .WithSimpleSchedule(x => x
                        .WithIntervalInHours(24)
                        .RepeatForever())
                    .Build();

             
                scheduler.ScheduleJob(job, trigger);
            }
            catch (Exception e)
            {
                Logger.Error("Fallo al iniciar quartz. || Exception: " + JsonConvert.SerializeObject(e), "api/v1/output");
            }
        }
    }
}
