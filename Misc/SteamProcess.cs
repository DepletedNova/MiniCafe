namespace MiniCafe.Misc
{
    public class SteamProcess : CustomProcess
    {
        public override string UniqueNameID => "steam_milk";
        public override bool CanObfuscateProgress => true;
        public override int EnablingApplianceCount => 1;
        public override GameDataObject BasicEnablingAppliance => GetCustomGameDataObject<BaristaMachine>().GameDataObject;

        public override void OnRegister(GameDataObject gdo)
        {
            gdo.name = "Steam Process";
        }
    }
}
