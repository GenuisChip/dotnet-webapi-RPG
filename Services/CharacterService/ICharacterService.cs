using System.Collections.Generic;
using System.Threading.Tasks;
using dotnet_rpg.Dtos.Charater;
using dotnet_rpg.Entities;
using dotnet_rpg.Models;

namespace dotnet_rpg.Services.CharacterService
{
    public interface ICharacterService
    {
        Task<ServiceResponse<PagedList<GetCharacterDto>>> GetAllCharacters(int userId,CharacterParams characterParams);
        Task<ServiceResponse<GetCharacterDto>> GetCharaterById(int id);
        Task<ServiceResponse<List<GetCharacterDto>>> AddCharater(AddCharaterDto character);

        Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updateCharacter);

         Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id);
    }
}