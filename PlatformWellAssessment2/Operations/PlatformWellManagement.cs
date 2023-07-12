using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PlatformWellAssessment2.Data;
using PlatformWellAssessment2.DataAccess;
using PlatformWellAssessment2.Models;
using static PlatformWellAssessment2.DtoModels.PlatformWellModelDto;

namespace PlatformWellAssessment2.Operations
{
    public class PlatformWellManagement
    {
        private PlatformWellDal _PlatformWellDal;
        //private readonly PlatformWelldbContext _context;
        private readonly PlatformWelldbContext _PlatformWelldbContext;

        public PlatformWellManagement(PlatformWelldbContext platformWelldbContext)
        {

            _PlatformWelldbContext = platformWelldbContext;
           // _configuration = configuration;
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


    }

}
