namespace MiniCafe.Processes
{
    public class SteamProcess : CustomProcess
    {
        public override string UniqueNameID => "steam_milk";
        public override bool CanObfuscateProgress => true;
        public override int EnablingApplianceCount => 1;
        public override GameDataObject BasicEnablingAppliance => GetExistingGDO(ApplianceReferences.CoffeeMachine);
        public override List<(Locale, ProcessInfo)> InfoList => new()
        {
            (Locale.English, LocalisationUtils.CreateProcessInfo("Steam", "<sprite name=\"steam_0\">"))
        };
    }
}
