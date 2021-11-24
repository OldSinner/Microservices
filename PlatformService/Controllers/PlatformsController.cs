using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Extensions;
using PlatformService.Models;
using Serilog;

namespace PlatformService.Controllers
{
    public class PlatformsController : BaseApiController
    {
        private readonly IPlatformRepo _repository;
        private readonly IMapper _mapper;

        public PlatformsController(IPlatformRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDTO>> GetPlatforms()
        {
            var platformItems = _repository.GetAllPlatfrom();
            return Ok(_mapper.Map<IEnumerable<PlatformReadDTO>>(platformItems));
        }
        [HttpGet("{id}", Name= "GetPlatformById")]
        public ActionResult<PlatformReadDTO> GetPlatformById(string id)
        {
            var platformItems = _repository.GetPlatformById(id);
            if (platformItems != null)
            {
                return Ok(_mapper.Map<PlatformReadDTO>(platformItems));
            }
            return NotFound();
        }
        [HttpPost]
        public ActionResult<PlatformReadDTO> CreatePlatform(PlatfromCreateDTO platform)
        {
            var platformModel = _mapper.Map<Platform>(platform);
            _repository.CreatePlatform(platformModel);
            _repository.SaveChanges();
            Log.Information(platformModel.IdInstance.ToString());
            var platformRead = _mapper.Map<PlatformReadDTO>(platformModel);
        
           return CreatedAtRoute(nameof(GetPlatformById),new {Id = platformRead.IdInstance}, platformRead);

        }

    }
}