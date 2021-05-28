using dotnet_rpg.Enums;

namespace dotnet_rpg.Dtos.Charater
{
    public class AddCharaterDto
    {
        public string Name { get; set; } = "Ali";
        public int HitPoints { get; set; } = 100;
        public int Strength { get; set; } = 10;
        public int Defense { get; set; } = 10;
        public int Intelligence { get; set; } = 10;

        public RgpClass Class { get; set; } = RgpClass.Knight;
    }
}