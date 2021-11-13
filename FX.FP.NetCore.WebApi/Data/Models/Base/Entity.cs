using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FX.FP.NetCore.WebApi.Data.Models.Base
{
    public abstract class Entity :IEntity
    {
        public DateTime CreatedOn { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public string ModifiedBy { get; set; }
    }

}
