namespace MiniCafe.Appliances
{
    public class HibiscusProvider : CustomAppliance
    {
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Hibiscus Provider");
        public override string UniqueNameID => "hibiscus_provider";
        public override List<(Locale, ApplianceInfo)> InfoList => new()
        {
            (Locale.English, LocalisationUtils.CreateApplianceInfo("Hibiscus Plant", "Provides hibiscus flowers", new(), new()))
        };
        public override bool IsPurchasable => true;
        public override PriceTier PriceTier => PriceTier.Cheap;
        public override RarityTier RarityTier => RarityTier.Common;
        public override ShoppingTags ShoppingTags => ShoppingTags.Cooking;
        public override bool SellOnlyAsDuplicate => true;

        public override List<IApplianceProperty> Properties => new()
        {
            GetUnlimitedCItemProvider(GetCustomGameDataObject<Hibiscus>().ID),
        };

        public override void OnRegister(Appliance gdo)
        {
            var plant = Prefab.GetChild("plant");
            plant.ApplyMaterialToChildCafe("pot", "Plastic - Red", "Soil");
            plant.ApplyMaterialToChildCafe("trunk", "Wood - Autumn");
            plant.ApplyMaterialToChildCafe("leaf", "Plant  Leafy");
            plant.ApplyMaterialToChildCafe("leaves", "Plant  Leafy");

            Prefab.ApplyMaterialToChildren("flower", "AppleRed", "AppleRed");
        }
    }
}
