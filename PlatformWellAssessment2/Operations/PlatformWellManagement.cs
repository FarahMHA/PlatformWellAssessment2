using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using FastExcel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PlatformWellAssessment2.Data;
using PlatformWellAssessment2.DataAccess;
using PlatformWellAssessment2.DtoModels;
using PlatformWellAssessment2.Models;

using static PlatformWellAssessment2.DtoModels.PlatformWellModelDto;
using System.IO;
using OfficeOpenXml;

namespace PlatformWellAssessment2.Operations
{
    public class PlatformWellManagement
    {
        private PlatformWellDal _PlatformWellDal;
        private readonly PlatformWelldbContext _PlatformWelldbContext;
        private readonly IConfiguration config;

        public PlatformWellManagement(PlatformWelldbContext platformWelldbContext, IConfiguration _configuration)
        {

            _PlatformWelldbContext = platformWelldbContext;
            config = _configuration;
        }

        public async Task<List<PlatformDto>> GetAllPlatformWell()
        {
            var platformList = new List<PlatformDto>();

            var platformData = await _PlatformWelldbContext.Platforms.ToListAsync();
            var wellData = await _PlatformWelldbContext.Wells.ToListAsync();

            if (platformData.Any() == true)
            {
                foreach (var platform in platformData)
                {
                    var wellList = new List<WellDto>();
                  
                    foreach (var well in wellData.Where(x => x.PlatformId == platform.Id).ToList())
                    {
                        wellList.Add(new WellDto()
                        {
                            Id = well.Id,
                            PlatformId = well.PlatformId,
                            UniqueName = well.UniqueName,
                            Latitude = well.Latitude,
                            Longitude = well.Longitude,
                            CreatedAt = well.CreatedAt,
                            UpdatedAt = well.UpdatedAt,
                        });
                    }

                    platformList.Add(new PlatformDto()
                    {
                        Id = platform.Id,
                        UniqueName = platform.UniqueName,
                        Latitude = platform.Latitude,
                        Longitude = platform.Longitude,
                        CreatedAt = platform.CreatedAt,
                        UpdatedAt = platform.UpdatedAt,
                        WellList = wellList
                });
                    
                }
                
            }

            return platformList;
        }


        public async Task<List<PlatformDto>> GetSpecificPlatform(int PId)
        {
            var platformList = new List<PlatformDto>();

            var platformData = await _PlatformWelldbContext.Platforms.Where(x=> x.Id == PId).ToListAsync();
            var wellData = await _PlatformWelldbContext.Wells.ToListAsync();

            if (platformData.Any() == true)
            {
                foreach (var platform in platformData)
                {
                    var wellList = new List<WellDto>();

                    foreach (var well in wellData.Where(x => x.PlatformId == platform.Id).ToList())
                    {
                        wellList.Add(new WellDto()
                        {
                            Id = well.Id,
                            PlatformId = well.PlatformId,
                            UniqueName = well.UniqueName,
                            Latitude = well.Latitude,
                            Longitude = well.Longitude,
                            CreatedAt = well.CreatedAt,
                            UpdatedAt = well.UpdatedAt,
                        });
                    }

                    platformList.Add(new PlatformDto()
                    {
                        Id = platform.Id,
                        UniqueName = platform.UniqueName,
                        Latitude = platform.Latitude,
                        Longitude = platform.Longitude,
                        CreatedAt = platform.CreatedAt,
                        UpdatedAt = platform.UpdatedAt,
                        WellList = wellList
                    });

                }

            }

            return platformList;
        }

        public async Task<MitDataTemplateDto> DownloadPlatformExcelData()
        {

            MitDataTemplateDto mitData;
            var generatedExcelFileName = "";
            var dsExcel = await GetExcelDownloadData();
            var inputFilePath = new FileInfo(Path.Combine(config["PlatformDownloadExcelTargetLocation"], "temp-platform.xlsx"));
            if (inputFilePath.Exists)
            {
                inputFilePath.Delete();
            }

            inputFilePath.Refresh();
            var templateFilePath = new FileInfo(config["PlatformDownloadExcelTemplate"]);
            using (var fastExcel = new FastExcel.FastExcel(templateFilePath, inputFilePath))
            {

                fastExcel.Write(dsExcel, "sheet1", true);
                generatedExcelFileName = fastExcel.ExcelFile.FullName;
            }

            if (File.Exists(generatedExcelFileName))
            {
                var content = File.ReadAllBytes(generatedExcelFileName);
                mitData = new MitDataTemplateDto
                {
                    TemplateName = "sheet1",
                    MitTemplate = content
                };

                return mitData;
            }

            return null;
        }


        public async Task<List<PlatformDataExcel>> GetExcelDownloadData()
        {
            try
            {
                var platformList = new List<PlatformDto>();

                var platformData = await _PlatformWelldbContext.Platforms.ToListAsync();

                List<PlatformDataExcel> finalList = new List<PlatformDataExcel>();

                foreach (var platform in platformData)
                {
                    
                    finalList.Add(new PlatformDataExcel()
                    {
                        Id = platform.Id,
                        UniqueName = platform.UniqueName,
                        Latitude = platform.Latitude,
                        Longitude = platform.Longitude,
                        CreatedAtExcel = platform.CreatedAt.ToString(),
                        UpdatedAtExcel = platform.UpdatedAt.ToString(),
                    });
                }

                return finalList.OrderBy(x => x.Id).ToList();
            }
            catch (Exception)
            {

                throw;
            }

        }

        public List<PlatformDto> ReadExcelData(string filePath)
        {
            var dataList = new List<PlatformDto>();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var worksheet = package.Workbook.Worksheets[0]; // Assuming data is in the first worksheet
               


                var totalRows = worksheet.Dimension.Rows;
                var totalColumns = worksheet.Dimension.Columns;

                for (int row = 2; row <= totalRows; row++)
                {
                    //for (int col = 1; col <= totalColumns; col++)
                    //{
                    //    var cellValue = worksheet.Cells[row, col].Value?.ToString();

                    //    if (!string.IsNullOrEmpty(cellValue))
                    //    {
                    //        dataList.Add(cellValue);
                    //    }
                    //}
                    var idEx = worksheet.Cells[row, 1].Value.ToString().Trim();
                    var id = int.Parse(idEx);
                    var UniqueNameEx = worksheet.Cells[row, 2].Value.ToString().Trim();
                    var LatitudeEx = worksheet.Cells[row, 3].Value.ToString().Trim();
                    var Latitude = Convert.ToDecimal(LatitudeEx);
                    var LongitudeEx = worksheet.Cells[row, 4].Value.ToString().Trim();
                    var Longitude = Convert.ToDecimal(LongitudeEx);
                    var CreatedAtEx = worksheet.Cells[row, 5].Value.ToString().Trim();
                    var CreatedAt = DateTime.Parse(CreatedAtEx);
                    var UpdatedAtEx = worksheet.Cells[row, 6].Value.ToString().Trim();
                    var UpdatedAt = DateTime.Parse(UpdatedAtEx);

                    dataList.Add(new PlatformDto
                    {
                        Id = id,
                        UniqueName = worksheet.Cells[row, 2].Value.ToString().Trim(),
                        Latitude = Latitude,
                        Longitude = Longitude,
                        CreatedAt = CreatedAt,
                        UpdatedAt = UpdatedAt,

                    });
                }
            }

            return dataList;
        }


        public string ExtractExcelData()
        {
            var filePath = "C:\\Users\\Fazli_Amri\\ResoursesTests\\temp-platform.xlsx";
            var data = ReadExcelData(filePath);


            // Use the extracted data as needed
            var platformExcel = new PlatformExcel();

            foreach (var t in data)
            {
                platformExcel.Id = t.Id;
                platformExcel.UniqueName = t.UniqueName;
                platformExcel.Latitude = t.Latitude;
                platformExcel.Longitude = t.Longitude;
                platformExcel.CreatedAt = t.CreatedAt;
                platformExcel.UpdatedAt = t.UpdatedAt;

                _PlatformWelldbContext.PlatformExcel.Add(platformExcel);
                _PlatformWelldbContext.SaveChanges();

            }


            return "sucess";
        }


    }

}
