using System.ComponentModel.DataAnnotations;
using dotnet_rpg.Enums;

namespace dotnet_rpg.Dtos.Charater
{
    public class CharacterManipulateDto
    {
        [Required(ErrorMessage = "Name is Required")]
        [MinLength(2, ErrorMessage = "Minimum 2 Charatcer")]
        public string Name { get; set; }  
        public int HitPoints { get; set; } = 100;
        public int Strength { get; set; } = 10;
        public int Defense { get; set; } = 10;
        public int Intelligence { get; set; } = 10;

        public RgpClass Class { get; set; } = RgpClass.Knight;
    }
}