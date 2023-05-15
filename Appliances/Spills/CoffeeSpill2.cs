namespace MiniCafe.Appliances.Spills
{
    public class CoffeeSpill2 : CustomAppliance
    {
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Coffee Spill 2");
        public override string UniqueNameID => "coffee_spill_2";
        public override List<(Locale, ApplianceInfo)> InfoList => new();
        public override bool IsPurchasable => false;
        public override EntryAnimation EntryAnimation => EntryAnimation.Mess;
        public override ExitAnimation ExitAnimation => ExitAnimation.MessDestroy;
        public override OccupancyLayer Layer => OccupancyLayer.Floor;
        public override bool ForceHighInteractionPriority => true;

        public override List<IApplianceProperty> Properties => new()
        {
            new CSlowPlayer()
            {
                Radius = 0.35f,
                Factor = 0.4f
            },
            new CTakesDuration()
            {
                Total = 2f,
                RelevantTool = DurationToolType.Clean,
                Manual = true,
                Mode = InteractionMode.Items
            },
            new CDestroyAfterDuration(),
            new CDestroyApplianceAtNight(),
            new CDisplayDuration()
            {
                Process = ProcessReferences.Clean
            },
            new CStackableMess()
            {
                BaseMess = GetCustomGameDataObject<CoffeeSpill1>().ID,
                NextMess = GetCustomGameDataObject<CoffeeSpill3>().ID
            }
        };

        public override void OnRegister(Appliance gdo)
        {
            Prefab.ApplyMaterialToChildCafe("spill", "Coffee - Black");
        }
    }
}
