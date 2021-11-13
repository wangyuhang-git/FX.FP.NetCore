using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FX.FP.NetCore.WebApi.Infrastructure.Services
{
    public interface ICurrentUserService
    {
        public string GetUserName();

        public string GetId();
    }
}
