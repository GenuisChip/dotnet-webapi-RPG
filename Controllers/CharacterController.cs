using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using dotnet_rpg.Dtos.Charater;
using dotnet_rpg.Entities;
using dotnet_rpg.Extensions;
using dotnet_rpg.Helpers;
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
    [Route("[controller]")]
    public class CharacterController : ControllerBase
    {

        private static List<Character> charaters = new List<Character>{
             new Character(),
             new Character(){Id=1,Name="Ahmed"},
        };

        private readonly ICharacterService _charaterService;
        private readonly IDistributedCache _cache;

        public CharacterController(ICharacterService charaterService, IDistributedCache cache)
        {
            _cache = cache;
            this._charaterService = charaterService;
        }

        //[AllowAnonymous] to get this without authentication
        [HttpGet("GetAll")]
        public async Task<IActionResult> Get([FromQuery] CharacterParams characterParams)
        {
            // throw new AppException("Test Middleware");
            int id = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var val = await _cache.GetRecordAsync<dynamic>($"GetAllCharacterFor_{id}");
            if (val is null)
            {
                Console.WriteLine("Get From DB Service");
                var res = await _charaterService.GetAllCharacters(id, characterParams);
                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(res.Data.MetaData));
                await _cache.SetRecordAsync<ServiceResponse<PagedList<GetCharacterDto>>>($"GetAllCharacterFor_{id}", res);
                return Ok(res);
            }
            Console.WriteLine("Get From Cache");
            return Ok(val);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle(int id)
        {
            return Ok(await _charaterService.GetCharaterById(id));
        }

        [HttpPost]
        public async Task<IActionResult> AddCharacter(AddCharaterDto newCharacter)
        {
            return Ok(await _charaterService.AddCharater(newCharacter));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCharacter(UpdateCharacterDto updateCharacter)
        {
            var response = await _charaterService.UpdateCharacter(updateCharacter);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(await _charaterService.UpdateCharacter(updateCharacter));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCharacter(int id)
        {
            var response = await _charaterService.DeleteCharacter(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }


}