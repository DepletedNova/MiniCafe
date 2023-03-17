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
            GetCItemProvider(GetCustomGameDataObject<CannedWhippedCream>().ID, 3, 3, false, false, false, false, false, true, false),
            new CItemHolder()
        };

        public override void OnRegister(Appliance gdo)
        {
            var holdTransform = Prefab.TryAddComponent<HoldPointContainer>().HoldPoint = Prefab.transform.Find("HoldPoint");
            var limitedSource = Prefab.TryAddComponent<LimitedItemSourceView>();
            limitedSource.HeldItemPosition = holdTransform;
            var whipped = Prefab.GetChild("Whipped");
            ReflectionUtils.GetField<LimitedItemSourceView>("Items").SetValue(limitedSource, new List<GameObject>()
            {
                whipped.GetChild("Can 0"),
                whipped.GetChild("Can 1"),
                whipped.GetChild("Can")
            });

            whipped.ApplyMaterialToChildren("Can", "Metal", "Plastic - White", "Plastic - Red");

            var stand = Prefab.GetChild("Stand/Model");
            stand.ApplyMaterialToChildCafe("Body", "Wood 4 - Painted");
            stand.ApplyMaterialToChildCafe("Doors", "Wood 4 - Painted");
            stand.ApplyMaterialToChildCafe("Handles", "Metal - Brass");
            stand.ApplyMaterialToChildCafe("Sides", "Wood - Default");
            stand.ApplyMaterialToChildCafe("Top", "Wood - Default");
        }
    }
}
