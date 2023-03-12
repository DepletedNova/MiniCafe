namespace MiniCafe.Appliances
{
    // Appliance
    public class MugCabinet : CustomAppliance
    {
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Mug Cabinet");
        public override string UniqueNameID => "mug_provider";
        public override List<(Locale, ApplianceInfo)> InfoList => new()
        {
            (Locale.English, LocalisationUtils.CreateApplianceInfo("Mugs", "Provides both large and small mugs", new(), new()))
        };
        public override bool IsPurchasable => true;
        public override PriceTier PriceTier => PriceTier.Cheap;
        public override RarityTier RarityTier => RarityTier.Common;
        public override ShoppingTags ShoppingTags => ShoppingTags.Misc;

        public override List<IApplianceProperty> Properties => new()
        {
            GetCItemProvider(SmallMug.ItemID, 6, 6, false, false, false, false, true, true, false),
            new CDualLimitedProvider()
            {
                Current = 1,
                Provide1 = SmallMug.ItemID,
                Available1 = 6,
                Maximum1 = 6,
                Provide2 = BigMug.ItemID,
                Available2 = 6,
                Maximum2 = 6,
            }
        };

        public override void OnRegister(GameDataObject gdo)
        {
            var cabinet = Prefab.GetChild("Cabinet");
            var bigMugs = cabinet.GetChild("Large Mugs");
            var smallMugs = cabinet.GetChild("Small Mugs");

            // View
            if (!Prefab.HasComponent<DualLimitedSourceView>())
            {
                var sourceView = Prefab.AddComponent<DualLimitedSourceView>();

                for (int i = 0; i < smallMugs.GetChildCount(); i++)
                    sourceView.Items1.Add(smallMugs.GetChild(i).gameObject);

                for (int i = 0; i < bigMugs.GetChildCount(); i++)
                    sourceView.Items2.Add(bigMugs.GetChild(i).gameObject);

                sourceView.Animator = Prefab.GetComponent<Animator>();
            }

            // Materials
            GameObject parent = Prefab.GetChildFromPath("Block/Counter2");
            var paintedWood = GetMaterialArray("Wood 4 - Painted");
            var defaultWood = GetMaterialArray("Wood - Default");
            parent.ApplyMaterialToChild("Counter", paintedWood);
            parent.ApplyMaterialToChild("Counter Doors", paintedWood);
            parent.ApplyMaterialToChild("Counter Surface", defaultWood);
            parent.ApplyMaterialToChild("Counter Top", defaultWood);
            parent.ApplyMaterialToChild("Handles", "Knob");

            for (int i = 0; i < smallMugs.GetChildCount(); i++)
                SmallMug.ApplyMugMaterials(smallMugs.GetChild(i));
            for (int i = 0; i < bigMugs.GetChildCount(); i++)
                BigMug.ApplyMugMaterials(bigMugs.GetChild(i));

            cabinet.ApplyMaterialToChild("cabinet", "Wood");
            cabinet.ApplyMaterialToChild("door", "Door Glass", "Wood", "Metal");
        }
    }
}
