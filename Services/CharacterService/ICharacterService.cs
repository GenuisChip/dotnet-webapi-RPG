using System.Collections.Generic;
using System.Threading.Tasks;
using dotnet_rpg.Models;

namespace dotnet_rpg.Services.CharacterService
{
    public interface ICharacterService
    {
         Task<List<Character>> GetAllCharacters();
        Task< Character> GetCharaterById(int id);
        Task< List<Character>> AddCharater(Character character);
    }
}