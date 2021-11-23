using FX.FP.NetCore.WebApi.Data.Models;
using FX.FP.NetCore.WebApi.Features.Identity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FX.FP.NetCore.WebApi.Features.Identity
{
    public class IdentityController : ApiController
    {
        private readonly UserManager<User> userManager;
        private readonly IIdentityService identity;
        private readonly AppSettings appSettings;

        public IdentityController(
            UserManager<User> userManager,
            IIdentityService identity,
            IOptions<AppSettings> appSettings)
        {
            this.userManager = userManager;
            this.identity = identity;
            this.appSettings = appSettings.Value;
        }

        [HttpPost]
        [AllowAnonymous]//用于跳过AuthorizeAttribute认证
        [Route(nameof(Register))]
        public async Task<ActionResult> Register(RegisterRequestModel model)
        {
            var user = new User
            {
                Email = model.Email,
                UserName = model.UserName
            };

            var result = await this.userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            return Ok();
        }

        [Route(nameof(Login))]
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<LoginResponseModel>> Login(LoginRequestModel model)
        {
            var user = await this.userManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                return Unauthorized();
            }
            var passwordValid = await this.userManager.CheckPasswordAsync(user, model.Password);
            if (!passwordValid)
            {
                return Unauthorized();
            }
            var token = this.identity.GenerateJwtToken(
                user.Id,
                user.UserName,
                this.appSettings.Secret);

            return new LoginResponseModel { Token = token };
        }

        [HttpGet]
        [Route(nameof(GetListAsync))]
        [AllowAnonymous]
        public async Task<Page<User>> GetListAsync()
        {
            List<User> list = await this.userManager.Users.ToListAsync();

            return new Page<User>(list, list.Count, 0, 5);
        }
    }

    public class Page<T> where T : class, new()
    {
        public Page(List<T> list, int totleCount, int pageIndex, int pageSize)
        {
            this.Data = list;
            this.PageIndex = pageIndex;
            this.PageSize = pageSize;
            this.TotalCount = totleCount;
            this.TotalPages = (int)Math.Ceiling(totleCount / (double)pageSize);
        }

        public List<T> Data { get; set; }

        public int TotalCount { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int TotalPages { get; set; }
    }
}
