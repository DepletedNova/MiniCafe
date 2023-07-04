namespace MiniCafe.Processes
{
    public class BoilWaterProcess : CustomProcess
    {
        public override string UniqueNameID => "boil_water";
        public override bool CanObfuscateProgress => true;
        public override int EnablingApplianceCount => 1;
        public override GameDataObject BasicEnablingAppliance => GetCustomGameDataObject<Boiler>().GameDataObject;
        public override List<(Locale, ProcessInfo)> InfoList => new()
        {
            (Locale.English, LocalisationUtils.CreateProcessInfo("Boil Water", "<sprite name=\"steam_0\">"))
        };
    }
}
