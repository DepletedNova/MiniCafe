namespace MiniCafe.Appliances
{
    public class BigMugHolder : CustomAppliance
    {
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Big Mug Rack");
        public override string UniqueNameID => "big_mug_provider";
        public override List<(Locale, ApplianceInfo)> InfoList => new()
        {
            (Locale.English, LocalisationUtils.CreateApplianceInfo("Big Mugs", "Provides only small mugs", new(), new()))
        };
        public override bool IsPurchasableAsUpgrade => true;
        public override bool IsPurchasable => true;
        public override PriceTier PriceTier => PriceTier.Medium;
        public override RarityTier RarityTier => RarityTier.Uncommon;
        public override ShoppingTags ShoppingTags => ShoppingTags.Misc;
        public override List<Process> RequiresProcessForShop => new()
        {
            GetCastedGDO<Process, CuplessFillCupProcess>()
        };

        public override List<Appliance> Upgrades => new()
        {
            GetCastedGDO<Appliance, SmallMugHolder>()
        };

        public override List<IApplianceProperty> Properties => new()
        {
            GetLimitedCItemProvider(BigMug.ItemID, 8, 8),
        };

        public override void OnRegister(GameDataObject gdo)
        {
            var mugs = Prefab.GetChild("Mugs");
            List<GameObject> mugList = new();
            for (int i = 0; i < mugs.GetChildCount(); i++)
                mugList.Add(mugs.GetChild(i));
            mugList.Reverse();

            // View
            Prefab.TryAddComponent<LocalLimitedItemSourceView>().Items = mugList;

            // Materials
            GameObject parent = Prefab.GetChildFromPath("Block/Counter2");
            var paintedWood = GetMaterialArray("Wood 4 - Painted");
            var defaultWood = GetMaterialArray("Wood - Default");
            parent.ApplyMaterialToChild("Counter", paintedWood);
            parent.ApplyMaterialToChild("Counter Doors", paintedWood);
            parent.ApplyMaterialToChild("Counter Surface", defaultWood);
            parent.ApplyMaterialToChild("Counter Top", defaultWood);
            parent.ApplyMaterialToChild("Handles", "Knob");

            foreach (var mug in mugList)
                BigMug.ApplyMugMaterials(mug);

            Prefab.ApplyMaterialToChild("Holder", "Wood 1 - Dim");
        }
    }
}
