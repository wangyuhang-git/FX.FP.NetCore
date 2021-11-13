using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FX.FP.NetCore.WebApi.Data.Models
{
    public class Profile
    {
        [Key]
        [Required]
        public string UserId { get; set; }

        [MaxLength(40)]
        public string Name { get; set; }

        public string MainPhotoUrl { get; set; }

        public string WebSite { get; set; }

        [MaxLength(150)]
        public string Biography { get; set; }

        public Gender Gender { get; set; }

        public bool IsPrivate { get; set; }
    }
}
