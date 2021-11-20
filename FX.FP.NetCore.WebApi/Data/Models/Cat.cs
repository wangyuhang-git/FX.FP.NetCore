using FX.FP.NetCore.WebApi.Data.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace FX.FP.NetCore.WebApi.Data.Models
{
    public class Cat : DeletableEntity
    {
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [MaxLength(2000)]
        public string ImageUrl { get; set; }

        [Required]
        public string UserId { get; set; }

        public User User { get; set; }

    }
}
