using System.Collections.Generic;
using System.Linq;
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
        public IActionResult Get()
        {
            return Ok(_charaterService.GetAllCharacters());
        }

        [HttpGet("{id}")]
        public IActionResult GetSingle(int id)
        {
            return Ok(_charaterService.GetCharaterById(id));
        }

        [HttpPost]
        public IActionResult AddCharacter(Character newCharacter)
        {
            return Ok(_charaterService.AddCharater(newCharacter));
        }
    }


}