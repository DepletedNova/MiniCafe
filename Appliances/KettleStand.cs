namespace MiniCafe.Appliances
{
    public class KettleStand : CustomAppliance
    {
        public static int KettleID { get; private set; }

        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Kettle Stand");
        public override string UniqueNameID => "kettle_provider";
        public override List<(Locale, ApplianceInfo)> InfoList => new()
        {
            (Locale.English, LocalisationUtils.CreateApplianceInfo("Kettles", "Provides kettles", new(), new()))
        };
        public override bool SellOnlyAsDuplicate => true;
        public override bool IsPurchasable => true;
        public override PriceTier PriceTier => PriceTier.Cheap;
        public override RarityTier RarityTier => RarityTier.Common;
        public override ShoppingTags ShoppingTags => ShoppingTags.Cooking;

        public override List<IApplianceProperty> Properties => new()
        {
            GetLimitedCItemProvider(GetCustomGameDataObject<Kettle>().ID, 2, 2),
        };

        public override void OnRegister(Appliance gdo)
        {
            KettleID = gdo.ID;

            var stand = Prefab.GetChild("Stand");
            List<GameObject> kettleList = new();
            for (int i = 0; i < stand.GetChildCount(); i++)
                kettleList.Add(stand.GetChild(i));

            // View
            Prefab.TryAddComponent<LocalLimitedItemSourceView>().Items = kettleList;

            // Materials
            GameObject parent = Prefab.GetChild("Block/Counter2");
            var paintedWood = GetMaterialArray("Wood 4 - Painted");
            var defaultWood = GetMaterialArray("Wood - Default");
            parent.ApplyMaterialToChildCafe("Counter", paintedWood);
            parent.ApplyMaterialToChildCafe("Counter Doors", paintedWood);
            parent.ApplyMaterialToChildCafe("Counter Surface", defaultWood);
            parent.ApplyMaterialToChildCafe("Counter Top", defaultWood);
            parent.ApplyMaterialToChildCafe("Handles", "Knob");

            stand.ApplyMaterial("Wood 1 - Dim", "Metal Very Dark");
            stand.ApplyMaterialToChildren("Kettle", "Plastic", "Metal Dark", "Metal", "Hob Black");
        }
    }
}
