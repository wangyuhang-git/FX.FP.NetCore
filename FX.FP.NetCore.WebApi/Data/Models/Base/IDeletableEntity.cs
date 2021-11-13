using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FX.FP.NetCore.WebApi.Data.Models.Base
{
    public interface IDeletableEntity
    {
        DateTime? DeletedOn { get; set; }

        string DeletedBy { get; set; }

        bool IsDeleted { get; set; }
    }
}
