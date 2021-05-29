namespace dotnet_rpg.Entities
{
    public class CharacterParams : RequestParameters
    {
        public uint MinStrength { get; set; } = 0;
        public uint MaxStrength { get; set; } = int.MaxValue;

        public uint MinDefense { get; set; } = 0;
        public uint MaxDefense { get; set; } = int.MaxValue;

        public uint MinIntelligence { get; set; } = 0;
        public uint MaxIntelligence { get; set; } = int.MaxValue;


        public string Search { get; set; }
    }
}