using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_rpg.Models;

namespace dotnet_rpg.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {

        private static List<Character> charaters = new List<Character>{
             new Character(),
             new Character(){Id=1,Name="Ahmed"},
        };
        public async Task<ServiceResponse<List<Character>>> AddCharater(Character newCharacter)
        {
            charaters.Add(newCharacter);
            var response = new ServiceResponse<List<Character>>()
            {
                Data = charaters
            };
            return response;
        }

        public async Task<ServiceResponse<List<Character>>> GetAllCharacters()
        {
            return new ServiceResponse<List<Character>>() { Data = charaters };
        }

        public async Task<ServiceResponse<Character>> GetCharaterById(int id)
        {
            return new ServiceResponse<Character>(){Data=charaters.FirstOrDefault(c => c.Id == id)};
        }
    }
}