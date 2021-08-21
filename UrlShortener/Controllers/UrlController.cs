using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.Dtos;
using UrlShortener.Entities;
using UrlShortener.Repository;
using UrlShortener.Services;
namespace UrlShortener.Controllers
{
    [ApiController]
    [Route("UrlGen")]
    public class UrlController:ControllerBase
    {
        private readonly IUrlRepo _repo;
        public readonly IMapper _mapper;
        private readonly IUrlGenerator _urlGen;

        public UrlController(IUrlRepo repo, IMapper mapper, IUrlGenerator urlGen)
        {
            _repo = repo;
            _mapper = mapper;
            _urlGen = urlGen;
        }
        [HttpGet]
       public async Task<ActionResult<IEnumerable<Url>>> GetUrlsAsync()
        {
            var result = await _repo.GetUrlsAsync();
            if(result is null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<Url>, IEnumerable<UrlDto>>(result));
        }
        // Url GetUrl(Guid id);
        [HttpGet("{id}")]
        public async Task<ActionResult<Url>> GetUrlAsync(Guid id)
        {
            var result = await  _repo.GetUrlAsync(id);
            if(result is null)
            {
                return NotFound();
            }
            var dto = _mapper.Map<Url, UrlDto>(result);
            return Ok(dto);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUrlAsync(Guid id)
        {
            var result =  await _repo.GetUrlAsync(id);
            if(result is null)
            {
                return NotFound();
            }
            await _repo.DeleteUrlAsync(id);
            return NoContent();
        }
     
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUrlAsync(Guid id,UpdateUrlDto urlDto)
        {
            var result =  await _repo.GetUrlAsync(id);
            if(result is null)
            {
                return NotFound();
            }
            result.ShortNameUrl = urlDto.ShortNameUrl;
            result.LongNameUrl = urlDto.LongNameUrl;
            await _repo.UpdateUrlAsync(result);
            return NoContent();
        }
        [HttpPost]
        public async Task<ActionResult> CreateUrlAsync(CreateUrlDto urlDto)
        {
            var shortNameUrlResult = _urlGen.getShortName(urlDto.LongNameUrl);
            if(shortNameUrlResult is null)
            {
                return BadRequest();
            }
            //this is where u take the longname and covert into short name
            Url url = new ()
            {
                Id = Guid.NewGuid(),
                ShortNameUrl = shortNameUrlResult,
                LongNameUrl = urlDto.LongNameUrl,
                CreatedDate = DateTime.UtcNow

            };
            await _repo.CreateUrlAsync(url);
            var dto = _mapper.Map<Url, UrlDto>(url);
            return CreatedAtAction(nameof(GetUrlAsync), new { id = dto.Id}, dto);
        }
    }
} 