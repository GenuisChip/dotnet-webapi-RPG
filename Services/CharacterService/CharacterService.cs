using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using dotnet_rpg.Dtos.Charater;
using dotnet_rpg.Models;

namespace dotnet_rpg.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {

        private static List<Character> characters = new List<Character>{
             new Character(),
             new Character(){Id=1,Name="Ahmed"},
        };
        public IMapper _mapper;

        public CharacterService(IMapper mapper)
        {
            this._mapper = mapper;

        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharater(AddCharaterDto newCharacter)
        {
            Character _charater = _mapper.Map<Character>(newCharacter);
            _charater.Id = characters.Max(c => c.Id) + 1;
            characters.Add(_charater);
            var response = new ServiceResponse<List<GetCharacterDto>>()
            {
                Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList()
            };
            return response;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {
            return new ServiceResponse<List<GetCharacterDto>>() { Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList() };
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharaterById(int id)
        {
            return new ServiceResponse<GetCharacterDto>() { Data = _mapper.Map<GetCharacterDto>(characters.FirstOrDefault(c => c.Id == id)) };
        }
    }
}