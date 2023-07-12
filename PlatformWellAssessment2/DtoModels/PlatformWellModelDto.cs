using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformWellAssessment2.DtoModels
{
    public class PlatformWellModelDto
    {
        public class PlatformDto
        {
            public int? Id { get; set; }
            public string UniqueName { get; set; }
            public decimal Latitude { get; set; }
            public decimal Longitude { get; set; }
            public DateTime? CreatedAt { get; set; }
            public DateTime? UpdatedAt { get; set; }
            public List<WellDto> WellList { get; set; } = new List<WellDto>();
        }

        public class WellDto
        {
            public int Id { get; set; }
            public int PlatformId { get; set; }
            public string UniqueName { get; set; }
            public decimal Latitude { get; set; }
            public decimal Longitude { get; set; }
            public DateTime? CreatedAt { get; set; }
            public DateTime? UpdatedAt { get; set; }

        }

    }
}
