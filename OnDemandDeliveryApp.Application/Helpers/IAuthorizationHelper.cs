using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnDemandDeliveryApp.Application.Helpers
{
    public interface IAuthorizationHelper
    {

        public long GetCurrentCustomerId();
        public long GetCurrentDispatcherId();


        public Task<bool> CurrentUserHasRoleAsync(string roleName);
    }
    }
