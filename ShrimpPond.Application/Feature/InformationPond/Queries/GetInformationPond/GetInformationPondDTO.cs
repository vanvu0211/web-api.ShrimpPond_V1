using ShrimpPond.Domain.Environments;
using ShrimpPond.Domain.PondData.Harvest;
using ShrimpPond.Domain.PondData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShrimpPond.Application.Feature.Update.Queries.GetFoodFeeding;
using ShrimpPond.Application.Feature.Update.Queries.GetMedicineFeeding;
using ShrimpPond.Application.Feature.Update.Queries.GetSizeUpdate;
using ShrimpPond.Application.Feature.Update.Queries.GetLossUpdate;

namespace ShrimpPond.Application.Feature.InformationPond.Queries.GetInformationPond
{
    public class GetInformationPondDTO
    {
        public string pondId { get; set; } = string.Empty;
        public string pondName { get; set; } = string.Empty;
        public float deep { get; set; }
        public float diameter { get; set; }
        public string pondTypeId { get; set; } = string.Empty;
        public string pondTypeName { get; set; } = string.Empty;
        public string status { get; set; } = string.Empty;
        public string originPondName { get; set; } = string.Empty;
        public string seedName { get; set; } = string.Empty;
        public string seedId { get; set; } = string.Empty;
        public DateTime startDate { get; set; }
        public List<Certificate>? certificates { get; set; } 
        public List<GetFoodFeedingDTO>? feedingFoods { get; set; }
        public List<GetMedicineFeedingDTO>? feedingMedicines { get; set; }
        public List<GetSizeUpdateDTO>? sizeShrimps { get; set; }
        public List<GetLossUpdateDTO>? lossShrimps { get; set; }
        public List<HarvestDTO>? harvests { get; set; }
    }
}
