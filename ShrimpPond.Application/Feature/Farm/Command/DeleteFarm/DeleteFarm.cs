﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Farm.Command.DeleteFarm
{
    public class DeleteFarm: IRequest<int>
    {
        public int FarmId { get; set; } 
        public string Email { get; set; } = string.Empty;
    }
}
