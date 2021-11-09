using System.ComponentModel.DataAnnotations;


namespace FX.FP.NetCore.WebApi.Features.Identity.Models
{
    public class LoginRequestModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
