using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PR.API.Data;
using PR.API.Models.Domain;
using PR.API.Models.DTO;
using PR.API.Repositories;

namespace PR.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {   
        private readonly NZDbContext  dbContext;
        private readonly IRegionRepository regionRepository;

        public RegionsController(NZDbContext dbContext , IRegionRepository regionRepository)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;     
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {   
            //Get Data From Database - Domain models
           var regions = await regionRepository.GetAllAsync();

            //Map Domain Models to DTOs
            var regionsDto = new List<RegionDTO>();
            foreach (var region in regions)
            {
                regionsDto.Add(new RegionDTO()
                {
                    Id = region.Id,
                    Code = region.Code,
                    Name = region.Name,
                    RegionImageUrl = region.RegionImageUrl
                });

            }
            return Ok(regions); 
        }
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetRegionById([FromRoute] Guid id)

        {
            var region = await  dbContext.Regions.FindAsync(id);
            if (region == null) return NotFound();
            //if not null map region domain mode to regionDTO
            return Ok(region);

        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto regionDto)
        {
            // Map or convert DTO to Domain Model
            var regionDomainModel = new Region
            {
                Code = regionDto.Code,
                Name = regionDto.Name,
                RegionImageUrl = regionDto.RegionImageUrl
            };
            //Use Domain Model to create Region
            await dbContext.Regions.AddAsync(regionDomainModel);
            await dbContext.SaveChangesAsync();

            //map domain model back to dto 
            var NewRegionDto = new RegionDTO
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };
            return CreatedAtAction(nameof(GetRegionById), new { id = NewRegionDto.Id }, NewRegionDto);
        }


        //Udate region
        // Put   https://localhost:portnumber/api/regions

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            var regionDomainModel = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (regionDomainModel == null) return NotFound();
            //Map Dto to Domain Model
            regionDomainModel.Code = updateRegionRequestDto.Code;
            regionDomainModel.Name = updateRegionRequestDto.Name;
            regionDomainModel.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;

            await dbContext.SaveChangesAsync();

            //Convert Domain To DTO
            var regionDto = new RegionDTO
            {
                Id = regionDomainModel.Id,
                Name = regionDomainModel.Name,
                Code = regionDomainModel.Code,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };
            return Ok(regionDto);

        }
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id )
        {
            var regionDomainModel = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (regionDomainModel == null) return NotFound();
            dbContext.Regions.Remove(regionDomainModel);
            await dbContext.SaveChangesAsync();
            var regionDto = new RegionDTO
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name =  regionDomainModel.Name,
                RegionImageUrl=regionDomainModel.RegionImageUrl
                
            };
            return Ok(regionDto);
        }
    }
}
