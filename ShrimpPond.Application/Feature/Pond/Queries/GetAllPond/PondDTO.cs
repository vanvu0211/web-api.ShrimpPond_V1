using ShrimpPond.Domain.PondData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Pond.Queries.GetAllPond
{
    public class PondDTO
    {
        public string pondId { get; set; } = string.Empty;
        public string pondName { get; set; } = string.Empty;
        public float deep { get; set; }
        public float diameter { get; set; }
        public string pondTypeName { get; set; } = string.Empty;
        public string pondTypeId { get; set; } = string.Empty;

        //Kích hoạt ao (Thêm SizeShrimp)
        [EnumDataType(typeof(EPondStatus))]
        public EPondStatus status { get; set; }
        public string? originPondId { get; set; } = string.Empty;
        public string seedId { get; set; } = string.Empty;
        [Column(TypeName = "VARBINARY(MAX)")]
        public float amountShrimp { get; set; }
        public DateTime startDate { get; set; }

    }
}
