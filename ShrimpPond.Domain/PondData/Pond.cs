using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using ShrimpPond.Domain.Environments;
using ShrimpPond.Domain.Food;
using ShrimpPond.Domain.Medicine;
using ShrimpPond.Domain.Farm;

namespace ShrimpPond.Domain.PondData
{
    public class Pond
    {
        //Khởi tạo ao
        public string PondId { get; set; } = string.Empty;
        public string PondName { get; set; } = string.Empty;
        public float Deep { get; set; } 
        public float Diameter { get; set; }       
        public string PondTypeId { get; set; } = string.Empty;
        public PondType PondType { get; set; }

        //Kích hoạt ao (Thêm SizeShrimp)
        [EnumDataType(typeof(EPondStatus))]
        public EPondStatus Status { get; set; }
        public string OriginPondId { get; set; } = string.Empty;

        public string SeedName { get; set; } = string.Empty;
        public string SeedId { get; set; } = string.Empty;

        [Column(TypeName = "VARBINARY(MAX)")]
        public List<Certificate>? Certificates { get; set; } 
        public float AmountShrimp { get; set; }
        //public string UnitAmountShrimp { get; set; } = string.Empty ;
        public DateTime StartDate { get; set; }

        //Trong quá trình nuôi 
        public ICollection<SizeShrimp>? SizeShrimps { get; set; } 
        public ICollection<LossShrimp>? LossShrimps { get; set; }
        public ICollection<FoodFeeding>? FoodFeedings { get; set; }
        public ICollection<MedicineFeeding>? MedicineFeedings { get; set; }
        public ICollection<Environments.EnvironmentStatus>? EnvironmentStatus { get; set; }


        //Thu hoạch
        public ICollection<Harvest.Harvest>? Harvests { get; set; }
        public int FarmId { get; set; }
    }
}
