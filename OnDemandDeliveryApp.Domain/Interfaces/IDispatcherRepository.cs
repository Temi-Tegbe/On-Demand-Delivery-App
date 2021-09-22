﻿using OnDemandDeliveryApp.Domain.Entitities;
using OnDemandDeliveryApp.Domain.Entitities.DTOs;
using OnDemandDeliveryApp.Domain.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnDemandDeliveryApp.Domain.Interfaces
{
    public interface IDispatcherRepository : IRepository<Dispatcher>
    {

        public Task AddAsync(DispatcherRegistration dispatcherInfo, ApplicationUser userInfo);
    }
}
