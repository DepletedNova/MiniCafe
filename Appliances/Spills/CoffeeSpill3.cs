namespace MiniCafe.Appliances.Spills
{
    public class CoffeeSpill3 : CustomAppliance
    {
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Coffee Spill 3");
        public override string UniqueNameID => "coffee_spill_3";
        public override List<(Locale, ApplianceInfo)> InfoList => new()
        {
            (Locale.English, LocalisationUtils.CreateApplianceInfo("Mess - Coffee 3", "", new(), new()))
        };
        public override bool IsPurchasable => false;
        public override EntryAnimation EntryAnimation => EntryAnimation.Mess;
        public override ExitAnimation ExitAnimation => ExitAnimation.MessDestroy;
        public override OccupancyLayer Layer => OccupancyLayer.Floor;
        public override bool ForceHighInteractionPriority => true;

        public override List<IApplianceProperty> Properties => new()
        {
            new CSlowPlayer()
            {
                Radius = 0.45f,
                Factor = 0.3f
            },
            new CTakesDuration()
            {
                Total = 5,
                RelevantTool = DurationToolType.Clean,
                Manual = true,
                Mode = InteractionMode.Items
            },
            new CDestroyAfterDuration(),
            new CDestroyApplianceAtNight(),
            new CDisplayDuration()
            {
                Process = ProcessReferences.Clean
            }
        };

        public override void OnRegister(Appliance gdo)
        {
            Prefab.ApplyMaterialToChildCafe("spill", "Coffee - Black");
        }
    }
}
