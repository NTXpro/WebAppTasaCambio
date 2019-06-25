using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using WebAppTasaCambio.Jobs;

namespace WebAppTasaCambio
{
    public class JobScheduler
    {
        public static void Start()
        {
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();

            IJobDetail JobPrimeroSunat = JobBuilder.Create<TasaCambioPrimeraActJob>().Build();
            IJobDetail JobSegundoSunat = JobBuilder.Create<TasaCambioSegundaActJob>().Build();
            int porDefecto;
            string intervaloMinutos = ConfigurationManager.AppSettings["IntervaloMinutosSunat"];
            if (!int.TryParse(intervaloMinutos, out porDefecto))
            {
                porDefecto = 1;
            }


            ITrigger trigger1 = TriggerBuilder.Create()

                .WithIdentity("trigger1", "group1")
                .WithSimpleSchedule(x => x
                    .WithIntervalInMinutes(porDefecto)
                    .RepeatForever())
                .Build();



            ITrigger trigger2 = TriggerBuilder.Create()

              .WithIdentity("trigger2", "group1")
              .WithSimpleSchedule(x => x
                  .WithIntervalInMinutes(porDefecto)
                  .RepeatForever())
              .Build();


            scheduler.ScheduleJob(JobPrimeroSunat, trigger1);
            scheduler.ScheduleJob(JobSegundoSunat, trigger2);
        }
    }

    internal interface IMyJob : IJob
    {
    }
}