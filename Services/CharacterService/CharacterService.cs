using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using dotnet_rpg.Data;
using dotnet_rpg.Dtos.Charater;
using dotnet_rpg.Entities;
using dotnet_rpg.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_rpg.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {

        public IMapper _mapper;
        private readonly DataContext _context;

        public CharacterService(IMapper mapper, DataContext context)
        {
            this._context = context;
            this._mapper = mapper;

        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharater(AddCharaterDto newCharacter)
        {
            Character _charater = _mapper.Map<Character>(newCharacter);
            await _context.characters.AddAsync(_charater);
            await _context.SaveChangesAsync();
            var response = new ServiceResponse<List<GetCharacterDto>>()
            {
                Data = _context.characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList()
            };
            return response;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
            var response = new ServiceResponse<List<GetCharacterDto>>();
            try
            {
                Character character = _context.characters.FirstOrDefault(c => c.Id == id);
                _context.Remove(character);
                await _context.SaveChangesAsync();
                response.Data = _context.characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            }
            catch (System.Exception ex)
            {

                response.Success = false;
                response.Message = ex.Message.ToString();
            }
            return response;
        }

        public async Task<ServiceResponse<PagedList<GetCharacterDto>>> GetAllCharacters(int userId, CharacterParams characterParams)
        {
            var characters = await _context.characters.Where(c => c.User.Id == userId)
            .Skip((characterParams.PageNumber - 1) * characterParams.PageSize)
            .Take(characterParams.PageSize)
            .ToListAsync();
            var data = PagedList<GetCharacterDto>.ToPagedList(characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList(),characterParams.PageNumber,characterParams.PageSize);
            return new ServiceResponse<PagedList<GetCharacterDto>>() { Data = data };
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharaterById(int id)
        {
            var character = await _context.characters.FirstOrDefaultAsync(c => c.Id == id);
            return new ServiceResponse<GetCharacterDto>() { Data = _mapper.Map<GetCharacterDto>(character) };
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updateCharacter)
        {
            var response = new ServiceResponse<GetCharacterDto>();
            try
            {
                Character character = await _context.characters.FirstOrDefaultAsync(c => c.Id == updateCharacter.Id);
                character.Name = updateCharacter.Name;
                character.Class = updateCharacter.Class;
                character.Defense = updateCharacter.Defense;
                character.HitPoints = updateCharacter.HitPoints;
                character.Intelligence = updateCharacter.Intelligence;
                character.Strength = updateCharacter.Strength;
                _context.characters.Update(character);
                await _context.SaveChangesAsync();
                response.Data = _mapper.Map<GetCharacterDto>(character);
            }
            catch (System.Exception ex)
            {

                response.Success = false;
                response.Message = ex.Message.ToString();
            }
            return response;
        }
    }
}