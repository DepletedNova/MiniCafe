namespace MiniCafe.Processes
{
    public class CuplessFillCupProcess : CustomProcess
    {
        public override string UniqueNameID => "cupless_fill_cup";
        public override bool CanObfuscateProgress => true;
        public override int EnablingApplianceCount => 2;
        public override GameDataObject BasicEnablingAppliance => GetCustomGameDataObject<CuplessCoffeeMachine>().GameDataObject;
        public override Process IsPseudoprocessFor => GetGDO<Process>(ProcessReferences.FillCoffee);
        public override List<(Locale, ProcessInfo)> InfoList => new()
        {
            (Locale.English, LocalisationUtils.CreateProcessInfo("Fill Cup", "<sprite name=\"fill_cup\">"))
        };
    }
}
