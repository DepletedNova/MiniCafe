namespace MiniCafe.Appliances
{
    public class SmallMugHolder : CustomAppliance
    {
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Small Mug Rack");
        public override string UniqueNameID => "small_mug_provider";
        public override List<(Locale, ApplianceInfo)> InfoList => new()
        {
            (Locale.English, LocalisationUtils.CreateApplianceInfo("Small Mugs", "Provides only small mugs", new(), new()))
        };
        public override bool IsPurchasableAsUpgrade => true;
        public override bool IsPurchasable => true;
        public override PriceTier PriceTier => PriceTier.Medium;
        public override RarityTier RarityTier => RarityTier.Common;
        public override ShoppingTags ShoppingTags => ShoppingTags.Misc;
        public override List<Appliance.ApplianceProcesses> Processes => new()
        {
            new()
            {
                Process = GetCastedGDO<Process, RequiresMugProcess>()
            }
        };
        public override List<Process> RequiresProcessForShop => new()
        {
            GetCastedGDO<Process, RequiresMugProcess>()
        };

        public override List<Appliance> Upgrades => new()
        {
            GetCastedGDO<Appliance, BigMugHolder>()
        };

        public override List<IApplianceProperty> Properties => new()
        {
            GetLimitedCItemProvider(SmallMug.ItemID, 8, 8),
        };

        public override void OnRegister(Appliance gdo)
        {
            var mugs = Prefab.GetChild("Mugs");
            List<GameObject> mugList = new();
            for (int i = 0; i < mugs.GetChildCount(); i++)
                mugList.Add(mugs.GetChild(i));
            mugList.Reverse();

            // View
            Prefab.TryAddComponent<LocalLimitedItemSourceView>().Items = mugList;

            // Materials
            GameObject parent = Prefab.GetChild("Block/Counter2");
            var paintedWood = GetMaterialArray("Wood 4 - Painted");
            var defaultWood = GetMaterialArray("Wood - Default");
            parent.ApplyMaterialToChildCafe("Counter", paintedWood);
            parent.ApplyMaterialToChildCafe("Counter Doors", paintedWood);
            parent.ApplyMaterialToChildCafe("Counter Surface", defaultWood);
            parent.ApplyMaterialToChildCafe("Counter Top", defaultWood);
            parent.ApplyMaterialToChildCafe("Handles", "Knob");

            foreach (var mug in mugList)
                SmallMug.ApplyMugMaterials(mug);

            Prefab.ApplyMaterialToChildCafe("Holder", "Wood 1 - Dim");
        }
    }
}
