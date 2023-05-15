namespace MiniCafe.Processes
{
    public class SteepProcess : CustomProcess
    {
        public override string UniqueNameID => "steep";
        public override bool CanObfuscateProgress => true;
        public override int EnablingApplianceCount => 1;
        public override GameDataObject BasicEnablingAppliance => GetCustomGameDataObject<SteepingStand>().GameDataObject;
        public override List<(Locale, ProcessInfo)> InfoList => new()
        {
            (Locale.English, LocalisationUtils.CreateProcessInfo("Steep", "<sprite name=\"steep_0\">"))
        };
    }
}
