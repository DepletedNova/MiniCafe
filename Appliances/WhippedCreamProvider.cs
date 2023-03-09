namespace MiniCafe.Appliances
{
    public class WhippedCreamProvider : CustomAppliance
    {
        public override string UniqueNameID => "whipped_cream_source";
        public override List<(Locale, ApplianceInfo)> InfoList => new()
        {
            (Locale.English, LocalisationUtils.CreateApplianceInfo("Canned Whipped Cream", "Provides canned whipped cream", new(), new()))
        };
        public override bool SellOnlyAsDuplicate => true;
        public override bool IsPurchasable => true;
        public override PriceTier PriceTier => PriceTier.Medium;
        public override ShoppingTags ShoppingTags => ShoppingTags.Cooking | ShoppingTags.Misc;
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Whipped Cream Store");

        public override List<IApplianceProperty> Properties => new List<IApplianceProperty>()
        {
            GetLimitedCItemProvider(GetCustomGameDataObject<CannedWhippedCream>().ID, 3, 3)
        };

        public override void OnRegister(GameDataObject gdo)
        {
            gdo.name = "Whipped Cream Source";

            var holdTransform = Prefab.TryAddComponent<HoldPointContainer>().HoldPoint = Prefab.transform.Find("HoldPoint");
            var limitedSource = Prefab.TryAddComponent<LimitedItemSourceView>();
            limitedSource.HeldItemPosition = holdTransform;
            ReflectionUtils.GetField<LimitedItemSourceView>("Items").SetValue(limitedSource, new List<GameObject>()
            {
                Prefab.GetChild("Whipped Cream (0)"),
                Prefab.GetChild("Whipped Cream (1)"),
                Prefab.GetChild("Whipped Cream (2)")
            });

            for (int i = 0; i < Prefab.GetChildCount(); i++)
                if (Prefab.GetChild(i).name.Contains("Whipped"))
                    Prefab.GetChild(i).ApplyMaterialToChild("Can", "Metal", "Plastic - White", "Plastic - Red");

            var stand = Prefab.GetChildFromPath("Stand/Model");
            stand.ApplyMaterialToChild("Body", "Wood 4 - Painted");
            stand.ApplyMaterialToChild("Doors", "Wood 4 - Painted");
            stand.ApplyMaterialToChild("Handles", "Metal - Brass");
            stand.ApplyMaterialToChild("Sides", "Wood - Default");
            stand.ApplyMaterialToChild("Top", "Wood - Default");
        }
    }
}
