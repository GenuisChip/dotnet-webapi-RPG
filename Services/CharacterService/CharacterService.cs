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
        public async Task<List<Character>> AddCharater(Character newCharacter)
        {
          charaters.Add(newCharacter);
            return charaters;
        }

        public async Task<List<Character>> GetAllCharacters()
        {
            return charaters;
        }

        public async Task<Character> GetCharaterById(int id)
        {
            return charaters.FirstOrDefault(c => c.Id == id);
        }
    }
}