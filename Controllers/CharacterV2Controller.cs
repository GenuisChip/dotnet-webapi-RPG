using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using dotnet_rpg.Dtos.Charater;
using dotnet_rpg.Entities;
using dotnet_rpg.Extensions;
using dotnet_rpg.Models;
using dotnet_rpg.Services.CharacterService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace dotnet_rpg.Controllers
{
    [Authorize]
    [ApiController]
    // [Route("api/{v:apiversion}/character")] // if you want to send the version in url
    [Route("api/character")] 
    public class CharacterV2Controller :ControllerBase
    {
        
        private readonly ICharacterService _charaterService;
        private readonly IDistributedCache _cache;

        public CharacterV2Controller(ICharacterService charaterService, IDistributedCache cache)
        {
            _cache = cache;
            this._charaterService = charaterService;
        }

        [ApiVersion("2.0")]
        [HttpGet("GetAll")]
        
        public async Task<IActionResult> Get([FromQuery] CharacterParams characterParams)
        {
            // throw new AppException("Test Middleware");
            int id = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var val = await _cache.GetRecordAsync<dynamic>($"GetAllCharacterFor_{id}");
            if (val is null)
            {
                Console.WriteLine("Get From DB Service API v2");
                var res = await _charaterService.GetAllCharacters(id, characterParams);
                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(res.Data.MetaData));
                await _cache.SetRecordAsync<ServiceResponse<PagedList<GetCharacterDto>>>($"GetAllCharacterFor_{id}", res);
                return Ok(res);
            }
            Console.WriteLine("Get From Cache API v2");
            return Ok(val);

        }
    }
}