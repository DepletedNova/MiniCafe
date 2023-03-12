namespace MiniCafe.Appliances
{
    // Appliance
    public class MugCabinetDebug : CustomAppliance
    {
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Mug Cabinet");
        public override string UniqueNameID => "mug_provider_dirty";
        public override List<(Locale, ApplianceInfo)> InfoList => new()
        {
            (Locale.English, LocalisationUtils.CreateApplianceInfo("Mugs (DEBUG)", "Provides both large and small dirty mugs", new(), new()))
        };
        public override bool PreventSale => true;

        public override List<IApplianceProperty> Properties => new()
        {
            GetCItemProvider(SmallMugDirty.ItemID, 6, 6, false, false, false, false, true, true, false),
            new CDualLimitedProvider()
            {
                Current = 1,
                Provide1 = SmallMugDirty.ItemID,
                Available1 = 6,
                Maximum1 = 6,
                Provide2 = BigMugDirty.ItemID,
                Available2 = 6,
                Maximum2 = 6,
            }
        };
    }
}
