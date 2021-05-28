using System.Collections.Generic;
using System.Linq;
using dotnet_rpg.Models;

namespace dotnet_rpg.Services.CharacterService
{
    public class CharacterService : ICharacterService
    { 
        
        private static List<Character> charaters = new List<Character>{
             new Character(),
             new Character(){Id=1,Name="Ahmed"},
        };
        public List<Character> AddCharater(Character newCharacter)
        {
          charaters.Add(newCharacter);
            return charaters;
        }

        public List<Character> GetAllCharacters()
        {
            return charaters;
        }

        public Character GetCharaterById(int id)
        {
            return charaters.FirstOrDefault(c => c.Id == id);
        }
    }
}