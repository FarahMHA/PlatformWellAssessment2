using System;
using System.Collections.Generic;

namespace PlatformWellAssessment2.Models
{
    public partial class Platform
    {
        public int Id { get; set; }
        public string UniqueName { get; set; } = null!;
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
