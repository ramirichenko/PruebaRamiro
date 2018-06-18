﻿using Api.Models;
using Api.Utils;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Api.Jobs
{
    public class ExpirationJob : IJob
    {
        Task IJob.Execute(IJobExecutionContext context)
        {
            Task.Run(() => ExpirationProcess());
            return null;
        }



        public void ExpirationProcess()
        {
            List<InventoryItem> expiredItems = new List<InventoryItem>();

            expiredItems = new InventoryItem().GetExpired();

            new Notifier().ExpirationNotify(expiredItems);

        }
    }
}