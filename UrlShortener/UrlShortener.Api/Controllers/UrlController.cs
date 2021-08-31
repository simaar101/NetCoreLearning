using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.Api.Dtos;
using UrlShortener.Api.Models;
using UrlShortener.Api.Repository;
using UrlShortener.Api.Logic;


namespace UrlShortener.Api.Controllers
{
    [ApiController]
    [Route("Url")]
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
       public async Task<ActionResult<IEnumerable<UrlDto>>> GetUrlsAsync()
        {
            var result = await _repo.GetUrlsAsync();
            if(result is null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<Url>, IEnumerable<UrlDto>>(result));
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<UrlDto>> GetUrlAsync(Guid id)
        {
            var result = await  _repo.GetUrlAsync(id);
            if(result is null)
            {
                return NotFound();
            }
         
            return _mapper.Map<UrlDto>(result);
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
        public async Task<ActionResult<UrlDto>> CreateUrlAsync(CreateUrlDto urlDto)
        {
            var hashResult = _urlGen.GetHashFunction(urlDto.LongNameUrl);
            if(hashResult is null)
            {
                return BadRequest(urlDto);
            }
            string curUrl = "https://localhost:5001/UrlGen/";
            //this is where u take the longname and covert into short name
            Url url = new ()
            {
                Id = Guid.NewGuid(),
                ShortNameUrl = curUrl+hashResult,
                LongNameUrl = urlDto.LongNameUrl,
                HashFunction = hashResult,
                CreatedDate = DateTime.UtcNow

            };
            await _repo.CreateUrlAsync(url);
            var dto = _mapper.Map<Url, UrlDto>(url);
            return CreatedAtAction(nameof(GetUrlAsync), new { id = dto.Id}, dto);
        }

        [Route("[action]/{hash}")]
        [HttpGet]
        public async Task<ActionResult> GetLongUrlByHash(string hash)
        {
            var resultUrl = await _repo.GetUrlByHashAsync(hash);
            if(resultUrl is null)
            {
                return NotFound();
            }
            return Redirect(resultUrl.LongNameUrl);
        }
    }
} 