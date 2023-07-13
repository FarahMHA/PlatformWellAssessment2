namespace PlatformWellAssessment2.DtoModels
{
    public class PlatformDataExcel
    {
        public int? Id { get; set; }
        public string UniqueName { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }

        public string CreatedAtExcel { get; set; }
        public string UpdatedAtExcel { get; set; }


    }
}
