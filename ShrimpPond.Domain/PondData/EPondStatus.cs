using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Domain.PondData
{
    public enum EPondStatus
    {
        [Display(Name = "InActive")]
        [Description("InActive")]
        InActive = 0,

        [Display(Name = "Active")]
        [Description("Active")]
        Active = 1,
    }
}
