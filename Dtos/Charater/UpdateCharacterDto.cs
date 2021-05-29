using dotnet_rpg.Enums;

namespace dotnet_rpg.Dtos.Charater
{
    public class UpdateCharacterDto : CharacterManipulateDto
    {
        public int Id { get; set; }
    }
}