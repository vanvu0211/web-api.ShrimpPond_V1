using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Domain.Farm
{
    public enum Role
    {
        [Display(Name = "Member")]
        [Description("Member")]
        Member = 0,

        [Display(Name = "Manager")]
        [Description("Manager")]
        Manager = 1,

        [Display(Name = "Admin")]
        [Description("Admin")]
        Admin = 2,
    }
}
