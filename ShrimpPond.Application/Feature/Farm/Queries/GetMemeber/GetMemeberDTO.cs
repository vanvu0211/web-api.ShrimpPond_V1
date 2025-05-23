﻿using MediatR;
using ShrimpPond.Application.Feature.Farm.Queries.GetAllFarm;
using ShrimpPond.Domain.Farm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Farm.Queries.GetMemeber
{
    public class GetMemeberDTO
    {
        public string Email { get; set; } = string.Empty;
        public Role Role { get; set; }
    }
}
