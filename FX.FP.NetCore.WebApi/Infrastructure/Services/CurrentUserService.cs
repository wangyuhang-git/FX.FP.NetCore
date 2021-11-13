using FX.FP.NetCore.WebApi.Infrastructure.Extensions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FX.FP.NetCore.WebApi.Infrastructure.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly ClaimsPrincipal user;
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            this.user = httpContextAccessor.HttpContext?.User;
        }

        public string GetId()
            => this.user?.GetId();

        public string GetUserName()
            => this.user?.Identity?.Name;
    }
}
