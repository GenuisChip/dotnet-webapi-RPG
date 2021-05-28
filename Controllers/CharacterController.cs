using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_rpg.Models;
using dotnet_rpg.Services.CharacterService;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_rpg.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CharacterController : ControllerBase
    {

        private static List<Character> charaters = new List<Character>{
             new Character(),
             new Character(){Id=1,Name="Ahmed"},
        };

        private readonly  ICharacterService _charaterService ;

        public CharacterController(ICharacterService charaterService)
        {
            this._charaterService = charaterService;
        }

        // [HttpGet("GetAll")] == [Route("GetAll")]
        [HttpGet("GetAll")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _charaterService.GetAllCharacters());
        }

        [HttpGet("{id}")]
        public  async Task<IActionResult> GetSingle(int id)
        {
            return Ok(await _charaterService.GetCharaterById(id));
        }

        [HttpPost]
        public async Task<IActionResult> AddCharacter(Character newCharacter)
        {
            return Ok(await _charaterService.AddCharater(newCharacter));
        }
    }


}