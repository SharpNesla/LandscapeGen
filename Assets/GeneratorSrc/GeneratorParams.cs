namespace Assets
{
    public struct GeneratorParams
    {
        public double BeachLevel, WaterLevel, RockyLevel, GrassLevel;
        public double RockyElevation, GrassElevation, SnowElevation;
        public double ForestminLevel, ForestmaxLevel;
        public int MaxTreeChance;
        public const double SnowLevel = 1;

        public GeneratorParams(float waterLevel, float beachlevel, float grasslevel, float rockylevel, float forestminLevel, float forestmaxLevel,
            float rockyElevation, float grassElevation, float snowElevation, int treeChance)
        {
            BeachLevel = beachlevel;
            WaterLevel = waterLevel;
            GrassLevel = grasslevel;
            RockyLevel = rockylevel;
            RockyElevation = rockyElevation;
            GrassElevation = grassElevation;
            SnowElevation = snowElevation;
            ForestminLevel = forestminLevel;
            ForestmaxLevel = forestmaxLevel;
            MaxTreeChance = treeChance;
        }
    }
}