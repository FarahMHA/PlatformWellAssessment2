using Microsoft.AspNetCore.Mvc;
using PlatformWellAssessment2.DtoModels;
using PlatformWellAssessment2.Operations;
using static PlatformWellAssessment2.DtoModels.PlatformWellModelDto;

namespace PlatformWellAssessment2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlatformWellController : ControllerBase
    {
        private readonly PlatformWellManagement _platformWellManagement;


        public PlatformWellController(PlatformWellManagement platformWellManagement)
        {
            _platformWellManagement = platformWellManagement;
        }

        [HttpGet(Name = "GetAllPlatformWell")]
        public async Task<ActionResult<PlatformDto>> GetAllPlatformWell()
        {
            var result = await _platformWellManagement.GetAllPlatformWell();
            return Ok(result);
        }

        [HttpGet("GetSpecificPlatformWell")]
        public async Task<ActionResult<PlatformDto>> GetSpecificPlatform(int id)
        {
            var result = await _platformWellManagement.GetSpecificPlatform(id);
            return Ok(result);
        }
    }
}