using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using dotnet_rpg.Dtos.Charater;
using dotnet_rpg.Entities;
using dotnet_rpg.Models;
using dotnet_rpg.Services.CharacterService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        public CharacterController(ICharacterService charaterService)
        {
            this._charaterService = charaterService;
        }

        //[AllowAnonymous] to get this without authentication
        [HttpGet("GetAll")]
        public async Task<IActionResult> Get([FromQuery] CharacterParams characterParams)
        {
            int id = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var res = await _charaterService.GetAllCharacters(id, characterParams);
            Response.Headers.Add("X-Pagination",JsonConvert.SerializeObject(res.Data.MetaData));
            return Ok(res);
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