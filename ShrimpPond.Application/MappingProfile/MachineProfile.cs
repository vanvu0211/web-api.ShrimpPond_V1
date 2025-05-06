using AutoMapper;
using ShrimpPond.Application.Feature.InformationPond.Queries.GetInformationPond;
using ShrimpPond.Application.Feature.Machine.Queries.GetALlMachine;
using ShrimpPond.Domain.Machine;
using ShrimpPond.Domain.PondData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.MappingProfile
{
    public class MachineProfile: Profile
    {
        public MachineProfile()
        {
            CreateMap<Domain.Machine.Machine, GetALlMachineDTO>();
        }
    }
}
