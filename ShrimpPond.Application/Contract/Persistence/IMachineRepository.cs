﻿using ShrimpPond.Application.Contract.Persistence.Genenric;
using ShrimpPond.Domain.Machine;
using ShrimpPond.Domain.PondData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Contract.Persistence
{
    public interface IMachineRepository : IRepositoryBaseAsync<Machine, int>
    {

    }
}
