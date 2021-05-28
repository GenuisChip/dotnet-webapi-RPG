using System.Collections.Generic;
using dotnet_rpg.Models;

namespace dotnet_rpg.Services.CharacterService
{
    public interface ICharacterService
    {
         List<Character> GetAllCharacters();
         Character GetCharaterById(int id);
         List<Character> AddCharater(Character character);
    }
}