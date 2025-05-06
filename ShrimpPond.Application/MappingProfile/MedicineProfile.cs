using AutoMapper;
using ShrimpPond.Application.Feature.Medicine.Queries.GetAllMedicine;
using ShrimpPond.Domain.Medicine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.MappingProfile
{
    public class MedicineProfile: Profile
    {
        public MedicineProfile() { 
        CreateMap<Medicine,GetAllMedicineDTO>();
        }
    }
}
